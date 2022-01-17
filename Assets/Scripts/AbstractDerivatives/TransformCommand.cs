using UnityEngine;

public abstract class TransformCommand : Command {
    protected Transform obj;

    public TransformCommand(Transform obj = null)
    {
        this.obj = obj;
    }
}