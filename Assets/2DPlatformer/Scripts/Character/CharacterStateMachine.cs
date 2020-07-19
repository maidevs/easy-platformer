using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine 
{

    private PlatformerController _controller;

    public CharacterIdleState IdleState;
    public CharacterMoveState MoveState;
    public CharacterJumpState JumpState;
    public CharacterFallingState FallingState;
    public CharacterWallSlideState WallSlideState;
    public CharacterWallJumpState WallJumpState;

    private CharacterBaseState _state;
    
    public CharacterStateMachine(PlatformerController controller)
    {
        _controller = controller;

        IdleState = new CharacterIdleState(this, _controller);
        MoveState = new CharacterMoveState(this, _controller);
        JumpState = new CharacterJumpState(this, _controller);
        FallingState = new CharacterFallingState(this, _controller);
        WallSlideState = new CharacterWallSlideState(this, _controller);
        WallJumpState = new CharacterWallJumpState(this, _controller);

        _state = IdleState;
    }

    public void ChangeState(CharacterBaseState newState)
    {

        _state.OnStateExit();
        _state = newState;
        _state.OnStateEnter();

    }
    
    public void RunCurrentState()
    {
        _state.RunState();
    }

}
