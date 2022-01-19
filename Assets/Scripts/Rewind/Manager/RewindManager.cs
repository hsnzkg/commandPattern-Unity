using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewindManager : MonoBehaviour
{

    [SerializeField]private List<RewindHandler> rewindHandlers = new List<RewindHandler>();
    private static RewindManager _instance;
    public static RewindManager instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<RewindManager>();
            }
            return _instance;
        }
    }
    public void RewindStarted()
    {
       Debug.Log("REWIND - STARTED ! ");
    }

    public void RewindStopped()
    {
       Debug.Log("REWIND - STOPPED ! ");
    }


    public void PauseRewindAbility()
    {
        foreach (var rewind in rewindHandlers)
        {
            rewind.PauseRewindAbility();
        }
    }

    public void ExecuteInSeconds(float seconds)
    {
        Invoke("Execute", seconds);
    }

    public void Execute()
    {
        RewindStarted();
        foreach (var rewind in rewindHandlers)
        {
            rewind.RequestRewind();
        }
    }

    public void CancelInSeconds(float seconds)
    {
        Invoke("Cancel", seconds);
    }

    public void Cancel()
    {
        RewindStopped();
        foreach (var rewind in rewindHandlers)
        {
            rewind.CancelRewind();
        }
    }

    public void RegisterRewindHandler(ObjectRewindHandler rewind)
    {
        rewindHandlers.Add(rewind);
    }

}