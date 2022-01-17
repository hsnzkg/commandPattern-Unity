using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	/// <summary>
	/// Base abstract command. All commands (e.g. movement, attack) should inherit from this class.
	/// </summary>
    
public abstract class Command
{
    protected int instanceID;

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
