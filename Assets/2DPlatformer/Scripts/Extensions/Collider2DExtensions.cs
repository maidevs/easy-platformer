using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collider2DExtensions 
{
    
    public static Vector3 BottomLeft(this Collider2D collider)
    {
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;
        
        return center - extents;
    }

    public static Vector3 BottomCenter(this Collider2D collider)
    {
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;

        return center - (Vector3.up * extents.y);
    }

    public static Vector3 BottomRight(this Collider2D collider)
    {
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;

        return center + new Vector3(extents.x, -extents.y);
    }

    public static Vector3 TopRight(this Collider2D collider)
    {
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;

        return center + extents;
    }

    public static Vector3 TopCenter(this Collider2D collider)
    {
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;

        return center + (Vector3.up * extents.y);
    }

    public static Vector3 TopLeft(this Collider2D collider)
    {
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;

        return center + new Vector3(-extents.x, extents.y);
    }

    public static Vector3 Right(this Collider2D collider)
    {
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;

        return center + (Vector3.right * extents.x);
    }

    public static Vector3 Left(this Collider2D collider)
    {
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;

        return center - (Vector3.right * (extents.x));
    }

    public static Vector3 Center(this Collider2D collider)
    {
        return collider.bounds.center;
    }


}
