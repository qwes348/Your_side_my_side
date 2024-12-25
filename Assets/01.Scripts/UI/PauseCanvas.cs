using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Toggle bgmToggle;
    [SerializeField]
    private Toggle sfxToggle;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button toMainMenuButton;
    
    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    public void Init()
    {
        bgmSlider.SetValueWithoutNotify(Managers.SaveLoad.localSaveData.BGMVolume);
        sfxSlider.SetValueWithoutNotify(Managers.SaveLoad.localSaveData.SFXVolume);
        bgmToggle.SetIsOnWithoutNotify(bgmSlider.value > 0f);
        sfxToggle.SetIsOnWithoutNotify(sfxSlider.value > 0f);

        bgmSlider.onValueChanged.AddListener(OnBgmChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxChanged);
        bgmToggle.onValueChanged.AddListener(OnBgmToggleChanged);
        sfxToggle.onValueChanged.AddListener(OnSfxToggleChanged);
        
        // 게임씬에서만 활성화
        restartButton.GameObject().SetActive(Managers.Scene.CurrentScene.SceneType == Define.Scene.Game);
        toMainMenuButton.GameObject().SetActive(Managers.Scene.CurrentScene.SceneType == Define.Scene.Game);
    }

    private void OnBgmChanged(float value)
    {
        Managers.SaveLoad.localSaveData.BGMVolume = value;
        if(value <= 0f)
            bgmToggle.SetIsOnWithoutNotify(false);
    }

    private void OnSfxChanged(float value)
    {
        Managers.SaveLoad.localSaveData.SFXVolume = value;
        if(value <= 0f)
            sfxToggle.SetIsOnWithoutNotify(false);
    }

    private void OnBgmToggleChanged(bool value)
    {
        bgmSlider.value = value ? Define.DEFAULT_BGM_VOLUME : 0f;
    }

    private void OnSfxToggleChanged(bool value)
    {
        sfxSlider.value = value ? Define.DEFAULT_SFX_VOLUME : 0f;
    }

    public void RestartGame()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public void ToMainMenu()
    {
        Managers.Scene.LoadSceneViaLoading(Define.Scene.Title);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
