using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Each rewindable action must be extend this class.
Each extended class must have only cloned component functions
*/

[System.Serializable]
public abstract class Command
{
    [SerializeField] protected GameObject commandObject;

    public Command(GameObject commandObject = null)
    {
        this.commandObject = commandObject;
    }

    public GameObject GetCommandObject()
    {
        return commandObject;
    }

    // use execute for run and undo for re undo
    public abstract void Execute();
    public abstract void Undo();
}


public class Idle : Command
{
    public Idle(GameObject obj) :base(obj)
    {
        //do nothing
        return;
    }
    public override void Execute()
    {
        //do nothing
        return;
    }
    public override void Undo()
    {
        //do nothing
        return;    
    }

}
