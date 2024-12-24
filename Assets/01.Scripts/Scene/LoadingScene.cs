using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : BaseScene
{
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private Slider loadingSlider;
    
    protected override void Init()
    {
        SceneType = Define.Scene.Loading;
        
        loadingText.DOText("불러오는 중...", 1f).SetLoops(-1, LoopType.Yoyo);
        LoadNextScene();
    }
    
    public override void Clear()
    {
        loadingText.DOKill();
    }

    private async UniTask LoadNextScene()
    {
        var op = Managers.Scene.LoadSceneAsync(Managers.Scene.NextSceneType);
        op.allowSceneActivation = false;
        
        float timer = 0f;
        while (!op.isDone)
        {
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                loadingSlider.value = Mathf.Lerp(op.progress, 1f, timer);
                if (loadingSlider.value >= op.progress)
                    timer = 0f;
            }
            else
            {
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, 1f, timer);
                if(loadingSlider.value >= 0.99f)
                    op.allowSceneActivation = true;
            }
            
            await UniTask.Yield();
        }
    }
}
