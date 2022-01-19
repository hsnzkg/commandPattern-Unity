using UnityEngine;

[System.Serializable]
public abstract class TransformCommand : Command {
    [SerializeField]protected Transform commandTransform;

    public TransformCommand(Transform commandTransform = null) : base(commandTransform.gameObject)
    {
        this.commandTransform = commandTransform;
    }

    public Transform GetCommandTransform(){
        return commandTransform;
    }
}