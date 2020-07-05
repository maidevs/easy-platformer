using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterMoveState : CharacterBaseState
{

    private const string WALK_ANIMATION_ID = "Walk";
    
    [Header( "Movement Properties")]
    [SerializeField]
    private float _walkSpeed = 5f;
    
    public CharacterMoveState(CharacterStateMachine stateMachine, PlatformerController controller) : base(stateMachine, controller) { }

    public override void OnStateEnter()
    {
        _controller.Animator.SetTrigger(WALK_ANIMATION_ID);
    }

    public override void OnStateExit() { }
    
    public override void RunStateRoutine()
    {
        _controller.HorizontalMovement();
    }

    public override void CheckForStateChange()
    {

        if (_controller.IsGrounded)
        {
            if (_controller.Input.IsPressingJump()) { _stateMachine.ChangeState(_stateMachine.JumpState); }
            else if (!_controller.CanMove()) { _stateMachine.ChangeState(_stateMachine.IdleState); }
        }
        else
        {
            if (_controller.Velocity.y < 0f) { _stateMachine.ChangeState(_stateMachine.FallingState); } 
        }

    }

}
