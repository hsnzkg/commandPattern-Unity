using System.Numerics;
using System;
using UnityEngine;

[Serializable]
public class SQuaternion
{
    private float x;
    private float y;
    private float z;
    private float w;

    public SQuaternion()
    {
        this.x = 0f;
        this.y = 0f;
        this.z = 0f;
        this.w = 0f;
    }

    public SQuaternion(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public SQuaternion(UnityEngine.Quaternion quaternion)
    {
        this.x = quaternion.x;
        this.y = quaternion.y;
        this.z = quaternion.z;
        this.w = quaternion.w;
    }



    public UnityEngine.Quaternion Quaternion
    {
        get
        {
            return new UnityEngine.Quaternion(x, y, z,w);
        }
        set
        {
            this.x = value.x;
            this.y = value.y;
            this.z = value.z;
            this.w = value.w;
        }
    }

}
