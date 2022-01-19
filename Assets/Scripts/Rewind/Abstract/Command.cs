using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Command
{
    [SerializeField]protected int instanceID;

    public Command(int instance = -1){
        this.instanceID = instance;
    }

    /// <summary>
    /// Perform the action.
    /// </summary>
    public abstract void Execute();


    /// <summary>
    /// Perform the reverse action.
    /// </summary>
    public abstract void Undo();
}
