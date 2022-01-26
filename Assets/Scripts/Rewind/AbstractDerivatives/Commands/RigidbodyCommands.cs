using UnityEngine;

public abstract class RigidbodyCommands : Command
{
    protected Rigidbody commandRigidbody;
    private float RIGIDBODY_THRESHOLD = Mathf.Epsilon;

    public RigidbodyCommands(Rigidbody commandRigidbody) : base(commandRigidbody.gameObject)
    {
        this.commandRigidbody = commandRigidbody;
    }

    public Rigidbody GetCommandRigidbody()
    {
        return this.commandRigidbody;
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

public class AddForce : RigidbodyCommands
{
    private Vector3 forceDir;
    private Vector3? forceUndoDir;
    private Vector3 commandPosition;
    private Quaternion commandRotation;
    private Vector3 commandVelocity;
    private Vector3 commandAngularVelocity;


    public AddForce(Rigidbody commandRigidbody, Vector3 forceDir) : base(commandRigidbody)
    {
        this.forceDir = forceDir;
        this.forceUndoDir = -forceDir;
        this.commandPosition = commandRigidbody.position;
        this.commandRotation = commandRigidbody.rotation;
        this.commandVelocity = commandRigidbody.velocity;
        this.commandAngularVelocity = commandRigidbody.angularVelocity;
    }

    public override void Execute()
    {
        commandRigidbody.AddForce(forceDir);
    }

    public override void Undo()
    {
        if (forceUndoDir.HasValue)
        {
            //UndoDynamic();
            UndoStatic();
        }
        else
        {
            UndoStatic();
        }
    }

    private void UndoDynamic()
    {
        commandRigidbody.AddForce(forceUndoDir.Value);
    }

    private void UndoStatic()
    {
        if (GetUndoPrecise(commandRigidbody.position, commandPosition, commandRigidbody.rotation.eulerAngles, commandRotation.eulerAngles, commandRigidbody.velocity, commandVelocity, commandRigidbody.angularVelocity, commandAngularVelocity))
        {
            commandRigidbody.velocity = commandVelocity;
            commandRigidbody.angularVelocity = commandAngularVelocity;
            commandRigidbody.position = commandPosition;
            commandRigidbody.rotation = commandRotation;
        }
    }
}