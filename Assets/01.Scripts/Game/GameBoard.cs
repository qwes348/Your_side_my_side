using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ysms.Game
{
    public class GameBoard : MonoBehaviour
    {
        private static GameBoard instance;
        public static GameBoard Instance => instance;

        // 모든 캐릭터 스크립터블 오브젝트 데이터
        private List<CharacterData> allCharacterDatas;
        // 모든 스페셜 스크립터블 오브젝트 데이터
        private List<CharacterData> allSpecialDatas;
        private Dictionary<Define.Character, SpecialCharacterExecutor> specialExecutorsDict;
        
        #region 컴포넌트

        private AnswerController answer;
        private LineController line;
        private LevelController level;
        private BombGaugeController bombGauge;
        private FeverController fever;
        #endregion
        
        #region 프로퍼티
        public IReadOnlyList<CharacterData> AllCharacterDatas => allCharacterDatas;
        public IReadOnlyList<CharacterData> AllSpecialDatas => allSpecialDatas;
        #endregion


        private void Awake()
        {
            instance = this;
        }

        private async void Start()
        {
            await Init();
            await GameStart();
        }

        private void Update()
        {
            if (Managers.Game.GameState == Define.GameState.Running)
            {
                Managers.Game.GameTime -= Time.deltaTime;
                if (Managers.Game.GameTime <= 0)
                    GameOver();
            }
        }

        private async UniTask Init()
        {
            allCharacterDatas = await Managers.Resource.LoadAssetsByLabel<CharacterData>("CharacterData");
            allSpecialDatas = allCharacterDatas.FindAll(cd => cd.characterClass == Define.CharacterClass.Special);
            allCharacterDatas.RemoveAll(cd => cd.characterClass == Define.CharacterClass.Special);
            
            answer = GetComponent<AnswerController>();
            line = GetComponent<LineController>();
            level = GetComponent<LevelController>();
            bombGauge = GetComponent<BombGaugeController>();
            fever = GetComponent<FeverController>();
                
            answer.Init();
            line.Init();
            level.Init();
            bombGauge.Init();
            fever.Init();

            line.onLineDequeue += SpawnSingleCharacter;
            
            specialExecutorsDict = new Dictionary<Define.Character, SpecialCharacterExecutor>();
            var executors = GetComponents<SpecialCharacterExecutor>();
            foreach (var ex in executors)
            {
                specialExecutorsDict.Add(ex.CharacterType, ex);
            }
        }

        private async UniTask GameStart()
        {
            await SpawnInitCharacters();
            await GameUI.Instance.GameStateUI.Ready();
            await GameUI.Instance.GameStateUI.GameStart();
            Managers.Game.GameState = Define.GameState.Running;
        }

        private async UniTask GameOver()
        {
            Managers.Game.GameState = Define.GameState.GameOver;

            if (Managers.Game.Score > Managers.SaveLoad.localSaveData.HighScore)
            {
                Managers.SaveLoad.localSaveData.HighScore = Managers.Game.Score;
                Managers.Game.IsNewHighScore = true;
            }
            
            await GameUI.Instance.GameStateUI.GameOver();
            Managers.Scene.LoadSceneViaLoading(Define.Scene.Score);
        }

        private async UniTask SpawnInitCharacters()
        {
            List<CharacterData> startCharcterDatas = allCharacterDatas.Take(2).ToList();
            // 좌,우 1종류씩만으로 시작
            await answer.AddAnswer(Define.InputType.Left, startCharcterDatas[0]);
            await answer.AddAnswer(Define.InputType.Right, startCharcterDatas[1]);

            for (int i = 0; i < Define.MAX_CHARACTERS_COUNT; i++)
            {
                await line.SpawnRandomCharacter(startCharcterDatas);
            }
        }

        private void SpawnSingleCharacter()
        {
            List<CharacterData> characterRange = allCharacterDatas.Take(Managers.Game.Level + 2).ToList();
            line.SpawnRandomCharacter(characterRange);
        }

        public void OnAnswerInput(Define.InputType inputType)
        {
            if (Managers.Game.GameState != Define.GameState.Running)
                return;
            
            if (line.FrontCharacter.MyCharcterData.characterClass == Define.CharacterClass.Special)
            {
                ApplySpecialCharacter(line.FrontCharacter);
                
                return;
            }

            // 정답
            if (answer.IsCorrectAnswer(inputType, line.FrontCharacter.CharacterType))
            {
                line.CorrectAnswerSequence(inputType);
            }
            // 오답
            else
            {
                line.WrongAnswerSequence();
                Managers.Game.Combo = 0;
            }
        }

        private void ApplySpecialCharacter(Character cha)
        {
            // 스페셜은 100번대
            if ((int)cha.CharacterType < 100)
                return;

            var executor = specialExecutorsDict[cha.CharacterType];
            if(executor != null)
                executor.Execute();
        }
    }
}
