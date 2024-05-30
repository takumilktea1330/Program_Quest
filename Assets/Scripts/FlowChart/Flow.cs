using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Flow : MonoBehaviour
{
    public FlowData Data;
    protected LineRenderer line = null;
    public Flow Next;
    Coroutine blinkCoroutine;
    protected UIController uiController;

    Color blinkColor;

    public virtual void Init(FlowData data = null)
    {
        if(data == null)
        {
            Data = new()
            {
                ID = Guid.NewGuid().ToString("N"),
                Name = "*********",
                Type = "Flow",
                PosX = transform.position.x,
                PosY = transform.position.y
            };
        }
        else{
            Data = data;
        }
        uiController = GameObject.Find("MainCanvas").GetComponent<UIController>();
        blinkColor = Color.white;
        blinkColor.a = 0.5f;
    }

    public virtual void Display(){}
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
    public void MoveTo(Vector3 dst)
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