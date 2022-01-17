using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class provides bass functionality for a concrete rewind class. Stores a list
/// of commands (i.e. movement/attack). When player presses rewind button, the execute method
/// is invoked each time step.
/// </summary>

public abstract class RewindHandler : MonoBehaviour 
{
    #region OWN-IMPLEMENTATIONS
    public RewindSettings rewindSetting;
    private bool rewindComplete = true;
    public bool complete {get {return rewindComplete;} set {rewindComplete = value;}}
    protected List<CommandGroup> commands = new List<CommandGroup>();
    public bool rewindRequired {get; set;}
    protected bool cancelRewind;
    protected int currentGroup = -1;
    protected bool canRewind = true;
    private static readonly string SCRIPT_NAME =typeof(RewindHandler).Name;
    public virtual void Start()
    {
        IncreaseGroup();
    }

    public void AddCommand(Command command, bool executeCommand)
    {
        if(!complete){
            Debug.LogError(SCRIPT_NAME + ": ATTEMPT TO ADD NEW COMMAND WHILE REWIND EXECUTE ALL BEGAIVOURS SHOULD BE PAUSE WHILE REWIND");
            return;
        }
        commands[currentGroup].Add(command);
        if(executeCommand){
            command.Execute();
        }
    }

    private void IncreaseGroup()
    {
        currentGroup++;
        commands.Add(new CommandGroup());
        if(commands.Count > rewindSetting.maxTimeStep){
            commands.RemoveAt(0);
            currentGroup = rewindSetting.maxTimeStep - 1;
        }      
    }

    protected bool RewindRequested(){
        bool shouldRewind = Input.GetKeyDown(KeyCode.LeftShift) && complete && canRewind;
        if(shouldRewind){
            RewindManager.instance.RewindStarted();
        }
        return shouldRewind;
    }

    protected bool CancelRequested(){
        bool cancel = Input.GetKeyUp(KeyCode.LeftShift) && canRewind && cancelRewind;
        if(cancel){
            RewindManager.instance.RewindStopped();
        }
        return cancel;
    }


    public void CancelRewind()
    {
        if(rewindRequired && !complete){
            cancelRewind = true;
        }
    }

    public void RequestRewind(){
        rewindRequired = true;
    }

    public void PauseRewindAbility(){
        canRewind  = false;
    }

    protected abstract void Execute();
    #endregion
    


 
    #region UNITY-IMPLEMENTATIONS
    private void LateUpdate() {
        if(!rewindRequired && complete)
        {
            IncreaseGroup();
        }
    }

    private void Update() {
        if(rewindRequired){
            complete = false;
            Execute();
        }
        if(RewindRequested()){
            rewindRequired = true;
        }
    }
    #endregion



}