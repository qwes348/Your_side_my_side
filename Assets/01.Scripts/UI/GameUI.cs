using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameUI : MonoBehaviour
{
    private static GameUI instance;
    public static GameUI Instance => instance;
    
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI comboText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private RectTransform timeRectTransform;
    
    [FormerlySerializedAs("gameStatePanel")]
    [HorizontalLine]
    [SerializeField]
    private GameStatePanel gameStateUIPanel;

    private int scoreForUi;
    private int comboForUi;
    private Tweener scoreTweener;
    private Tweener comboTweener;
    private Sequence comboSequence;
    private CanvasGroup comboGroup;
    private float timeRectSizeX;
    
    public GameStatePanel GameStateUI => gameStateUIPanel;

    private void Awake()
    {
        instance = this;
        
        Managers.Game.onScoreUpdate += UpdateScore;
        Managers.Game.onComboUpdate += UpdateCombo;
        Managers.Game.onTimeUpdate += UpdateTime;
        Managers.Game.onGameStateChanged += state =>
        {
            if (state == Define.GameState.GameOver)
                GameStateUI.GameOver();
        };

        scoreText.text = "0";
        comboText.text = "0";
        scoreForUi = 0;
        comboForUi = 0;
        comboGroup = comboText.GetComponentInParent<CanvasGroup>();
        comboGroup.alpha = 0;
        timeText.text = Define.GAME_INITIAL_TIME.ToString("N0");
    }

    public void UpdateScore(int score)
    {
        if(scoreTweener != null && scoreTweener.IsPlaying())
            scoreTweener.Kill();
        scoreTweener = DOVirtual.Int(scoreForUi, score, 0.15f, v => scoreText.text = v.ToString("N0"));
        scoreForUi = score;
    }

    public void UpdateCombo(int combo)
    {
        if (combo < 2)
        {
            comboGroup.alpha = 0;
            return;
        }

        if (comboSequence == null)
        {
            comboSequence = DOTween.Sequence()
                .OnStart(() =>
                {
                    comboGroup.alpha = 0.3f;
                    comboGroup.transform.localScale = Vector3.one * 0.3f;
                })
                .Append(comboGroup.DOFade(1f, 0.15f))
                .Join(comboGroup.transform.DOScale(1f, 0.15f))
                .SetAutoKill(false);
        }
        else
        {
            comboSequence.Restart();
        }
        
        if(comboTweener != null && comboTweener.IsPlaying())
            comboTweener.Kill();
        comboTweener = DOVirtual.Int(comboForUi, combo, 0.15f, v => comboText.text = v.ToString("N0"));
        comboForUi = combo;
    }

    public void UpdateTime(float time)
    {
        timeText.text = time.ToString("N0");
        var remainTimeRate = time / Define.GAME_INITIAL_TIME;
        timeRectTransform.anchoredPosition = Vector2.left * ((1f - remainTimeRate) * timeRectTransform.rect.width);
    }
}
