using UnityEngine;

[System.Serializable]
public class TransformCommand : Command
{
    [SerializeField] protected Transform commandTransform;

    #region CONSTRUCTORS
    public TransformCommand(Transform commandTransform = null) : base(commandTransform.gameObject)
    {
        this.commandTransform = commandTransform;
    }

    public TransformCommand(Transform commandTransform, Vector3 dir) : base(commandTransform.gameObject)
    {
        Debug.Log("New movement command");
    }

    public TransformCommand(Transform commandTransform, Quaternion dir)
    {
        Debug.Log("New rotation command");
    }

    #endregion

    #region VARIABLES

    private Vector3 moveDir;
    private Vector3? moveUndoDir;
    private Quaternion rotDir;
    private Quaternion? rotUndoDir;
    private Vector3 commandPosition;
    private float TRANSFORM_THRESHOLD = Mathf.Epsilon;

    #endregion




    #region  IMPLEMENTATIONS
    
    public Transform GetCommandTransform()
    {
        return commandTransform;
    }

    private void Rotate()
    {

    }
    
    private void Move()
    {

    }

    #endregion

    #region OVERRIDES
    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Undo()
    {

    }
    #endregion

}