using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFallingState : CharacterBaseState
{

    private const string FALLING_ANIMATION_ID = "Falling";

    private Vector3 horizontalForce;

    public CharacterFallingState(CharacterStateMachine stateMachine, PlatformerController controller) : base(stateMachine, controller) { }

    public override void OnStateEnter()
    {
        _controller.Animator.SetTrigger(FALLING_ANIMATION_ID);
}
    
    public override void OnStateExit()
    {
        _controller.SnapToGroundSurface();
    }
    
    public override void RunStateRoutine()
    {
        _controller.HorizontalMovement();
    }

    public override void CheckForStateChange()
    {

        if (_controller.IsGrounded)
        {
            if (_controller.CanMove()) { _stateMachine.ChangeState(_stateMachine.MoveState); }
            else { _stateMachine.ChangeState(_stateMachine.IdleState); } 
        }

    }


}
