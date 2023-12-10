using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFlowUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Open();
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
