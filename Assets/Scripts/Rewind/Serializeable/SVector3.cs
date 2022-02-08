using System;
using UnityEngine;

[Serializable]
public class SVector3
{
    private float x;
    private float y;
    private float z;

    public SVector3()
    {
        this.x = 0f;
        this.y = 0f;
        this.z = 0f;
    }

    public SVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public SVector3(Vector3 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }

    public Vector3 Vector3
    {
        get
        {
            return new Vector3(x, y, z);
        }
        set
        {
            this.x = value.x;
            this.y = value.y;
            this.z = value.z;
        }
    }

}
