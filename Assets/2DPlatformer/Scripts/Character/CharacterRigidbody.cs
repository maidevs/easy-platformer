using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRigidbody
{
    private CharacterCollider _collider;
    private Vector3 _baseVelocity, _instantVelocity;
    private Collider2D _previousHorizontalCollision, _previousVerticalCollision;

    private float _gravity;
    private float _gravityScale = 1f;

    private Transform _transform;

    private float _coyoteTime = 0f;
    private const float _coyoteWindow = .6f;

    public Vector3 Velocity { get { return _baseVelocity + _instantVelocity; } }
    public bool IsInCoyoteTime { get { return _coyoteTime <= _coyoteWindow; } }

    public CharacterRigidbody(float gravity, Transform transform, CharacterCollider collider)
    {
        _gravity = gravity;
        _transform = transform;
        _collider = collider;
    }

    public void AddInstantForce(Vector3 force)
    {
        _instantVelocity += force;
    }

    public void AddForce(Vector3 force)
    {
        _baseVelocity += force;
    }

    public void SetVelocityY(float y)
    {
        _baseVelocity.SetY(y);
        _instantVelocity.SetY(0f);
    }

    public void SetVelocityX(float x)
    {
        _baseVelocity.SetX(x);
        _instantVelocity.SetX(0f);
    }

    public void ResetHorizontalVelocity()
    {
        SetVelocityX(0f);
    }

    public void ResetVerticalVelocity()
    {
        SetVelocityY(0f);
    }

    public void SetGravityScale(float gravityScale)
    {
        _gravityScale = gravityScale;
    }

    public void RunRigidbodyPhysics()
    {
        CheckRigidbodyCollisions();

        ApplyGravitationalAcceleration();

        ClampVelocity();

        UpdatePosition();

        _instantVelocity = Vector3.zero;
    }

    private void CheckRigidbodyCollisions()
    {
        RaycastHit2D horizontalHit = _collider.GetHorizontalCollision(Vector3.right * Velocity.x);
        RaycastHit2D verticalHit = _collider.GetVerticalCollision(Vector3.up * Velocity.y);

        if (horizontalHit.collider != null && Mathf.Abs(Velocity.x) != 0f)
        {
            if (_previousHorizontalCollision == null)
            {
                _previousHorizontalCollision = horizontalHit.collider;
                SnapHorizontal(horizontalHit);
            }
            ResetHorizontalVelocity();
        }
        else
        {
            _previousHorizontalCollision = null;
        }


        if (verticalHit.collider != null && Mathf.Abs(Velocity.y) != 0)
        {
            if (_previousHorizontalCollision == null)
            {
                _previousVerticalCollision = verticalHit.collider;
                SnapVertical(verticalHit);
            }
            ResetVerticalVelocity();
        }
        else
        {
            _previousVerticalCollision = null;
        }

    }

    private void SnapVertical(RaycastHit2D verticalHit)
    {
        Vector3 position = verticalHit.point + (verticalHit.normal.normalized * _collider.Extents.y);
        _transform.position = new Vector3(_transform.position.x, position.y, _transform.position.z) - (Vector3.up * _collider.Offset.y);
    }

    private void SnapHorizontal(RaycastHit2D horizontalHit)
    {
        Vector3 position = horizontalHit.point + (horizontalHit.normal.normalized * _collider.Extents.x);
        _transform.position = new Vector3(position.x, _transform.position.y, _transform.position.z) - (Vector3.right * _collider.Offset.x);
    }

    private void ClampVelocity()
    {
        var clampedY = Mathf.Clamp(Velocity.y, -15f, 15f);
        _baseVelocity.SetY(clampedY);
    }

    private void ApplyGravitationalAcceleration()
    {
        if (!_collider.IsGrounded())
        {
            AddForce(-_transform.up * _gravity * _gravityScale * Time.deltaTime);
            _coyoteTime += Time.deltaTime;
        }
        else
        {
            _coyoteTime = 0f;
        }
    }

    private void UpdatePosition()
    {
        _transform.position += Velocity * Time.deltaTime;
    }


}
