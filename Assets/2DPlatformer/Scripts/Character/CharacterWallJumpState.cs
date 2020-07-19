using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWallJumpState : CharacterBaseState
{
    private const string JUMP_ANIMATION_ID = "Jump";
    private float _jumpElapsedTime;

    public CharacterWallJumpState(CharacterStateMachine stateMachine, PlatformerController controller) : base(stateMachine, controller)
    {
    }

    public override void CheckForStateChange()
    {
        if (_stateMachine.WallSlideState.IsPushingAgainstWallOnAir() && _jumpElapsedTime > 0.35f) { _stateMachine.ChangeState(_stateMachine.WallSlideState); }
        else if (!_controller.IsGrounded && _controller.Velocity.y <= 0) { _stateMachine.ChangeState(_stateMachine.FallingState); }
        else if (_controller.IsGrounded && _jumpElapsedTime > 0.2f && !_controller.CanMoveToInputDirection()) { _stateMachine.ChangeState(_stateMachine.IdleState); }
        else if (_controller.IsGrounded && _jumpElapsedTime > 0.2f && _controller.CanMoveToInputDirection()) { _stateMachine.ChangeState(_stateMachine.MoveState); }
    }

    private Vector3 GetWallJumpDirection()
    {
        var raycastHit = _controller.Collider.GetHorizontalCollision(Vector3.right);
        if (raycastHit.collider == null)
        {
            raycastHit = _controller.Collider.GetHorizontalCollision(Vector3.left);
        }

        if (raycastHit.collider == null)
        {
            Debug.LogError("Raycast hit nothing during wall jump");
            return Vector3.zero;
        }

        return (Vector3)raycastHit.normal;
    }

    public override void OnStateEnter()
    {
        _jumpElapsedTime = 0f;
        
        var wallJumpDirection = GetWallJumpDirection();

        _controller.Animator.SetTrigger(JUMP_ANIMATION_ID);
        _controller.Body.ResetVerticalVelocity();
        _controller.Body.AddForce(wallJumpDirection * 5f);
        _controller.Jump();
    }

    public override void OnStateExit()
    {
        _controller.Body.ResetHorizontalVelocity();
    }

    public override void RunStateRoutine()
    {
        _jumpElapsedTime += Time.deltaTime;
        if (_jumpElapsedTime > 0.35f)
        {
            _controller.Body.ResetHorizontalVelocity();
            _controller.HorizontalMovement();
        }
    }
}
