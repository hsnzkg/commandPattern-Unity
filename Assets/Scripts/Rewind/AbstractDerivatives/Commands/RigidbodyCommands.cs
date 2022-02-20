using UnityEngine;

[System.Serializable]
public abstract class RigidbodyCommands : Command
{
    private float RIGIDBODY_THRESHOLD = Mathf.Epsilon;
    public RigidbodyCommands(string obj) : base(obj)
    {

    }

    public Rigidbody GetCommandRigidbody()
    {
        return base.GetCommandObject().GetComponent<Rigidbody>();
    }

    public bool GetUndoPrecise(Vector3 pos, Vector3 targetPos, Vector3 rot, Vector3 targetRot, Vector3 vel, Vector3 targetVel, Vector3 angularVel, Vector3 targetAngularVel)
    {
        if (Vector3.Distance(pos, targetPos) > RIGIDBODY_THRESHOLD || Vector3.Distance(rot, targetRot) > RIGIDBODY_THRESHOLD || Vector3.Distance(vel, targetVel) > RIGIDBODY_THRESHOLD || Vector3.Distance(angularVel, targetAngularVel) > RIGIDBODY_THRESHOLD)
        {
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class AddForce : RigidbodyCommands
{
    private SVector3 forceDir;
    //private SVector3? forceUndoDir;
    private SVector3 commandPosition;
    private SQuaternion commandRotation;
    private SVector3 commandVelocity;
    private SVector3 commandAngularVelocity;


    public AddForce(string obj,Vector3 forceDir) : base(obj)
    {
        this.forceDir = new SVector3(forceDir);
        //this.forceUndoDir = new SVector3(-forceDir);
        this.commandPosition = new SVector3(base.GetCommandRigidbody().position);
        this.commandRotation = new SQuaternion(base.GetCommandRigidbody().rotation);
        this.commandVelocity = new SVector3(base.GetCommandRigidbody().velocity);
        this.commandAngularVelocity = new SVector3(base.GetCommandRigidbody().angularVelocity);
    }

    public override void Execute()
    {
        base.GetCommandRigidbody().AddForce(forceDir.Vector3);
    }

    public override void Undo()
    {   /*
        if (forceUndoDir.HasValue)
        {
            commandRigidbody.AddForce(forceUndoDir.Value);
        }
        */
        base.GetCommandRigidbody().velocity = commandVelocity.Vector3;
        base.GetCommandRigidbody().angularVelocity = commandAngularVelocity.Vector3;
        base.GetCommandRigidbody().position = commandPosition.Vector3;
        base.GetCommandRigidbody().rotation = commandRotation.Quaternion;
        /*
        if (GetUndoPrecise(commandRigidbody.position, commandPosition, commandRigidbody.rotation.eulerAngles, commandRotation.eulerAngles, commandRigidbody.velocity, commandVelocity, commandRigidbody.angularVelocity, commandAngularVelocity))
        {
      
        }
        */
    }
}