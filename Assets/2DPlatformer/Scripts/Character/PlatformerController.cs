using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterRigidbody))]
public class PlatformerController : MonoBehaviour
{
    
    [Header("Collision Detection")]
    [SerializeField]
    private LayerMask groundLayer;

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
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }
    public CharacterCollider Collider { get { return _collider; } }
    public ICharacterInput Input { get { return _input; } }

    public Vector3 Velocity { get { return _rigidbody.Velocity; } }
    public float WalkSpeed { get { return _walkSpeed; } }
    public bool IsGrounded { get { return _collider.IsGrounded(); } }
    
    void Start()
    {

        _collider = new CharacterCollider(GetComponent<Collider2D>(), groundLayer);
        _stateMachine = new CharacterStateMachine(this);
        _input = new KeyboardCharacterInput();

        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        _gravity = (2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        _jumpVelocity = Mathf.Sqrt(2 * _gravity * jumpHeight);

        _rigidbody = new CharacterRigidbody(_gravity, transform, _collider);
    }

    public bool CanMove()
    {
        float inputValue = Input.GetHorizontalMovementAxis();
        return CanMove(inputValue);
    }

    private bool CanMove(float inputValue)
    {
        return inputValue != 0 && Collider.GetHorizontalCollision(Vector3.right * inputValue).collider == null;
    }

    public void HorizontalMovement()
    {
        float inputValue = Input.GetHorizontalMovementAxis();

        if (!CanMove(inputValue)) { return; } 

        Vector3 movementForce = (Vector3.right * inputValue * WalkSpeed);

        _rigidbody.AddInstantForce(movementForce);

        SetSpriteDirection(inputValue);
    }

    public void Jump(float jumpScale = 1f)
    {
        Vector3 jumpForce = (transform.up* _jumpVelocity) * jumpScale;
        _rigidbody.AddForce(jumpForce);
    }

    private void SetSpriteDirection(float inputValue)
    {
        _spriteRenderer.flipX = (inputValue < 0);
    }
    
    public void SnapToGroundSurface()
    {
        Vector3 hitPoint = _collider.GetVerticalCollision(-transform.up).collider.TopCenter();

        float y_fix = hitPoint.y - 0.025f;

        _rigidbody.ResetVerticalVelocity();

        transform.position = new Vector3(transform.position.x, y_fix, transform.position.z);
    }
    
    void Update()
    {
        _stateMachine.RunCurrentState();
        _rigidbody.RunRigidbodyPhysics();
    }

}
