using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObject/Character")]
public class CharacterData : ScriptableObject
{
    public Define.CharacterClass characterClass;
    public Define.Character characterType;
    public int score;
}
