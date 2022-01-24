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
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 finalDir = moveDir * Time.deltaTime;
        objectRewindHandler.AddCommand(new Translate(transform,finalDir),true);
    }
    private void Rotate()
    {
        Vector3 rotateDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Quaternion finalDir = Quaternion.Euler( rotateDir * 100f * Time.deltaTime);
        objectRewindHandler.AddCommand(new Rotate(transform,finalDir),true);
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