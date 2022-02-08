using UnityEngine;

public class ObjectDeadCommand : TransformCommand 
{
    private IKillable killable;

    public ObjectDeadCommand(string obj, IKillable killable):base(obj)
    {
        this.killable = killable;
    }

    public override void Execute()
    {
        killable.dead = true;
    }

    public override void Undo()
    {
        killable.dead = false;
    }
}