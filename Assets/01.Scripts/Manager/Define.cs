using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Define 
{
    #region constant
    
    public const int MAX_CHARACTERS_COUNT = 20;
    public const int LEVEL_UP_THRESHOLD = 4000;
    public const int MAX_LEVEL = 4;
    
    public const float SPECIAL_PROBABILITY = 0.02f;
    public const float GAME_INITIAL_TIME = 60f;
    
    #endregion

    #region enum
    public enum Scene
    {
        Start,
        Game,
        Loading,
        Score
    }

    public enum CharacterClass
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

    public enum InputType
    {
        Left,
        Right
    }
    public enum GameState
    {
        None = 0,
        Running = 1,
        GameOver = 2
    }
    #endregion
}
