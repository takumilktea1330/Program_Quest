using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;  //AssetDatabaseを使うために追加
using System.IO;  //StreamWriterなどを使うために追加
using System.Linq; //Selectを使うために追加
using UnityEditor.VersionControl;

public class FlowChartSystem : MonoBehaviour
{
    // Instances
    [SerializeField] PlayerController player;
    [SerializeField] AddFlowSelectUI addFlowSelectUI;
    [SerializeField] SkillKindSelectUI skillKindSelectUI;
    [SerializeField] SelectSkillUI selectSkillUI;
   // [SerializeField] SwipeController swipeController;
    [SerializeField] EditSelectUI editSelectUI;


    // Lists
    List<FlowChartObject> flow;
    List<FlowChartObject> objectsList;
    List<GameObject> presentPrefabs = new();
    List<GameObject> linePrefabs = new();

    //Prefabs
    GameObject VLinePrefab;
    GameObject HLinePrefab;

    GameObject SelectObjectFramePrefab;
    GameObject SelectObjectFrame;

    FlowChartObject nextAddObject;
    FlowChartObject selectedObject;



    string datapath;

    void Awake()
    {
        //保存先の計算をする
        //これはAssets直下を指定. /以降にファイル名
        datapath = Application.dataPath + "/FlowSaveData.json";
    }
    private enum State
    {
        View,
        Edit,
        Add,
        Moving,
        AddFlowSelect,
        SkillKindSelect,
        SkillSelect,
    }

    private State state;

    public bool AddFlow(FlowChartObject addObject, Vector3 place)
    {
        FlowChartObject original = objectsList.Find(obj => obj.Place == place);

        if (original != null)
        {
            Debug.Log($"{original.Name}があった{place}に{addObject.Name}を追加します");

            IfEndObject ifTrueEndObject = null;
            IfEndObject ifFalseEndObject = null;
            WhileEndObject whileEndObject = null;

            if (addObject is IfObject)
            {
                ifTrueEndObject = new IfEndObject();
                ifFalseEndObject = new IfEndObject();

                (addObject as IfObject).TrueList.Add(ifTrueEndObject);
                ifTrueEndObject.Parent = (addObject as IfObject).TrueList;

                (addObject as IfObject).FalseList.Add(ifFalseEndObject);
                ifFalseEndObject.Parent = (addObject as IfObject).FalseList;
            }
            else if (addObject is WhileObject)
            {
                whileEndObject = new WhileEndObject();
                (addObject as WhileObject).Children.Add(whileEndObject);
                whileEndObject.Parent = (addObject as WhileObject).Children;
            }
            List<FlowChartObject> parent = original.Parent;
            addObject.Parent = parent;

            int index = parent.IndexOf(original);
            if (index == -1)
            {
                Debug.Log("error: index == -1");
                return false;
            }
            if (parent != flow && original is BlankObject)
            {
                objectsList.Remove(original);
                parent[index] = addObject;
            }
            else
                parent.Insert(index, addObject);

            MakeFlow();

            objectsList.Add(addObject);
            if (addObject is IfObject)
            {
                objectsList.Add(ifTrueEndObject);
                objectsList.Add(ifFalseEndObject);
                MakeFlow();
            }
            else if (addObject is WhileObject)
            {
                objectsList.Add(whileEndObject);
                MakeFlow();
            }
            return true;
        }
        else
        {
            Debug.Log($"original == null なので{place}にObjectを追加できません");
            return false;
        }
    }

    private void DeleteFlow()
    {
        flow.Remove(selectedObject);
        objectsList.Remove(selectedObject);
        MakeFlow();
    }

    private bool IsInstallable(Vector3 place)
    {
        if (objectsList.Find(obj => obj.Place == place) != null) return true;
        else return false;
    }

    private void MakeFlow()
    {
        List<FlowChartObject> ifList = objectsList.FindAll(obj => obj is IfObject);
        foreach (FlowChartObject obj in ifList)
        {
            if (obj is IfObject)
            {
                IfObject ifObject = (IfObject)obj;
                while (true)
                {
                    //Debug.Log($"ifObject.VSize:{ifObject.VSize}");
                    //if(ifObject.VSize>1)Debug.Log($"ifObject.TrueList[ifObject.VSize - 2]:{ifObject.TrueList[ifObject.VSize - 2]}");
                    BlankObject blankObject = new BlankObject();
                    if (ifObject.TrueVSize > ifObject.FalseVSize)
                    {
                        ifObject.FalseList.Insert(ifObject.FalseList.Count - 1, blankObject);
                        blankObject.Parent = ifObject.FalseList;
                        objectsList.Add(blankObject);
                    }
                    else if (ifObject.TrueVSize < ifObject.FalseVSize)
                    {
                        ifObject.TrueList.Insert(ifObject.TrueList.Count - 1, blankObject);
                        blankObject.Parent = ifObject.TrueList;
                        objectsList.Add(blankObject);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        Reset();
        AssembleFlow(flow, 0, 0);
    }



    /// <summary>
    /// column, rowに対してlistを画面上に表示します
    /// </summary>
    /// <param name="list"></param>
    /// <param name="column"></param>
    /// <param name="row"></param>
    private void AssembleFlow(List<FlowChartObject> list, int column, int row)
    {
        if (list.Count == 0)
        {
            return;
        }
        foreach (FlowChartObject obj in list)
        {
            linePrefabs.Add(Instantiate(VLinePrefab, LineLocation(column, row), Quaternion.identity));

            if (obj is IfObject)
            {
                GameObject hLinePrefab = Instantiate(HLinePrefab, LineLocation(column, row), Quaternion.identity) as GameObject;
                hLinePrefab.transform.localScale = new Vector3((obj as IfObject).TrueHSize, 1, 1);
                linePrefabs.Add(hLinePrefab);

                IfObject ifObject = (IfObject)obj;
                obj.Place = Location(column, row);

                ObjectDisplay(obj, column, row);

                //次の行以降を描写
                row++;
                AssembleFlow(ifObject.TrueList, column, row);
                AssembleFlow(ifObject.FalseList, column + ifObject.TrueHSize, row);
                row += ifObject.VSize - 1;
            }
            else if (obj is WhileObject)
            {
                WhileObject whileObject = (WhileObject)obj;
                obj.Place = Location(column, row);

                ObjectDisplay(obj, column, row);

                row++;
                AssembleFlow(whileObject.Children, column, row);
                row += whileObject.VSize - 1;
            }
            else
            {
                ObjectDisplay(obj, column, row);
                obj.Place = Location(column, row);
                row++;
            }
        }
    }

    private Vector3 Location(int column, int row)
    {
        return new Vector3(Constant.HorizontalSpace * column - 2.0f, Constant.VerticalSpace * row + 2.0f, 0);
    }

    private Vector3 LineLocation(int column, int row)
    {
        return Location(column, row) + new Vector3(0, 0, 1.0f);
    }

    private void Start()
    {
        Init();
        Test();
    }

    private void Init()
    {
        state = State.View;
        VLinePrefab = Resources.Load<GameObject>("Prefabs/FlowChart/VLinePrefab");
        HLinePrefab = Resources.Load<GameObject>("Prefabs/FlowChart/HLinePrefab");
        SelectObjectFramePrefab = Resources.Load<GameObject>("Prefabs/FlowChart/SelectObjectFramePrefab");

        UIInit();
        Reset();
    }

    private void UIInit()
    {
        //addFlowSelectUI.OnTouch += () => swipeController.enabled = false;
        //addFlowSelectUI.OnRelease += () => swipeController.enabled = true;

        //skillKindSelectUI.OnTouch += () => swipeController.enabled = false;
        //skillKindSelectUI.OnRelease += () => swipeController.enabled = true;

        addFlowSelectUI.SkillButtonOnClick += SkillKindSelect;
        addFlowSelectUI.IfButtonOnClick += AddIfObject;
        addFlowSelectUI.WhileButtonOnClick += AddWhileObject;

        editSelectUI.AddButtonOnClick += AddFlowSelect;
        editSelectUI.DeleteButtonOnClick += DeleteFlow;
    }


    /// <summary>
    /// This function is for a execution test
    /// it'll be deleted
    /// </summary>
    private void Test()
    {
        AddFlow(new SkillObject(player.PlayerBattler.GetRandomSkillBase()), Location(0, 0));
        AddFlow(new SkillObject(player.PlayerBattler.GetRandomSkillBase()), Location(0, 1));

        AddFlow(new IfObject(), Location(0, 2));
        AddFlow(new WhileObject(), Location(0, 3));
        AddFlow(new SkillObject(player.PlayerBattler.GetRandomSkillBase()), Location(0, 4));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Reset();
        }
        switch (state)
        {
            case State.View:
                if (Input.GetMouseButtonDown(0))
                {
                    FlowChartObject obj = GetTouchedObject();
                    if (obj != null)
                    {
                        //open property
                    }
                    else
                    {
                        //close property
                    }
                }
                break;
            case State.Add:
                if (Input.GetMouseButtonDown(0))
                {
                    FlowChartObject obj = GetTouchedObject();
                    if (obj != null)
                    {
                        StartCoroutine(SelectToWhereAddFlow(obj.Place));
                    }
                }
                break;
            case State.Edit:
                break;
            case State.Moving:
                break;
            case State.AddFlowSelect:
                break;
            case State.SkillKindSelect:
                break;
            case State.SkillSelect:
                break;
        }
    }


    private FlowChartObject GetTouchedObject()
    {
        GameObject touchedObject;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D rayCastHit2D = Physics2D.Raycast(ray.origin, ray.direction);
        if (rayCastHit2D)
        {
            touchedObject = rayCastHit2D.transform.gameObject;
            Vector3 place = touchedObject.transform.position;

            Destroy(SelectObjectFrame);
            SelectObjectFrame = Instantiate(SelectObjectFramePrefab, place + new Vector3(0, 0, -1), Quaternion.identity);

            return selectedObject = objectsList.Find(obj => obj.Place == place);
        }
        else
        {
            Destroy(SelectObjectFrame);
            return null;
        }
    }

    private void ObjectDisplay(FlowChartObject obj, int column, int row)
    {
        obj.Prefab = Instantiate(obj.OriginalPrefab, Location(column, row), Quaternion.identity);
        if (obj.DispText != null) obj.DispText.text = obj.Name;
        presentPrefabs.Add(obj.Prefab);
    }

    public void View()
    {
        state = State.View;
    }

    public void AddFlowSelect()
    {
        state = State.AddFlowSelect;
        addFlowSelectUI.Open();
    }

    private void AddIfObject()
    {
        state = State.Add;
        nextAddObject = new IfObject();
    }
    private void AddWhileObject()
    {
        state = State.Add;
        nextAddObject = new WhileObject();
    }

    private void SkillKindSelect()
    {
        Debug.Log("SkillKindSelect");
        state = State.SkillKindSelect;
        skillKindSelectUI.Open();
    }

    private void SkillSelect()
    {
        state = State.SkillSelect;
        selectSkillUI.Open();
    }

    private void AddSkillObject()
    {
        state = State.Add;
        nextAddObject = new SkillObject(player.PlayerBattler.GetRandomSkillBase());
    }


    private IEnumerator SelectToWhereAddFlow(Vector3 place)
    {
        //場所をクリックしたらそこが配置可能かどうかを判定
        //配置可能なら、配置しますか？のアラートを出す
        //配置不可能なら配置不可能であることを表示

        if (IsInstallable(place))
        {
            bool isApplied;
            isApplied = true; //いずれ確認アラートを実装
            if (isApplied)
            {
                Debug.Log("Added");
                AddFlow(nextAddObject, place);
                nextAddObject = null;
                addFlowSelectUI.Close();
                View();
                yield break;
            }
        }
    }

    private void Reset()
    {
        presentPrefabs.ForEach(obj => Destroy(obj));
        presentPrefabs.Clear();
        linePrefabs.ForEach(obj => Destroy(obj));
        linePrefabs.Clear();
    }

    public void AllClear()
    {
        state = State.View;
        flow.Clear();
        objectsList.Clear();
        Reset();
        //SetStartObjects();
    }
}