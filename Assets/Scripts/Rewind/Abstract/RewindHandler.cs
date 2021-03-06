using System.ComponentModel;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class provides bass functionality for a concrete rewind class. Stores a list
/// of commandGroups (i.e. movement/attack). When player presses rewind button, the execute method
/// is invoked each time step.
/// </summary>

public abstract class RewindHandler : MonoBehaviour
{
    [SerializeField] private bool DEBUG = false;
    [SerializeField] protected CommandGroup commandGroup;


    // HISTORY
    protected Stack<byte[]> commandDatas = new Stack<byte[]>();
    protected Stack<CommandGroup> commandGroups = new Stack<CommandGroup>();

    [Header("REWIND SETTINGS")]
    [SerializeField] public RewindSettings rewindSetting;

    [Header("REWIND CONTROLLERS")]
    [SerializeField] private bool rewindComplete = true;
    [SerializeField] public bool complete { get { return rewindComplete; } set { rewindComplete = value; } }
    [SerializeField] public bool rewindRequired { get; set; }
    [SerializeField] protected bool cancelRewind;
    [SerializeField] protected bool canRewind = true;

    [Header("UPDATE SETTINGS")]
    [SerializeField] protected RewindEnums.UPDATE_TYPE updateType = RewindEnums.UPDATE_TYPE.UPDATE;

    #region OWN-IMPLEMENTATIONS
    public void AddCommand(Command command, bool executeCommand)
    {
        if (!complete)
        {
            if (DEBUG)
            {
                Debug.LogError(command.GetType() + ": ATTEMPT TO ADD NEW COMMAND WHILE REWIND EXECUTE ALL BEHAIVOURS SHOULD BE PAUSE WHILE REWIND");
            }
            return;
        }

        if (commandGroup.IsFull())
        {
            if (DEBUG)
            {
                Debug.LogWarning("COMMAND GROUP IS FULL CREATING NEW GROUP");
            }
            IncreaseGroup();
        }

        if (DEBUG)
        {
            Debug.Log(command.GetCommandObject().name + ": ATTEMPT TO ADD " + command.GetType().Name);
        }
        commandGroup.Add(command);
        if (executeCommand)
        {
            command.Execute();
        }
    }

    protected void IncreaseGroup()
    {
        if (rewindSetting.isCompressing)
        {
            commandDatas.Push(DataCompresser.Compress(commandGroup));
        }
        else
        {
            commandGroups.Push(commandGroup);
   
        }
        commandGroup.ClearCommands();
    }

    protected bool RewindRequested()
    {
        bool shouldRewind = Input.GetKeyDown(KeyCode.LeftShift) && complete && canRewind;
        if (shouldRewind)
        {
            RewindManager.instance.RewindStarted();
        }
        return shouldRewind;
    }

    protected bool CancelRequested()
    {
        bool cancel = Input.GetKeyUp(KeyCode.LeftShift) && canRewind || cancelRewind;
        if (cancel)
        {
            RewindManager.instance.RewindStopped();
        }
        return cancel;
    }

    public void CancelRewind()
    {
        if (rewindRequired && !complete)
        {
            cancelRewind = true;
        }
    }

    public void RequestRewind()
    {
        rewindRequired = true;
    }

    public void PauseRewindAbility()
    {
        canRewind = false;
    }

    public void RewindLoop()
    {
        if (RewindRequested())
        {
            rewindRequired = true;
        }
        if (rewindRequired)
        {
            complete = false;
        }
    }

    protected virtual void Reset()
    {
        ResetFlags();
    }

    protected virtual void ResetFlags()
    {
        if (DEBUG)
        {
            Debug.Log("REWIND RESETTING");
        }

        complete = true;
        cancelRewind = false;
        rewindRequired = false;
        canRewind = true;
    }

    protected virtual void RegisterHandler()
    {
        RewindManager rewindManager = RewindManager.instance;
        if (!rewindManager)
        {
            if (DEBUG)
            {
                Debug.LogWarning("CANT FIND REWIND MANAGER !");

            }
            return;
        }
        rewindManager.RegisterRewindHandler(this);
    }

    protected virtual void UnRegisterHandler()
    {
        RewindManager rewindManager = RewindManager.instance;
        if (!rewindManager)
        {
            if (DEBUG)
            {
                Debug.LogWarning("CANT FIND REWIND MANAGER !");

            }
            return;
        }
        rewindManager.UnRegisterRewindHandler(this);
    }


    protected virtual void Execute()
    {
        if (!commandGroup.IsEmpty())
        {
            commandGroup.GetLastCommand().Undo();
        }
        else
        {
            if (rewindSetting.isCompressing)
            {
                if (commandDatas.Count > 0)
                {
                    commandGroup = DataCompresser.DeCompress(commandDatas.Pop());
                }
            }
            else
            {
                if (commandGroups.Count > 0)
                {
                    commandGroup = commandGroups.Pop();
                }
            }
        }

        if (rewindSetting.isCompressing)
        {
            if (commandGroup.IsEmpty() && commandDatas.Count <= 0)
            {
                Reset();
                return;
            }
        }
        else
        {
            if (commandGroup.IsEmpty() && commandGroups.Count <= 0)
            {
                Reset();
                return;
            }
        }

    }
    #endregion

    #region UNITY-IMPLEMENTATIONS


    public virtual void Awake()
    {
        this.gameObject.name = this.gameObject.name + this.gameObject.GetInstanceID().ToString();
        commandGroup = new CommandGroup(rewindSetting.maxFrameCount);
        RegisterHandler();
    }

    public virtual void Start()
    {
   
    }

    private void OnDestroy()
    {
        UnRegisterHandler();
    }

    public virtual void Update()
    {
        RewindLoop();
        if (CancelRequested())
        {
            Reset();
        }

        if (rewindRequired && updateType == RewindEnums.UPDATE_TYPE.UPDATE)
        {
            Execute();
        }
    }

    public virtual void FixedUpdate()
    {
        if (rewindRequired && updateType == RewindEnums.UPDATE_TYPE.FIXEDUPDATE)
        {
            Execute();
        }
    }
    #endregion
}