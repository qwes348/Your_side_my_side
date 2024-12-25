using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseMenuButton : MonoBehaviour
{
    private PauseCanvas activePauseCanvas;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OpenPauseMenu());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activePauseCanvas != null)
            {
                activePauseCanvas.Close();
                return;
            }
            
            OpenPauseMenu();
        }
    }

    public async UniTask OpenPauseMenu()
    {
        activePauseCanvas = await Managers.Resource.InstantiateAsset<PauseCanvas>("Prefab/PauseCanvas");
        activePauseCanvas.Init();
    }
}
