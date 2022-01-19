using UnityEngine;

[System.Serializable]
public class TransformMovementCommand : TransformCommand
{

    [SerializeField] private Vector3? undoDir;
    [SerializeField] private Vector3 dir;

    public TransformMovementCommand(Transform obj, Vector3 dir = default(Vector3)) : base(obj)
    {
        this.dir = dir;
    }

    public override void Execute()
    {
        Debug.Log(dir + "EXECUTE COMMAND");
        undoDir = -dir;
        obj.Translate(dir, Space.World);
    }

    public override void Undo()
    {
        if (undoDir.HasValue)
        {
            Debug.Log(undoDir + "UNDO COMMAND");

            obj.Translate(undoDir.Value, Space.World);
        }
    }

}