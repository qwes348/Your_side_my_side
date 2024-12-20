using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Ysms.Game
{
    public class BombGaugeController : MonoBehaviour
    {
        [SerializeField]
        private Slider bombSlider;
        
        private float bombGauge;
        private GameBoard board;
        private LineController line;
        
        public void Init()
        {
            bombSlider.value = 0f;
            Managers.Game.onScoreUpdate += OnScoreUpdate;
            board = GetComponent<GameBoard>();
            line = GetComponent<LineController>();
        }

        public void OnScoreUpdate(int score)
        {
            bombGauge += (1 + Managers.Game.Combo * 0.01f);
            bombSlider.value = bombGauge / 100f;
            if (bombGauge > 100f)
            {
                bombGauge = 0f;
                MakeBomb();
            }
        }

        private void MakeBomb()
        {
            var bombData = board.AllSpecialDatas.First(data => data.characterType == Define.Character.Bomb);
            line.ChangeLineCharacter(2, bombData);
        }
    }
}