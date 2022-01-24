using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Object : MonoBehaviour, IKillable
{
    private RewindManager rewindManager;
    private ObjectRewindHandler objectRewindHandler;
    private bool Dead = false;
    private bool onDeadRewindInvoked = false;
    public bool dead
    {
        get
        {
            return Dead;
        }
        set
        {
            Dead = value;
            if (Dead)
            {
                onDeadRewindInvoked = false;
            }
        }
    }
    private void Move()
    {
        Vector3 amountToMove = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        objectRewindHandler.AddCommand(new TransformCommand(transform,(amountToMove * Time.deltaTime)),true);
    }
    private void Rotate()
    {
        Vector3 amountToRotate = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        objectRewindHandler.AddCommand(new TransformCommand(transform,(Quaternion.Euler(amountToRotate * 100f * Time.deltaTime))),true);
    }
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
        if (Dead)
        {
            if (!onDeadRewindInvoked)
            {
                onDeadRewindInvoked = true;
                rewindManager.PauseRewindAbility();
                rewindManager.ExecuteInSeconds(0.4f);
                rewindManager.CancelInSeconds(2.5f);
            }
            objectRewindHandler.AddCommand(new ObjectDeadCommand(transform, this), true);
            return;
        }
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Move();
            Rotate();
        }


    }

}