using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleScene : BaseScene
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    
    protected override void Init()
    {
        SceneType = Define.Scene.Title;
        highScoreText.text = Managers.SaveLoad.localSaveData.HighScore.ToString("N0");
        Managers.Audio.PlayBgm(Define.Bgm.Title);
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
