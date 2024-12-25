using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameStatePanel : MonoBehaviour
{
    [SerializeField]
    private Transform readyTransform;
    [SerializeField]
    private Transform startTransform;
    [SerializeField]
    private Transform gameOverTransform;

    private void Awake()
    {
        readyTransform.gameObject.SetActive(false);
        startTransform.gameObject.SetActive(false);
        gameOverTransform.gameObject.SetActive(false);
    }

    [Button]
    public async UniTask Ready()
    {
        var cg = readyTransform.GetComponent<CanvasGroup>();
        
        readyTransform.localPosition = new Vector3(Screen.width / 2f * -1f, readyTransform.localPosition.y, readyTransform.localPosition.z);
        cg.alpha = 0;
        readyTransform.gameObject.SetActive(true);
        
        cg.DOFade(1f, 0.65f).SetLoops(2, LoopType.Yoyo);
        await readyTransform.DOLocalMoveX(Screen.width / 2f, 1.5f);
        readyTransform.gameObject.SetActive(false);
    }

    [Button]
    public async UniTask GameStart()
    {
        var cg = startTransform.GetComponent<CanvasGroup>();

        startTransform.localScale = Vector3.zero;
        cg.alpha = 0;
        startTransform.gameObject.SetActive(true);

        Managers.Audio.PlaySfx(Define.Sfx.GameStart);
        cg.DOFade(1f, 0.35f);
        await startTransform.DOScale(1f, 0.4f).SetEase(Ease.OutCubic);
        cg.DOFade(0f, 0.5f).OnComplete(() => startTransform.gameObject.SetActive(false));
    }

    [Button]
    public async UniTask GameOver()
    {
        var cg = gameOverTransform.GetComponent<CanvasGroup>();

        cg.alpha = 1;
        gameOverTransform.DOLocalMoveY(500f, 0f);
        gameOverTransform.gameObject.SetActive(true);

        Managers.Audio.PlaySfx(Define.Sfx.GameOver);
        await gameOverTransform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBounce);
        await cg.DOFade(0f, 0.5f);
        gameOverTransform.gameObject.SetActive(false);
    }
}
