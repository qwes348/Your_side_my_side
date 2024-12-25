using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Runtime.InteropServices;
using Random = UnityEngine.Random;

namespace Ysms.Game
{
    public class LineController : MonoBehaviour
    {
        [SerializeField]
        private Transform lineParent;
        
        private GameBoard board;
        // 생성된(화면에 나와있는) 캐릭터 인스턴스들
        private YsmsQueue<Character> characterInstances = new YsmsQueue<Character>();

        public Character FrontCharacter => characterInstances.Count == 0 ? null : characterInstances.Peek();
        
        #region const
        public static readonly Vector3 CORRECT_ANSWER_ANIM_POS_LEFT = new Vector3(-1.5f, -3.5f, 0);
        public static readonly Vector3 CORRECT_ANSWER_ANIM_POS_RIGHT = new Vector3(1.5f, -3.5f, 0);
        #endregion
        
        #region 이벤트

        public Action onLineDequeue;
        #endregion

        public void Init()
        {
            board = GetComponent<GameBoard>();
            onLineDequeue += UpdateCharactersPos;
        }
        
        public async UniTask<Character> SpawnRandomCharacter(List<CharacterData> charcterRange, bool canSpecial = true)
        {
            CharacterData cd = charcterRange[Random.Range(0, charcterRange.Count)];

            // 풀에서 프리팹 가져오고 초기화
            var poolable = await Managers.Pool.PopAsync("Prefab/Character");
            var character = poolable.GetComponent<Character>();
            character.transform.SetParent(lineParent);
            await character.Init(cd);

            // 줄세우기
            character.transform.localPosition = (Vector3.forward + Vector3.up) * characterInstances.Count * 0.5f;
            character.gameObject.SetActive(true);

            characterInstances.Enqueue(character);

            return character;
        }

        public void CorrectAnswerSequence(Define.InputType inputType, bool applyScore = true)
        {
            FrontCharacter.transform.SetParent(null);
            DOTween.Kill(FrontCharacter.transform);
            Vector3 pos = inputType == Define.InputType.Left ? CORRECT_ANSWER_ANIM_POS_LEFT : CORRECT_ANSWER_ANIM_POS_RIGHT;
            FrontCharacter.transform.DOMove(pos, 0.3f).OnComplete(FrontCharacter.ClearAndPush);

            var outChar = characterInstances.Dequeue();
            onLineDequeue?.Invoke();

            if (applyScore)
            {
                Managers.Game.AddScore(outChar.MyCharcterData.score);
                Managers.Game.Combo += 1;
            }

            Managers.Audio.PlaySfx(Define.Sfx.Move);
        }

        public void WrongAnswerSequence()
        {
            if (DOTween.IsTweening(FrontCharacter.transform))
                return;
            FrontCharacter.transform.DOShakePosition(0.5f, Vector3.right * 0.3f, 20, fadeOut: false);
        }

        /// <summary>
        /// 연출없이 디큐만 진행
        /// </summary>
        public void DequeueFront(bool applyScore = true)
        {
            var front = characterInstances.Dequeue();
            onLineDequeue?.Invoke();
            if (applyScore)
            {
                Managers.Game.AddScore(front.MyCharcterData.score);
                Managers.Game.Combo += 1;
            }
            front.ClearAndPush();
        }

        /// <summary>
        /// 캐릭터들 앞으로 당기기
        /// </summary>
        private void UpdateCharactersPos()
        {
            foreach (var character in characterInstances.GetEnumerable())
            {
                character.transform.DOBlendableLocalMoveBy((Vector3.down + Vector3.back) * 0.5f, 0.3f);
            }
        }

        public async UniTask ChangeLineCharacter(int index, CharacterData cd)
        {
            await characterInstances[index].Init(cd);
        }
    }   
}
