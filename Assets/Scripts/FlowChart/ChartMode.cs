using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ChartMode
{
    public enum State
    {
        View,
        Connection,
        Processing,
    }
    public static State CurrentState;
}
