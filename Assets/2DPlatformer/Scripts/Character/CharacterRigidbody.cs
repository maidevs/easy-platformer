using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRigidbody
{

    private CharacterCollider _collider;
    
    private Vector3 _baseVelocity;
    private Vector3 _instantVelocity;

    private float _gravity;
    private float _gravityScale = 1f;

    private Transform _transform;

    public Vector3 Velocity { get { return _baseVelocity + _instantVelocity; } }
    
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
        _instantVelocity.SetY(y);

    }

    public void SetVelocityX(float x)
    {
        _baseVelocity.SetX(x);
        _instantVelocity.SetX(x);
    }

    public void ResetHorizontalVelocity()
    {
        SetVelocityX(0f);
    }

    public void ResetVerticalVelocity()
    {
        SetVelocityY(0f);
    }
    
    public void RunRigidbodyPhysics()
    {
        CheckRigidbodyCollisions();

        ApplyGravitationalAcceleration();

        UpdatePosition();

        _instantVelocity = Vector3.zero;
    }

    private void CheckRigidbodyCollisions()
    {
        if (_collider.GetHorizontalCollision(Velocity).collider != null)
        {
            ResetHorizontalVelocity();
        }
        if (_collider.GetVerticalCollision(Velocity).collider != null)
        {
            ResetVerticalVelocity();
        }
    }

    private void ApplyGravitationalAcceleration()
    {
        if (!_collider.IsGrounded())
        {
            AddForce(-_transform.up * _gravity * _gravityScale * Time.deltaTime);
        }
    }

    private void UpdatePosition()
    {
        _transform.position += Velocity * Time.deltaTime;
    }


}
