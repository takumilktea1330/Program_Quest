using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnLine : MonoBehaviour
{
    Vector3 direction;
    Vector3 start;
    Vector3 end;
    int Distance = 0;
    bool isEnable = false;

    readonly float speed = 0.0008f;
    public void Set(Vector3 start, Vector3 end, string color = "#00FFFF")
    {
        this.start = start;
        this.end = end;
        direction = (end - start).normalized;
        transform.position = start;
        Distance = (int)Vector3.Distance(start, end);

        if (ColorUtility.TryParseHtmlString(color, out Color colorValue))
        {
            gameObject.GetComponent<SpriteRenderer>().color = colorValue;
        }
        else
        {
            Debug.LogWarning("Invalid color code: " + color);
        }

        isEnable = true;
    }
    void Update()
    {
        if (!isEnable) return;
        Vector3 newpos = transform.position + Distance * speed * direction;
        if(Vector3.Distance(newpos, end) < 0.1f)
        {
            newpos = start;
        }
        transform.position = newpos;
    }
}
