using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Object : MonoBehaviour, IKillable
{
    public enum UPDATE_TYPE
    {
        UPDATE,
        FIXEDUPDATE
    }
    private RewindManager rewindManager;
    private ObjectRewindHandler objectRewindHandler;
    private bool Dead = false;
    private bool onDeadRewindInvoked = false;
    [SerializeField]private UPDATE_TYPE updateType;
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
    private void TransformMove()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 finalDir = moveDir * 1000f * Time.deltaTime;
        objectRewindHandler.AddCommand(new Translate(transform, finalDir), true);
    }
    private void TransformRotate()
    {
        Vector3 rotateDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Quaternion finalDir = Quaternion.Euler(rotateDir * 100f * Time.deltaTime);
        objectRewindHandler.AddCommand(new Rotate(transform, finalDir), true);
    }

    private void RigidbodyMove()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 finalDir = moveDir * 1000f * Time.deltaTime;
        objectRewindHandler.AddCommand(new AddForce(gameObject.GetComponent<Rigidbody>(), finalDir), true);
    }

    private void RigidbodyRotate()
    {

    }

    private void MovementLoop()
    {
        //TransformMove();
        //TransformRotate();
        RigidbodyMove();
        RigidbodyRotate();
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
            //objectRewindHandler.AddCommand(new ObjectDeadCommand(transform, this), true);
            return;
        }
        if (updateType == UPDATE_TYPE.UPDATE)
        {
            MovementLoop();
        }
    }
    private void FixedUpdate()
    {
        if (!objectRewindHandler.complete)
        {
            return;
        }
        if (updateType == UPDATE_TYPE.FIXEDUPDATE)
        {
            MovementLoop();
        }
    }
}