using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJumpState : CharacterBaseState
{
    
    private const string JUMP_ANIMATION_ID = "Jump";
    
    private float _jumpElapsedTime;
    
    public CharacterJumpState(CharacterStateMachine stateMachine, PlatformerController controller) : base(stateMachine, controller)
    {
    }
    
    public override void OnStateEnter()
    {
        _jumpElapsedTime = 0f;

        _controller.Animator.SetTrigger(JUMP_ANIMATION_ID);

        _controller.Body.ResetVerticalVelocity();
        _controller.Jump();
    }

    public override void OnStateExit()
    {
    }
    
    public override void RunStateRoutine()
    {
        _jumpElapsedTime += Time.deltaTime;
        _controller.HorizontalMovement();
    }

    public override void CheckForStateChange()
    {
        if (_stateMachine.WallSlideState.IsPushingAgainstWallOnAir()) { _stateMachine.ChangeState(_stateMachine.WallSlideState); }
        else if (!_controller.IsGrounded && _controller.Velocity.y <= 0) { _stateMachine.ChangeState(_stateMachine.FallingState); }
        else if (_controller.IsGrounded && _jumpElapsedTime > 0.2f && !_controller.CanMoveToInputDirection()) { _stateMachine.ChangeState(_stateMachine.IdleState); }
        else if (_controller.IsGrounded && _jumpElapsedTime > 0.2f && _controller.CanMoveToInputDirection()) { _stateMachine.ChangeState(_stateMachine.MoveState); }
    }

}
