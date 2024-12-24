using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Define.Scene.Title;
    }
    
    public override void Clear()
    {
        
    }

    public void StartGame()
    {
        Managers.Scene.LoadSceneViaLoading(Define.Scene.Game);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
