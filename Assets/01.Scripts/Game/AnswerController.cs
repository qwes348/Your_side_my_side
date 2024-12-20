using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ysms.Game
{
    public class AnswerController : MonoBehaviour
    {
        private GameBoard board;
        // 좌우 정답 캐릭터 종류를 담는 딕셔너리
        private Dictionary<Define.InputType, List<Define.Character>> answerDict = new Dictionary<Define.InputType, List<Define.Character>>();
        private Dictionary<Define.InputType, Transform> answerSamplesParentDict = new Dictionary<Define.InputType, Transform>();

        private readonly Vector3 LEFT_ANSWER_PARENT_POS = new Vector3(-1.5f, -1f, 0);
        private readonly Vector3 RIGHT_ANSWER_PARENT_POS = new Vector3(1.5f, -1f, 0);

        public void Init()
        {
            answerDict.Add(Define.InputType.Left, new List<Define.Character>());
            answerDict.Add(Define.InputType.Right, new List<Define.Character>());

            var leftParent = new GameObject("LeftAnswer");
            var rightParent = new GameObject("RightAnswer");
            leftParent.transform.SetParent(transform);
            rightParent.transform.SetParent(transform);
            leftParent.transform.position = LEFT_ANSWER_PARENT_POS;
            rightParent.transform.position = RIGHT_ANSWER_PARENT_POS;
            
            answerSamplesParentDict[Define.InputType.Left] = leftParent.transform;
            answerSamplesParentDict[Define.InputType.Right] = rightParent.transform;
            
            board = GetComponent<GameBoard>();
            Managers.Game.onLevelUpdate += OnLevelUp;
        }

        public async UniTask AddAnswer(Define.InputType directionType, CharacterData cd)
        {
            answerDict[directionType].Add(cd.characterType);
            
            var poolable = await Managers.Pool.PopAsync("Prefab/Character");
            var character = poolable.GetComponent<Character>();
            var parent = answerSamplesParentDict[directionType];

            await character.Init(cd);
            
            character.transform.SetParent(parent);
            character.transform.localPosition = (Vector3.forward + Vector3.up) * parent.childCount;
            character.gameObject.SetActive(true);
        }

        public bool IsCorrectAnswer(Define.InputType directionType, Define.Character characterType)
        {
            return answerDict[directionType].Contains(characterType);
        }

        public Define.InputType GetCorrectAnswer(Define.Character characterType)
        {
            return answerDict[Define.InputType.Left].Contains(characterType) ? Define.InputType.Left : Define.InputType.Right;
        }

        public void OnLevelUp(int level)
        {
            var cd = board.AllCharacterDatas[level + 1];
            var inputType = level % 2 == 0 ? Define.InputType.Left : Define.InputType.Right;
            AddAnswer(inputType, cd);
        }
    }
}