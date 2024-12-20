using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Define.Scene.Game;
        Managers.Game.Init();
    }
    
    public override void Clear()
    {
        Managers.Game.Clear();
    }
}
