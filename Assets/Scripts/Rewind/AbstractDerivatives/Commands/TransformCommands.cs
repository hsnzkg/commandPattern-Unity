using UnityEngine;

[System.Serializable]
public abstract class TransformCommand : Command
{
    protected Transform commandTransform;
    private float TRANSFORM_THRESHOLD = Mathf.Epsilon;

    public TransformCommand(string obj) : base(obj)
    {
        this.commandTransform = base.GetCommandObject().transform;
    }

    public Transform GetCommandTransform()
    {
        return commandTransform;
    }

    public bool GetUndoPrecise(Vector3 pos, Vector3 targetPos)
    {
        return Vector3.Distance(pos, targetPos) > TRANSFORM_THRESHOLD ? true : false;
    }
}

public class Translate : TransformCommand
{
    private Vector3 translateDir;
    private Vector3? translateUndoDir;
    private Vector3 commandPosition;
    public Translate(string obj, Vector3 translateDir) : base(obj)
    {
        this.commandPosition = commandTransform.localPosition;
        this.translateDir = translateDir;
        this.translateUndoDir = -translateDir;
    }

    public override void Execute()
    {
        commandTransform.Translate(translateDir, Space.World);
    }

    public override void Undo()
    {
        if (translateUndoDir.HasValue)
        {
            commandTransform.Translate(translateUndoDir.Value, Space.World);
            if (GetUndoPrecise(commandTransform.localPosition, commandPosition))
            {
                commandTransform.localPosition = commandPosition;
            }
        }
    }
}

public class Rotate : TransformCommand
{
    private Quaternion rotateDir;
    private Quaternion rotateUndoDir;
    private Quaternion commandRotation;
    public Rotate(string obj, Quaternion rotateDir) : base(obj)
    {
        this.commandRotation = commandTransform.localRotation;
        this.rotateDir = rotateDir;
        this.rotateUndoDir = Quaternion.Euler(-rotateDir.eulerAngles);
    }
    public override void Execute()
    {
        commandTransform.Rotate(rotateDir.eulerAngles, Space.World);
    }

    public override void Undo()
    {
        commandTransform.Rotate(rotateUndoDir.eulerAngles, Space.World);
        if (GetUndoPrecise(commandTransform.localRotation.eulerAngles, commandRotation.eulerAngles))
        {
            commandTransform.localRotation = commandRotation;
        }
    }
}

