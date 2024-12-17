using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private MainInputAction mainAction;
    private InputAction horizontalAction;

    private void Awake()
    {
        mainAction = new MainInputAction();
        horizontalAction = mainAction.InGame.Horizontal;
    }

    private void OnEnable()
    {
        mainAction.Enable();
        horizontalAction.started += HorizontalInput;
    }

    private void OnDisable()
    {
        mainAction.Disable();
        horizontalAction.started -= HorizontalInput;
    }

    private void HorizontalInput(InputAction.CallbackContext context)
    {
        Vector2 inputVec = context.ReadValue<Vector2>();
        if (inputVec == Vector2.left)
        {
            StageManager.Instance.CheckAnswer(Define.InputType.Left);
        }
        else if(inputVec == Vector2.right)
        {
            StageManager.Instance.CheckAnswer(Define.InputType.Right);
        }
    }
}
