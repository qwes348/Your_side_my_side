using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance => instance;
    
    [SerializeField]
    private Transform charactersParent;
    
    // 모든 캐릭터 스크립터블 오브젝트 데이터
    private List<CharacterData> allCharacterDatas;
    // 모든 스페셜 스크립터블 오브젝트 데이터
    private List<CharacterData> allSpecialDatas;
    // 생성된(화면에 나와있는) 캐릭터 인스턴스들
    private List<Character> charcterInstances = new List<Character>();
    // 좌우 정답 캐릭터 종류를 담는 딕셔너리
    private Dictionary<Define.InputType, List<Define.Character>> answerCharacterTypes = new Dictionary<Define.InputType, List<Define.Character>>();
    

    private void Awake()
    {
        instance = this;
        Init();
    }

    private async UniTaskVoid Init()
    {
        allCharacterDatas = await Managers.Resource.LoadAssetsByLabel<CharacterData>("CharacterData");
        allSpecialDatas = allCharacterDatas.FindAll(cd => cd.@class == Define.CharacterClass.Special);
        allCharacterDatas.RemoveAll(cd => cd.@class == Define.CharacterClass.Special);
        answerCharacterTypes.Add(Define.InputType.Left, new List<Define.Character>());
        answerCharacterTypes.Add(Define.InputType.Right, new List<Define.Character>());
        
        TestStartStage();
    }
    
    [Button]
    public void TestStartStage()
    {
        StartStage();
    }

    public async UniTaskVoid StartStage()
    {
        List<CharacterData> startCharcterDatas = allCharacterDatas.Take(2).ToList();
        answerCharacterTypes[Define.InputType.Left].Add(startCharcterDatas[0].character);
        answerCharacterTypes[Define.InputType.Right].Add(startCharcterDatas[1].character);
        
        for (int i = 0; i < Define.MAX_CHARACTERS_COUNT; i++)
        {
            await SpawnRandomCharacter(startCharcterDatas);
        }
    }

    private async UniTask<Character> SpawnRandomCharacter(List<CharacterData> charcterRange, bool canSpecial = true)
    {
        CharacterData cd = null;
        // 스페셜(폭탄) 확률 체크
        if (canSpecial && Random.value <= Define.SPECIAL_PROBABILITY)
        {
            cd = allSpecialDatas[Random.Range(0, allSpecialDatas.Count)];
        }
        else
        {
            cd = charcterRange[Random.Range(0, charcterRange.Count)];
        }
        
        // 풀에서 프리팹 가져오고 초기화
        var poolable = await Managers.Pool.PopAsync("Prefab/Character");
        var character = poolable.GetComponent<Character>();
        character.transform.SetParent(charactersParent);
        await character.Init(cd);
        
        // 줄세우기
        character.transform.localPosition = (Vector3.forward + Vector3.up) * charcterInstances.Count;
        character.gameObject.SetActive(true);
        
        charcterInstances.Add(character);

        return character;
    }

    public void CheckAnswer(Define.InputType inputType)
    {
        // 정답
        if (answerCharacterTypes[inputType].Contains(charcterInstances[0].CharacterType))
        {
            var target = charcterInstances[0];
            target.transform.SetParent(null);
            DoCorrectAnswerAnim(target, inputType);
            charcterInstances.RemoveAt(0);
        }
        // 오답
        else
        {
            // TODO: 오답연출
        }
    }

    private void DoCorrectAnswerAnim(Character targetCharacter, Define.InputType inputType)
    {
        DOTween.Kill(targetCharacter.transform);
        Vector3 pos = inputType == Define.InputType.Left ? Define.CORRECT_ANSWER_ANIM_POS_LEFT : Define.CORRECT_ANSWER_ANIM_POS_RIGHT;
        targetCharacter.transform.DOMove(pos, 0.3f).OnComplete(targetCharacter.ClearAndPush);
        charactersParent.DOBlendableMoveBy(Vector3.down, 0.3f);
    }
}
