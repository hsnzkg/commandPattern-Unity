using UnityEngine;

public class TransformMovementCommand : TransformCommand {

    private Vector3? undoDir;
    private Vector3 dir;

    public TransformMovementCommand (Transform obj,Vector3 dir = default(Vector3)):base(obj)
    {
        this.dir = dir;
    }

    public override void Execute()
    {
        undoDir = -dir;
        obj.Translate(dir,Space.World);
    }

    public override void Undo()
    {
        if(undoDir.HasValue){
            obj.Translate(undoDir.Value,Space.World);
        }
    }
    
}