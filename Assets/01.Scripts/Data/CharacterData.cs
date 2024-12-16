using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObject/Character")]
public class CharacterData : ScriptableObject
{
    public Define.CharacterType type;
    public Define.Character character;
    public int score;
}
