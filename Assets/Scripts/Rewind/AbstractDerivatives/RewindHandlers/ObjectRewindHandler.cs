using UnityEngine;

public class ObjectRewindHandler : RewindHandler
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
        RegisterHandler();
    }

    private void RegisterHandler()
    {
        RewindManager rewindManager = RewindManager.instance;
        if (!rewindManager)
        {
            Debug.LogError("CANT FIND REWIND MANAGER !");
            return;
        }
        rewindManager.RegisterRewindHandler(this);
    }

    protected override void Execute()
    {
        objectRigidbody.Sleep();
        
        if (!commandGroups[currentCommandGroup].IsEmpty())
        {
            Command lastCommand = commandGroups[currentCommandGroup].GetLastCommand();
            lastCommand.Undo();
            commandGroups[currentCommandGroup].Remove(lastCommand);
        }
        else
        {
            commandGroups.RemoveAt(currentCommandGroup);    
            currentCommandGroup--;
        }

        if(currentCommandGroup < 0)
        {
            Reset();
            return;
        }
    }

    private void Reset()
    {
        if (currentCommandGroup < 0)
        {
            IncreaseGroup();
        }
        ResetFlags();
    }

    private void ResetFlags()
    {
        Debug.Log("REWIND RESETTING");
        objectRigidbody.WakeUp();
        complete = true;
        cancelRewind = false;
        rewindRequired = false;
        canRewind = true;
    }

    public override void Update()
    {
        base.Update();
        if (CancelRequested())
        {
            Reset();
        }
    }

    public override void FixedUpdate()
    {
        if (rewindRequired)
        {
            Execute();
        }
    }

    public override void LateUpdate()
    {
        
    }

}