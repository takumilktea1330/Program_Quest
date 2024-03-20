using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkillFlow : Flow
{
    SelectSkillUI selectSkillUI;
    AsyncOperationHandle<GameObject> SelectSkillViewPrefabHandler;
    GameObject SelectSkillViewPrefab;
    public override void Init(int id, string type)
    {
        base.Init(id, type);
    }
    public void SelectSkill()
    {
        SelectSkillViewPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/SelectSkillViewPrefab");
        SelectSkillViewPrefab = SelectSkillViewPrefabHandler.WaitForCompletion();
        GameObject obj = Instantiate(SelectSkillViewPrefab, new Vector3(300, 250, 0), Quaternion.identity);
        obj.transform.SetParent(canvas.transform);

        selectSkillUI = obj.GetComponent<SelectSkillUI>();
    }
}
