using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Flow : MonoBehaviour
{
    public FlowData Data;
    protected GameObject canvas; 
    protected LineRenderer line = null;
    public Flow Next;
    Coroutine blinkCoroutine;
    protected UIController uiController;

    Color blinkColor;

    public virtual void Init(string id, bool isnew = true)
    {
        if(isnew)
        {
            Data = new()
            {
                ID = id,
                Name = "New Flow",
                Type = "Flow",
                PosX = transform.position.x,
                PosY = transform.position.y
            };
        }
        canvas = GameObject.Find("MainCanvas");
        uiController = canvas.GetComponent<UIController>();
        blinkColor = Color.white;
        blinkColor.a = 0.5f;
    }

    public virtual void Display(){}

    public void ShowData()
    {
        Debug.Log($"This is Flow( {Data.Name}) Data\n" +
        $"Position: {transform.position}");
    }

    public virtual IEnumerator Connect(Flow targetFlow){ yield break; }

    private IEnumerator Blink()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        while (true)
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void StartBlink()
    {
        blinkCoroutine = StartCoroutine(Blink());
    }
    public void StopBlink()
    {
        StopCoroutine(blinkCoroutine);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void MoveFlowTo(Vector3 dst)
    {
        transform.position = dst;
        Data.PosX = dst.x;
        Data.PosY = dst.y;
    }
    public virtual void DrawConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler)
    {
        if (line != null) Destroy(line.gameObject);
        if (Next != null)
        {
            line = Instantiate(_connectLinePrefabHandler.Result, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
            line.GetComponentInChildren<ParticleOnLine>().Set(transform.position, Next.transform.position);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, Next.transform.position);
        }
    }
}