using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HorizontalInputButton : MonoBehaviour
{
    public Define.InputType inputType;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => StageManager.Instance.CheckAnswer(inputType));
    }
}
