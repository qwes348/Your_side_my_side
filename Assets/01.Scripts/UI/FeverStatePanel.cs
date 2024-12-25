using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverStatePanel : MonoBehaviour
{
    [SerializeField]
    private Transform feverTransform;
    [SerializeField]
    private Transform superFeverTransform;
    [SerializeField]
    private ParticleSystem starParticle;
    [SerializeField]
    private ParticleSystem feverLoopParticle;
    [SerializeField]
    private ParticleSystem superFeverLoopParticle;
    [SerializeField]
    private SpriteRenderer background;
    

    private void Awake()
    {
        feverTransform.gameObject.SetActive(false);
        superFeverTransform.gameObject.SetActive(false);
        Managers.Game.onFeverStateChanged += OnFeverChanged;
    }

    private void OnFeverChanged(Define.FeverState feverState)
    {
        switch (feverState)
        {
            case Define.FeverState.Normal:
                Normal();
                break;
            case Define.FeverState.Fever:
                Fever();
                break;
            case Define.FeverState.SuperFever:
                SuperFever();
                break;
        }
    }

    /// <summary>
    /// 피버 연출 초기화
    /// </summary>
    public void Normal()
    {
        background.DOKill();
        background.DOColor(Color.white, 0.3f);
        feverLoopParticle.Stop();
        superFeverLoopParticle.Stop();
    }

    /// <summary>
    /// 피버 UI 연출
    /// </summary>
    public async UniTask Fever()
    {
        var cg = feverTransform.GetComponent<CanvasGroup>();

        feverTransform.localScale = Vector3.one * 0.4f;
        cg.alpha = 0.3f;
        starParticle.Play();
        feverLoopParticle.Play();
        background.DOKill();
        background.DOColor(Color.gray, 0.3f);
        feverTransform.gameObject.SetActive(true);
        
        feverTransform.DOScale(1f, 0.4f);
        await cg.DOFade(1f, 0.4f);
        
        await cg.DOFade(0f, 0.5f);
        
        feverTransform.gameObject.SetActive(false);
    }

    /// <summary>
    /// 슈퍼 피버 UI 연출
    /// </summary>
    public async UniTask SuperFever()
    {
        var cg = superFeverTransform.GetComponent<CanvasGroup>();

        superFeverTransform.localScale = Vector3.one * 0.4f;
        cg.alpha = 0.3f;
        starParticle.Play();
        superFeverLoopParticle.Play();
        background.DOKill();
        background.DOColor(Color.black, 0.3f);
        superFeverTransform.gameObject.SetActive(true);
        
        superFeverTransform.DOScale(1f, 0.4f);
        await cg.DOFade(1f, 0.4f);
        
        await cg.DOFade(0f, 0.5f);
        
        superFeverTransform.gameObject.SetActive(false); 
    }
}
