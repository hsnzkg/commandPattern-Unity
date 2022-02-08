using System.Data;
using UnityEngine;

public abstract class MonoBehaviourCommands : Command 
{
    public MonoBehaviourCommands(string obj) :base(obj)
    {
    }
}


public class Instantiate : MonoBehaviourCommands
{
    private GameObject objRef;
    private GameObject objPrefab;
    private Vector3 pos;
    private Quaternion rot;
    private Transform parent;

    public Instantiate(string obj,GameObject objPrefab,Vector3 pos,Quaternion rot,Transform parent = null) : base(obj)
    {
        this.objPrefab = objPrefab;
        this.pos = pos;
        this.rot = rot;
        this.parent = parent;
    }

    public override void Execute()
    {
        this.objRef = MonoBehaviour.Instantiate(objPrefab,pos,rot,parent);
    }

    public override void Undo()
    {
        MonoBehaviour.DestroyImmediate(this.objRef);
    }
}

public class SetActive : MonoBehaviourCommands
{
    private GameObject target;
    private bool value;
    private bool? undoValue;
    public SetActive(string obj,GameObject target,bool value) : base(obj)
    {
        this.target = target;
        this.value = value;
        this.undoValue = !value;
    }

    public override void Execute()
    {
        target.SetActive(value);
    }

    public override void Undo()
    {
        if(undoValue.HasValue)
        {
            target.SetActive(undoValue.Value);
        }
    }
}