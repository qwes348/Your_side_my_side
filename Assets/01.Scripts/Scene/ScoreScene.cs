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
        // TODO: 하이스코어 불러오기
        newHighScoreObject.SetActive(false);
        
        nowScoreText.text = Managers.Game.Score.ToString("N0");
        highScoreText.text = "0";
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
        // TODO: 구현
    }
}
