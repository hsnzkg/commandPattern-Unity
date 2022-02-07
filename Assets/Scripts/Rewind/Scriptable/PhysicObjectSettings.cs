using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhysicSetting", menuName = "ScriptableObjects/PhysicSetting/PhysicObjectSettings", order = 1)]
public class PhysicObjectSettings : ScriptableObject
{
    public float movementSpeed = 1000f;
}
