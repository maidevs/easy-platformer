using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollider
{

    private Collider2D _collider;
    private LayerMask _groundLayer;

    //These values are to make the collisions more "forgivables"
    private readonly float COLLIDER_SIZE_OVERFLOW = 0.2f;
    private readonly Vector3 HORIZONTAL_OVERFLOW = Vector3.right * 0.1f; 

    public CharacterCollider(Collider2D collider, LayerMask groundLayer)
    {
        _collider = collider;
        _groundLayer = groundLayer;
    }
    
    private RaycastHit2D GetAxisCollision(Vector3 raycastDirection)
    {
        RaycastHit2D centralRaycast;
        RaycastHit2D bottomLimitRaycast;
        RaycastHit2D topLimitRaycast;
        
        if (raycastDirection.y != 0)
        {
            float raycastDistance = _collider.bounds.extents.y + COLLIDER_SIZE_OVERFLOW;

            centralRaycast = Physics2D.Raycast(_collider.Center(), raycastDirection, raycastDistance, _groundLayer);
            bottomLimitRaycast = Physics2D.Raycast(_collider.Left(), raycastDirection, raycastDistance, _groundLayer);
            topLimitRaycast = Physics2D.Raycast(_collider.Right(), raycastDirection, raycastDistance, _groundLayer);
        }
        else
        {
            float raycastDistance = _collider.bounds.extents.x + COLLIDER_SIZE_OVERFLOW;

            centralRaycast = Physics2D.Raycast(_collider.Center(), raycastDirection, raycastDistance, _groundLayer);
            bottomLimitRaycast = Physics2D.Raycast(_collider.BottomCenter() + HORIZONTAL_OVERFLOW, raycastDirection, raycastDistance, _groundLayer);
            topLimitRaycast = Physics2D.Raycast(_collider.TopCenter() - HORIZONTAL_OVERFLOW, raycastDirection, raycastDistance, _groundLayer);
        }

        if (centralRaycast.collider != null) { return centralRaycast; }
        else if (bottomLimitRaycast.collider != null) { return bottomLimitRaycast; }
        else if (topLimitRaycast.collider != null) { return topLimitRaycast; }
       
        return new RaycastHit2D();
    }

    public RaycastHit2D GetHorizontalCollision(Vector3 direction)
    {
        return GetAxisCollision(Vector3.right * direction.x);
    }

    public RaycastHit2D GetVerticalCollision(Vector3 direction)
    {
        return GetAxisCollision(Vector3.up * direction.y);
    }
    
    public bool IsGrounded()
    {
        return GetVerticalCollision(-_collider.transform.up).collider != null;
    }
   
}
