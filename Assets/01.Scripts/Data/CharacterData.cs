using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObject/Character")]
public class CharacterData : ScriptableObject
{
    [FormerlySerializedAs("type")]
    public Define.CharacterClass @class;
    public Define.Character character;
    public int score;
}
