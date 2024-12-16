using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private Transform charactersParent;
    
    [ReadOnly]
    [Expandable]
    [SerializeField]
    private List<CharacterData> characterDatas;

    private void Awake()
    {
        Init();
    }

    private async UniTaskVoid Init()
    {
        characterDatas = await Managers.Resource.LoadAssetsByLabel<CharacterData>("CharacterData");
    }

    public async UniTaskVoid StartStage()
    {
        for (int i = 0; i < Define.MAX_CHARACTERS_COUNT; i++)
        {
            var poolable = await Managers.Pool.PopAsync("Prefab/Character");
            var character = poolable.GetComponent<Character>();
        }
    }

    private void SpawnRandomCharacter(bool canSpecial = true)
    {
        
    }
}
