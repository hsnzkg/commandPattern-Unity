using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour, IKillable
{
    private ObjectRewindHandler objectRewindHandler;
    private bool Dead = false;
    public bool dead
    {
        get
        {
            return dead;
        }
        set
        {
            dead = value;
            if (dead)
            {
                onDeadRewindInvoked = false;
            }
        }
    }
    private bool onDeadRewindInvoked = false;
    private RewindManager rewindManager;
    private void Start()
    {
        objectRewindHandler = GetComponent<ObjectRewindHandler>();
        rewindManager = GameObject.FindObjectOfType<RewindManager>();
    }
    private void Update()
    {
        if (!objectRewindHandler.complete)
        {
            return;
        }
        if(Dead){
            if(!onDeadRewindInvoked){
                rewindManager.PauseRewindAbility();
                rewindManager.ExecuteInSeconds(0.4f);
                rewindManager.CancelInSeconds(2.5f);
                onDeadRewindInvoked = true;
            }
        }
    }
}