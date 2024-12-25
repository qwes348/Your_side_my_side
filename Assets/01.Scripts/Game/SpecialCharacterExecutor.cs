using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialCharacterExecutor : MonoBehaviour
{
    protected Define.Character characterType;
    
    public Define.Character CharacterType => characterType;

    protected void Start()
    {
        Init();
    }

    public abstract void Init();
    
    public abstract void Execute();
}
