using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewindSetting", menuName = "ScriptableObjects/Rewind/RewindSettings", order = 1)]
public class RewindSettings : ScriptableObject
{
    public int maxFrameCount = 60;
    public bool isCompressing = false;
}
