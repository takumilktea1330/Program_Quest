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
    protected GameObject canvas;


    /// <summary>
    /// initialize this flow
    /// </summary>
    /// <param name="id">newStructId(it must be unique)</param>
    /// <param name="type">"skill", "if" </param>
    public virtual void Init(string id)
    {
        Data = new()
        {
            StructId = id,
        };
        canvas = GameObject.Find("MainCanvas");
        //displayText = GetComponentInChildren<Text>();
    }

    public virtual void Display(Skill skill)
    {
        //displayText.text = Data.SkillName;
        gameObject.GetComponent<SpriteRenderer>().sprite = skill.DisplaySprite;
    }

    public virtual void ShowData()
    {
        Debug.Log($"This is Flow(ID: {Data.StructId}) Data\n" +
        $"Position: {transform.position}");
    }
    //private void OnMouseDrag()
    //{
    //    transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}

}