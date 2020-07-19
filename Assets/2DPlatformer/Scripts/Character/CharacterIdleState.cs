using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterBaseState
{

    private const string IDLE_ANIMATION_ID = "Idle";

    public CharacterIdleState(CharacterStateMachine stateMachine, PlatformerController controller) : base(stateMachine, controller)
    {
    }
    
    public override void OnStateEnter()
    {
        _controller.Animator.SetTrigger(IDLE_ANIMATION_ID);
    }

    public override void OnStateExit()
    {
    }

    public override void RunStateRoutine()
    {
    }
    
    public override void CheckForStateChange()
    {

        if (_controller.IsGrounded)
        {
            if (_controller.Input.IsPressingJump()) { _stateMachine.ChangeState(_stateMachine.JumpState); }
            else if (_controller.CanMoveToInputDirection()) { _stateMachine.ChangeState(_stateMachine.MoveState); }
        }
        else
        {
            if (_controller.Velocity.y < 0) { _stateMachine.ChangeState(_stateMachine.FallingState); }
        }
        
    }
}
