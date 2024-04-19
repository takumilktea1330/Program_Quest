using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeController : MonoBehaviour
{
    public enum State
    {
        View,
        Edit,
        Connection,
    }
    public State state;
    public void ToViewMode()
    {
        state = State.View;
    }
    public void ToEditMode()
    {
        state = State.Edit;
    }
    public void ToConnectMode()
    {
        state = State.Connection;
    }
}
