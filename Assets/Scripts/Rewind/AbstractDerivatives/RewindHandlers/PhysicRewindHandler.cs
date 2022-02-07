using UnityEngine;

public class PhysicRewindHandler : RewindHandler
{
    private Rigidbody objectRigidbody;

    public override void Awake()
    {
        base.Awake();
        objectRigidbody = GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        base.Start();
    }

    protected override void RegisterHandler()
    {
        base.RegisterHandler();
    }

    protected override void Execute()
    {
        objectRigidbody.Sleep();
        base.Execute();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void ResetFlags()
    {
        objectRigidbody.WakeUp();
        base.ResetFlags();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}