using UnityEngine;

public class KeyboardCharacterInput : ICharacterInput 
{

    public KeyboardCharacterInput()
    {
    }

    public float GetHorizontalMovementAxis()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool IsPressingJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
