using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Abstract class provides bass functionality for a concrete rewind class. Stores a list
/// of commandGroups (i.e. movement/attack). When player presses rewind button, the execute method
/// is invoked each time step.
/// </summary>

public abstract class RewindHandler : MonoBehaviour
{
    #region OWN-IMPLEMENTATIONS

    [Header("GROUP SETTINGS")]
    [SerializeField] protected List<CommandGroup> commandGroups = new List<CommandGroup>();
    [Header("REWIND SETTINGS")]
    [SerializeField] private RewindSettings rewindSetting;
    protected int currentCommandGroup = -1;

    [Header("REWIND CONTROLLERS")]
    [SerializeField] private bool rewindComplete = true;
    [SerializeField] public bool complete { get { return rewindComplete; } set { rewindComplete = value; } }
    [SerializeField] public bool rewindRequired { get; set; }
    [SerializeField] protected bool cancelRewind;
    [SerializeField] protected bool canRewind = true;

    [Header("UPDATE SETTINGS")]
    [SerializeField] protected UPDATE_TYPE updateType = UPDATE_TYPE.UPDATE;
    private static readonly string SCRIPT_NAME = typeof(RewindHandler).Name;

    public enum UPDATE_TYPE
    {
        UPDATE,
        FIXEDUPDATE,
    }

    public virtual void Awake()
    {
        IncreaseGroup();
    }

    public virtual void Start()
    {

    }

    public void AddCommand(Command command, bool executeCommand)
    {
        if (!complete)
        {
            Debug.LogError(command.GetType() + ": ATTEMPT TO ADD NEW COMMAND WHILE REWIND EXECUTE ALL BEHAIVOURS SHOULD BE PAUSE WHILE REWIND");
            return;
        }

        if (commandGroups[currentCommandGroup].IsFull())
        {
            Debug.LogWarning("COMMAND GROUP IS FULL CREATING NEW GROUP");
            IncreaseGroup();
        }

        Debug.Log(command.GetCommandObject().name + ": ATTEMPT TO ADD " + command.GetType().Name);
        commandGroups[currentCommandGroup].Add(command);
        if (executeCommand)
        {
            command.Execute();
        }
    }

    protected void IncreaseGroup()
    {
        currentCommandGroup++;
        commandGroups.Add(new CommandGroup(rewindSetting.maxTimeStep));
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

    protected abstract void Execute();
    #endregion

    #region UNITY-IMPLEMENTATIONS
    public virtual void Update()
    {
        RewindLoop();
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void LateUpdate()
    {

    }
    #endregion
}