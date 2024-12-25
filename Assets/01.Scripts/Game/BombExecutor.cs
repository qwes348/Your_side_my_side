using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ysms.Game;

public class BombExecutor : SpecialCharacterExecutor
{
    private LineController line;
    private AnswerController answer;
    
    public override void Init()
    {
        characterType = Define.Character.Bomb;
        line = GetComponent<LineController>();
        answer = GetComponent<AnswerController>();
    }
    
    public override void Execute()
    {
        line.DequeueFront();
        for (int i = 0; i < 5; i++)
        {
            var inputType = answer.GetCorrectAnswer(line.FrontCharacter.CharacterType);
            line.CorrectAnswerSequence(inputType);
        }
    }
}
