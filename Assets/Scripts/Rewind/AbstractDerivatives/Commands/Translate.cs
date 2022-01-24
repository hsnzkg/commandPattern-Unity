using UnityEngine;

[System.Serializable]
public class Translate : TransformCommand
{
    private Vector3? undoDir;
    private Vector3 dir;
    private Vector3 commandPosition;
    private float positionThreshold = Mathf.Epsilon;

    public Translate(Transform obj, Vector3 dir = default(Vector3)) : base(obj)
    {
        this.dir = dir;
    }
    public override void Execute()
    {
        TranslateExecute();
    }
    public override void Undo()
    {
        TranslateUndo();
    }
    private void TranslateExecute()
    {
        undoDir = -dir; 
        commandPosition = commandTransform.localPosition;
        commandTransform.Translate(dir, Space.World);
    }
    private void TranslateUndo()
    {
        if (undoDir.HasValue)
        {
            commandTransform.Translate(undoDir.Value, Space.World);
            if (GetPositionPrecise(commandTransform.localPosition, commandPosition))
            {
                commandTransform.localPosition = commandPosition;
            }
        }
    }
    private bool GetPositionPrecise(Vector3 pos, Vector3 targetPos)
    {
        return Vector3.Distance(pos, targetPos) > positionThreshold ? true : false;
    }
}