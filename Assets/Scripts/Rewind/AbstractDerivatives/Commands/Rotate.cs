using UnityEngine;

[System.Serializable]
public class Rotate : TransformCommand
{
    private Vector3? undoDir;
    private Vector3 dir;
    private Vector3 commandRotation;
    private float rotationThreshold = Mathf.Epsilon;

    public Rotate(Transform obj, Vector3 dir = default(Vector3)) : base(obj)
    {
        this.dir = dir;
    }
    public override void Execute()
    {
        RotateExecute();
    }
    public override void Undo()
    {
        RotateUndo();
    }
    private void RotateExecute()
    {
        undoDir = -dir;
        commandRotation = commandTransform.localRotation.eulerAngles;
        commandTransform.Rotate(dir, Space.World);
    }
    private void RotateUndo()
    {
        if (undoDir.HasValue)
        {
            commandTransform.Rotate(undoDir.Value, Space.World);
            if (GetRotationPrecise(commandTransform.localRotation.eulerAngles, commandRotation))
            {
                commandTransform.localRotation = Quaternion.Euler(commandRotation);
            }
        }
    }
    private bool GetRotationPrecise(Vector3 pos, Vector3 targetPos)
    {
        return Vector3.Distance(pos, targetPos) > rotationThreshold ? true : false;
    }
}