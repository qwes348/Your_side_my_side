using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ysms.Game
{
    public class LevelController : MonoBehaviour
    {
        public void Init()
        {
            Managers.Game.onScoreUpdate += LevelUpCheck;
        }

        public void LevelUpCheck(int score)
        {
            if (Managers.Game.Level >= Define.MAX_LEVEL)
                return;
            
            if ((score / Define.LEVEL_UP_THRESHOLD) > Managers.Game.Level)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Managers.Game.Level++;
        }
    }
}