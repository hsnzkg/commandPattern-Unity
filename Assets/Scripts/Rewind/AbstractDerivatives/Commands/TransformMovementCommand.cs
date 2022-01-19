using UnityEngine;

[System.Serializable]
public class TransformMovementCommand : TransformCommand
{

    private Vector3? undoDir;
    private Vector3 dir;
    private Vector3 commandPosition;
    private float positionThreshold = Mathf.Epsilon;

    public TransformMovementCommand(Transform obj, Vector3 dir = default(Vector3)) : base(obj)
    {
        this.dir = dir;
    }

    public override void Execute()
    {
        undoDir = -dir;
        commandPosition = obj.localPosition;
        obj.Translate(dir, Space.World);
    }

    public override void Undo()
    {
        if (undoDir.HasValue)
        {
            obj.Translate(undoDir.Value, Space.World);
            if (GetPositionPrecise(obj.localPosition, commandPosition))
            {
                obj.localPosition = commandPosition;
            }
        }
    }

    private bool GetPositionPrecise(Vector3 pos, Vector3 targetPos)
    {
        return Vector3.Distance(pos, targetPos) > positionThreshold ? true : false;
    }

}