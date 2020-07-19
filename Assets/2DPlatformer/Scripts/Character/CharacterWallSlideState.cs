using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWallSlideState : CharacterBaseState
{
    private const string SLIDE_ANIM_ID = "Sliding";
    
    public CharacterWallSlideState(CharacterStateMachine stateMachine, PlatformerController controller) : base(stateMachine, controller)
    {
    }

    public override void CheckForStateChange()
    {
        if (_controller.IsGrounded) { _stateMachine.ChangeState(_stateMachine.IdleState); }
        else if (!IsPushingAgainstWallOnAir()) { _stateMachine.ChangeState(_stateMachine.FallingState); }
        else if (IsPushingAgainstWallOnAir() && _controller.Input.IsPressingJump()) { _stateMachine.ChangeState(_stateMachine.WallJumpState); }
    }

    public override void OnStateEnter()
    {
        _controller.Animator.SetTrigger(SLIDE_ANIM_ID);
        _controller.Body.SetGravityScale(.05f);
        _controller.Body.ResetVerticalVelocity();
        _controller.Body.ResetHorizontalVelocity();
        _controller.SetSpriteDirection(_controller.Input.GetHorizontalMovementAxis());
    }

    public override void OnStateExit()
    {
        _controller.Body.SetGravityScale(1f);
        _controller.SpriteRenderer.flipX = !_controller.SpriteRenderer.flipX;
    }

    public override void RunStateRoutine()
    {
    }
    
    public bool IsPushingAgainstWallOnAir()
    {
        float movementInput = _controller.Input.GetHorizontalMovementAxis();

        if (movementInput == 0) { return false; }

        Vector3 moveDirection =  movementInput * Vector3.right;
        
        return !_controller.IsGrounded && _controller.Collider.GetHorizontalTopCollision(moveDirection).collider != null;
    }
}
