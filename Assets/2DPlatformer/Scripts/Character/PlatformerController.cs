using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterRigidbody))]
public class PlatformerController : MonoBehaviour
{

    [Header("Collision Detection")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Jump Properties")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float timeToApex = 0.1f;

    private float _gravity;
    private float _jumpVelocity;
    private float _walkSpeed = 5f;

    private CharacterCollider _collider;
    private CharacterRigidbody _rigidbody;
    private CharacterStateMachine _stateMachine;
    private ICharacterInput _input;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    /* Properties */

    public Animator Animator { get { return _animator; } }
    public CharacterRigidbody Body { get { return _rigidbody; } }
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }
    public CharacterCollider Collider { get { return _collider; } }
    public ICharacterInput Input { get { return _input; } }

    public Vector3 Velocity { get { return _rigidbody.Velocity; } }
    public float WalkSpeed { get { return _walkSpeed; } }
    public bool IsGrounded { get { return _collider.IsGrounded(); } }

    void Start()
    {

        _collider = new CharacterCollider(GetComponentInChildren<Collider2D>(), groundLayer);
        _stateMachine = new CharacterStateMachine(this);
        _input = new KeyboardCharacterInput();

        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _gravity = (2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        _jumpVelocity = Mathf.Sqrt(2 * _gravity * jumpHeight);

        _rigidbody = new CharacterRigidbody(_gravity, transform, _collider);
    }

    public bool CanMoveToInputDirection()
    {
        float inputValue = Input.GetHorizontalMovementAxis();
        return CanMoveToInputDirection(inputValue);
    }

    private bool CanMoveToInputDirection(float inputValue)
    {
        return inputValue != 0 && Collider.GetHorizontalCollision(Vector3.right * inputValue).collider == null;
    }

    public void HorizontalMovement()
    {
        var inputValue = Input.GetHorizontalMovementAxis();
        var movementForce = (Vector3.right * inputValue * WalkSpeed);

        _rigidbody.AddInstantForce(movementForce);
        SetSpriteDirection(inputValue);
    }

    public void Jump(float jumpScale = 1f)
    {
        Vector3 jumpForce = (transform.up * _jumpVelocity) * jumpScale;
        _rigidbody.AddForce(jumpForce);
    }

    public void SetSpriteDirection(float inputValue)
    {
        if (inputValue == 0) { return; }

        _spriteRenderer.flipX = (inputValue < 0);
    }

    void Update()
    {
        _stateMachine.RunCurrentState();
        _rigidbody.RunRigidbodyPhysics();
    }

}
