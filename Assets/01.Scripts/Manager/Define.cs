using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Define 
{
    #region constant
    
    public const int MAX_CHARACTERS_COUNT = 50;
    public const float SPECIAL_PROBABILITY = 0.02f;
    #endregion

    #region enum
    public enum Scene
    {
        Start,
        Game,
        Loading,
        Score
    }

    public enum CharacterType
    {
        None = -1,
        Normal,
        Special
    }

    public enum Character
    {
        None = -1,
        Bear,
        Chick,
        Chicken,
        Cow,
        Duck,
        Giraffe,
        Hippo,
        Narwhal,
        Panda,
        Parrot,
        Penguin,
        Pig,
        Snake,
        Whale,
        Zebra,
        Bomb = 100
    }
    #endregion
}
