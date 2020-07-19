using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollider
{
    private Collider2D _collider;
    private LayerMask _groundLayer;

    //These values are to make the collisions more "forgivables"
    private readonly int RAYCAST_GRANULARITY = 8; //Number of raycasts in each axis;
    private readonly float COLLIDER_SIZE_OVERFLOW = 0.02f; //Overflow of the raycas (to go a little bit further than the collider)
    private readonly Vector3 VERTICAL_OVERFLOW = Vector3.up * 0.05f; //Vertical collider padding
    private readonly Vector3 HORIZONTAL_OVERFLOW = Vector3.right * 0.05f; //Horizontal collider padding

    public Vector2 Extents
    {
        get { return _collider.bounds.extents; }
    }

    public Vector3 Offset
    {
        get { return _collider.offset; }
    }

    public CharacterCollider(Collider2D collider, LayerMask groundLayer)
    {
        _collider = collider;
        _groundLayer = groundLayer;
    }


    public RaycastHit2D GetHorizontalCollision(Vector3 direction)
    {
        direction = Vector3.right * direction.x;

        var offset = (_collider.bounds.size.y - VERTICAL_OVERFLOW.y) / RAYCAST_GRANULARITY;
        var raycastOrigin = _collider.Bottom() + VERTICAL_OVERFLOW;
        var raycastDistance = _collider.bounds.extents.x + COLLIDER_SIZE_OVERFLOW;

        for (int i = 0; i < RAYCAST_GRANULARITY; i++)
        {
            var raycast = Physics2D.Raycast(raycastOrigin + (Vector3.up * offset * i), direction, raycastDistance, _groundLayer);
            if (raycast.collider != null) { return raycast; }
        }

        return new RaycastHit2D();
    }

    public RaycastHit2D GetVerticalCollision(Vector3 direction)
    {
        direction = Vector3.up * direction.y;

        var offset = (_collider.bounds.size.x - HORIZONTAL_OVERFLOW.x) / RAYCAST_GRANULARITY;

        var raycastOrigin = _collider.Left() + HORIZONTAL_OVERFLOW;
        var raycastDistance = _collider.bounds.extents.y + COLLIDER_SIZE_OVERFLOW + 0.2f;

        for (int i = 0; i < RAYCAST_GRANULARITY; i++)
        {
            var raycast = Physics2D.Raycast(raycastOrigin + (Vector3.right * offset * i), direction, raycastDistance, _groundLayer);
            if (raycast.collider != null) { return raycast; }
        }

        return new RaycastHit2D();
    }

    public RaycastHit2D GetHorizontalTopCollision(Vector3 direction)
    {
        float raycastDistance = _collider.bounds.extents.x + COLLIDER_SIZE_OVERFLOW;
        return Physics2D.Raycast(_collider.Top() - VERTICAL_OVERFLOW, direction, raycastDistance, _groundLayer);
    }

    public RaycastHit2D GetHorizontalMiddleCollision(Vector3 direction)
    {
        float raycastDistance = _collider.bounds.extents.x + COLLIDER_SIZE_OVERFLOW;
        return Physics2D.Raycast(_collider.Center(), direction, raycastDistance, _groundLayer);
    }

    public RaycastHit2D GetHorizontalBottomCollision(Vector3 direction)
    {
        float raycastDistance = _collider.bounds.extents.x + COLLIDER_SIZE_OVERFLOW;
        return Physics2D.Raycast(_collider.Top() + VERTICAL_OVERFLOW, direction, raycastDistance, _groundLayer);
    }

    public RaycastHit2D GetVerticalLeftCollision(Vector3 direction)
    {
        float raycastDistance = _collider.bounds.extents.y + COLLIDER_SIZE_OVERFLOW;
        return Physics2D.Raycast(_collider.Left(), direction, raycastDistance, _groundLayer); ;
    }

    public RaycastHit2D GetVerticalMiddleCollision(Vector3 direction)
    {
        float raycastDistance = _collider.bounds.extents.y + COLLIDER_SIZE_OVERFLOW;
        return Physics2D.Raycast(_collider.Center(), direction, raycastDistance, _groundLayer);
    }

    public RaycastHit2D GetVerticalRightCollision(Vector3 direction)
    {
        float raycastDistance = _collider.bounds.extents.y + COLLIDER_SIZE_OVERFLOW;
        return Physics2D.Raycast(_collider.Right(), direction, raycastDistance, _groundLayer);
    }

    public bool IsGrounded()
    {
        var hit = GetVerticalCollision(-_collider.transform.up);
        return hit.collider != null && hit.collider.Top().y <= _collider.Center().y;
    }

}
