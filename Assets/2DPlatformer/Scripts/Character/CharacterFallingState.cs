using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFallingState : CharacterBaseState
{

    private const string FALLING_ANIMATION_ID = "Falling";

    private Vector3 horizontalForce;

    public CharacterFallingState(CharacterStateMachine stateMachine, PlatformerController controller) : base(stateMachine, controller)
    {
    }

    public override void OnStateEnter()
    {
        _controller.Animator.SetTrigger(FALLING_ANIMATION_ID);
    }

    public override void OnStateExit()
    {
    }

    public override void RunStateRoutine()
    {
        _controller.HorizontalMovement();
    }

    public override void CheckForStateChange()
    {
        if (_controller.IsGrounded && _controller.CanMoveToInputDirection()) { _stateMachine.ChangeState(_stateMachine.MoveState); }
        else if (_stateMachine.WallSlideState.IsPushingAgainstWallOnAir()) { _stateMachine.ChangeState(_stateMachine.WallSlideState); }
        else if (_controller.IsGrounded) { _stateMachine.ChangeState(_stateMachine.IdleState); }
        else if (!_controller.IsGrounded && _controller.Body.IsInCoyoteTime && _controller.Input.IsPressingJump()) { _stateMachine.ChangeState(_stateMachine.JumpState); }
    }


}
