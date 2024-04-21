using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlow : Flow
{
    public override void Init(string id)
    {
        base.Init(id);
        Data.Type = "Start";
    }
    public override void Display()
    {
    }
}
