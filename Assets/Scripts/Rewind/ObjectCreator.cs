using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public int objCount;
    public int objLimit;
    public enum UPDATE_TYPE
    {
        UPDATE,
        FIXEDUPDATE
    }
    public List<GameObject> prefabs = new List<GameObject>();
    RewindHandler rewindHandler;
    RewindManager rewindManager;
    [SerializeField] private UPDATE_TYPE updateType;
    private void Start()
    {
        rewindManager = RewindManager.instance;
        rewindHandler = gameObject.GetComponent<RewindHandler>();
    }

    void Update()
    {
        if (!rewindHandler.complete)
        {
            return;
        }
        if (updateType == UPDATE_TYPE.UPDATE)
        {
            if (Input.GetKey(KeyCode.Space) && (objCount < objLimit ||objLimit == -1))
            {
                rewindHandler.AddCommand(new Instantiate(gameObject.name, prefabs[0], Vector3.zero, Quaternion.identity, null), true);
                objCount++;
            }
            else
            {
                rewindHandler.AddCommand(new Idle(gameObject.name), false);
            }
        }

    }
    private void FixedUpdate()
    {
        if (!rewindHandler.complete)
        {
            return;
        }
        if (updateType == UPDATE_TYPE.FIXEDUPDATE)
        {
            if (Input.GetKey(KeyCode.Space) && (objCount < objLimit ||objLimit == -1))
            {
                rewindHandler.AddCommand(new Instantiate(gameObject.name, prefabs[0], Vector3.zero, Quaternion.identity, null), true);
                objCount++;
            }
            else
            {
                rewindHandler.AddCommand(new Idle(gameObject.name), false);
            }
        }

    }

}
