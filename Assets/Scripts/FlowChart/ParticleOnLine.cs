using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnLine : MonoBehaviour
{
    Vector3 direction;
    Vector3 start;
    Vector3 end;
    bool isEnable = false;

    float speed = 0.005f;
    public void Set(Vector3 start, Vector3 end, string color = "Blue")
    {
        this.start = start;
        this.end = end;
        direction = (end - start).normalized;
        transform.position = start;
        gameObject.GetComponent<SpriteRenderer>().color = color == "Blue" ? Color.blue : Color.red;
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
