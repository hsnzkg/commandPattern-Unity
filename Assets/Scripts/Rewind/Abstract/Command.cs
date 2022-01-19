using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Command
{
    [SerializeField]protected GameObject commandObject;

    public Command(GameObject commandObject = null){
        this.commandObject = commandObject;
    }

    public GameObject GetCommandObject(){
        return commandObject;
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
