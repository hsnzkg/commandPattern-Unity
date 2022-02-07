using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PhysicObject : MonoBehaviour, IKillable
{
    public enum UPDATE_TYPE
    {
        UPDATE,
        FIXEDUPDATE
    }
    private Rigidbody rb;
    private RewindManager rewindManager;
    private PhysicRewindHandler objectRewindHandler;
    private bool Dead = false;
    private bool onDeadRewindInvoked = false;
    [SerializeField] private PhysicObjectSettings physicSettings;
    [SerializeField] private UPDATE_TYPE updateType;
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
        Vector3 finalDir = moveDir * physicSettings.movementSpeed * Time.deltaTime;
        objectRewindHandler.AddCommand(new AddForce(rb, finalDir), true);
    }
    private bool IsTryingToMove(){
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            return true;
        }
        return false;
    }

    private void RigidbodyRotate()
    {

    }

    private void RigidbodyIdle()
    {
        objectRewindHandler.AddCommand(new Idle(this.gameObject), false);
    }

    private void MovementLoop()
    {
        if (rb.IsSleeping())
        {
            RigidbodyIdle();
        }
        if(IsTryingToMove() || !rb.IsSleeping())
        {
            RigidbodyMove();
            //RigidbodyRotate();
            //TransformMove();
            //TransformRotate();
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        objectRewindHandler = GetComponent<PhysicRewindHandler>();
    }

    private void Start()
    {       
        rewindManager = RewindManager.instance;
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