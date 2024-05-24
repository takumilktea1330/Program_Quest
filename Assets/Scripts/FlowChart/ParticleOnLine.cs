using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnLine : MonoBehaviour
{
    Vector3 direction;
    Vector3 start;
    Vector3 end;
    bool isEnable = false;

    readonly float speed = 0.003f;
    public void Set(Vector3 start, Vector3 end, string color = "#000000")
    {
        this.start = start;
        this.end = end;
        direction = (end - start).normalized;
        transform.position = start;

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
        Vector3 newpos = transform.position + direction * speed;
        if(Vector3.Distance(newpos, end) < 0.1f)
        {
            newpos = start;
        }
        transform.position = newpos;
    }
}
