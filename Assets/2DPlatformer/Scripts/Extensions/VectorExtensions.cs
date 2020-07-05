using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions 
{
    
    public static void SetX(this ref Vector3 vec, float x)
    {
        vec = new Vector3(x, vec.y, vec.z);
    }
    
    public static void SetY(this ref Vector3 vec, float y)
    {
        vec = new Vector3(vec.x, y, vec.z);
    }

    public static void SetZ(this ref Vector3 vec , float z)
    {
        vec = new Vector3(vec.x, vec.y, z);
    }

}

