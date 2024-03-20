using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Flow : MonoBehaviour
{
    public FlowData Data;
    Text displayText;
    Camera _camera;
    protected GameObject canvas;


    /// <summary>
    /// initialize this flow
    /// </summary>
    /// <param name="id">newStructId(it must be unique)</param>
    /// <param name="type">"skill", "if" </param>
    public virtual void Init(int id, string type)
    {
        Data = new()
        {
            StructId = id,
            Type = type,
        };
        _camera = Camera.main;
        canvas = GameObject.Find("Chart/MainCanvas");
        displayText = GetComponentInChildren<Text>();
    }

    public virtual void Display()
    {
        displayText.text = Data.SkillName;
    }

    public virtual void OpenProperty()
    {
    }

    public virtual void ShowData()
    {
        Debug.Log($"This is Flow(Struct ID: {Data.StructId}) Data\n" +
        $"Position: {transform.position}");
    }


    private void OnMouseDrag()
    {
        transform.position = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
    }

}