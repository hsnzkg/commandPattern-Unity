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
    [SerializeField] protected string commandObject;

    public Command(string commandObject = null)
    {
        this.commandObject = commandObject;
    }

    public GameObject GetCommandObject()
    {
        return GameObject.Find(commandObject);
    }

    // use execute for run and undo for re undo
    public abstract void Execute();
    public abstract void Undo();
}

[System.Serializable]
public class Idle : Command
{
    public Idle(string obj) :base(obj)
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
