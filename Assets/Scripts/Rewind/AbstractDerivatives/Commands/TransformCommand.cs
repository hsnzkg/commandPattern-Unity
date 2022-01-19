using UnityEngine;

[System.Serializable]
public abstract class TransformCommand : Command {
    [SerializeField]protected Transform obj;

    public TransformCommand(Transform obj = null)
    {
        this.obj = obj;
    }
}