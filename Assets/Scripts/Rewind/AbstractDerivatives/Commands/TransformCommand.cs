using UnityEngine;

[System.Serializable]
public class TransformCommand : Command
{
    [SerializeField] protected Transform commandTransform;

    #region CONSTRUCTORS
    public TransformCommand(Transform commandTransform = null) : base(commandTransform.gameObject)
    {
        this.commandTransform = commandTransform;
        this.COMMAND_TYPE = TRANSFORM_COMMAND_TYPE.NULL;
    }

    public TransformCommand(Transform commandTransform, Vector3 dir) : base(commandTransform.gameObject)
    {
        this.commandTransform = commandTransform;
        this.COMMAND_TYPE = TRANSFORM_COMMAND_TYPE.MOVE;
        moveDir = dir;
        moveUndoDir = -dir;
        commandPosition = commandTransform.localPosition;
    }

    public TransformCommand(Transform commandTransform, Quaternion dir) : base(commandTransform.gameObject)
    {
        this.commandTransform = commandTransform;
        this.COMMAND_TYPE = TRANSFORM_COMMAND_TYPE.ROTATE;
        rotateDir = Quaternion.Euler((dir.eulerAngles));
        rotateUndoDir = Quaternion.Euler(-(dir.eulerAngles));
        commandRotation = commandTransform.localRotation;
    }

    #endregion

    #region VARIABLES

    private Vector3 moveDir;
    private Vector3 moveUndoDir;
    private Quaternion rotateDir;
    private Quaternion rotateUndoDir;
    private Vector3 commandPosition;
    private Quaternion commandRotation;
    private float TRANSFORM_THRESHOLD = Mathf.Epsilon;
    private TRANSFORM_COMMAND_TYPE COMMAND_TYPE = TRANSFORM_COMMAND_TYPE.NULL;

    public enum TRANSFORM_COMMAND_TYPE
    {
        NULL,
        MOVE,
        ROTATE
    }
    #endregion




    #region  IMPLEMENTATIONS

    public Transform GetCommandTransform()
    {
        return commandTransform;
    }

    private void Rotate()
    {
        commandTransform.Rotate(rotateDir.eulerAngles, Space.World);
    }
    private void RotateUndo()
    {
        commandTransform.Rotate(rotateUndoDir.eulerAngles, Space.World);
        if (GetUndoPrecise(commandTransform.rotation.eulerAngles,commandRotation.eulerAngles))
        {
            commandTransform.localRotation = commandRotation;
        }
    }

    private void Move()
    {
        commandTransform.Translate(moveDir, Space.World);
    }

    private void MoveUndo()
    {
        commandTransform.Translate(moveUndoDir, Space.World);
        if (GetUndoPrecise(commandTransform.localPosition, commandPosition))
        {
            commandTransform.localPosition = commandPosition;
        }
    }

    private bool GetUndoPrecise(Vector3 pos, Vector3 targetPos)
    {
        return Vector3.Distance(pos, targetPos) > TRANSFORM_THRESHOLD ? true : false;
    }
    #endregion

    #region OVERRIDES
    public override void Execute()
    {
        switch (COMMAND_TYPE)
        {
            case TRANSFORM_COMMAND_TYPE.NULL:
                Debug.Log("NULL");
                break;

            case TRANSFORM_COMMAND_TYPE.MOVE:
                Debug.Log("MOVE");
                Move();
                break;

            case TRANSFORM_COMMAND_TYPE.ROTATE:
                Debug.Log("ROTATE");
                Rotate();
                break;
        }
    }

    public override void Undo()
    {
        switch (COMMAND_TYPE)
        {
            case TRANSFORM_COMMAND_TYPE.NULL:
                break;

            case TRANSFORM_COMMAND_TYPE.MOVE:
                MoveUndo();
                break;

            case TRANSFORM_COMMAND_TYPE.ROTATE:
                RotateUndo();
                break;
        }
    }
    #endregion

}