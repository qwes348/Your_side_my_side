using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScene : BaseScene
{
    [SerializeField]
    private TextMeshProUGUI nowScoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private GameObject newHighScoreObject;
    
    protected override void Init()
    {
        newHighScoreObject.SetActive(Managers.Game.IsNewHighScore);
        
        nowScoreText.text = Managers.Game.Score.ToString("N0");
        highScoreText.text = Managers.SaveLoad.localSaveData.HighScore.ToString("N0");

        Managers.Audio.SetBgmPitch(0);
        Managers.Audio.PlayBgm(Define.Bgm.Score);
    }
    
    public override void Clear()
    {
        
    }

    public void RestartGame()
    {
        Managers.Scene.LoadSceneAsync(Define.Scene.Game);
    }

    public void ToMainMenu()
    {
        Managers.Scene.LoadSceneAsync(Define.Scene.Title);
    }

    public void ResetHighScore()
    {
        Managers.SaveLoad.localSaveData.HighScore = 0;
        highScoreText.text = "0";
        newHighScoreObject.SetActive(false);
    }
}
