using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseState 
{

    protected CharacterStateMachine _stateMachine;
    protected PlatformerController _controller;

    public CharacterBaseState(CharacterStateMachine stateMachine, PlatformerController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }
    
    public void RunState()
    {
        CheckForStateChange();
        RunStateRoutine();
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void RunStateRoutine();
    public abstract void CheckForStateChange();
}
