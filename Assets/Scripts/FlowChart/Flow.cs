using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Flow : MonoBehaviour
{
    public FlowData Data;
    protected GameObject canvas; 
    //PropertyWindow propertyWindow;
    protected LineRenderer line = null;
    public Flow Next;

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
        //propertyWindow = canvas.transform.Find("PropertyWindow").GetComponent<PropertyWindow>();
        blinkColor = Color.white;
        blinkColor.a = 0.5f;
    }

    public virtual void Display(){}

    public virtual void ShowData()
    {
        Debug.Log($"This is Flow( {Data.Name}) Data\n" +
        $"Position: {transform.position}");
    }

    public virtual void Connect(Flow flow){}

    public IEnumerator Blink()
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
    public virtual void DrawConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler){}

//     public void DestroyConnectLine()
//     {
//         Destroy(line);
//     }
}