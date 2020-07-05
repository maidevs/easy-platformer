using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterInput
{
    float GetHorizontalMovementAxis();
    bool IsPressingJump();
}
