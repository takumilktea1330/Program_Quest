using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ChartMode
{
    public enum State
    {
        Loading,
        View,
        Connection,
        Select,
        Processing,
    }
    public static State CurrentState;
}
