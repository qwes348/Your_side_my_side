using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ysms.Game
{
    public class FeverController : MonoBehaviour
    {
        private float feverTimer;
        
        public void Init()
        {
            feverTimer = 0f;
            Managers.Game.onComboUpdate += FeverUpdate;
        }

        public void FeverUpdate(int combo)
        {
            switch (Managers.Game.FeverState)
            {
                case Define.FeverState.Normal:
                    if (combo >= Define.FEVER_THRESHOLD)
                    {
                        Managers.Game.FeverState = Define.FeverState.Fever;
                    }
                    break;
                case Define.FeverState.Fever:
                    if (combo >= Define.SUPER_FEVER_THRESHOLD)
                    {
                        Managers.Game.FeverState = Define.FeverState.SuperFever;
                    }
                    else if (combo == 0)
                    {
                        Managers.Game.FeverState = Define.FeverState.Normal;
                    }
                    break;
                case Define.FeverState.SuperFever:
                    if (combo == 0)
                    {
                        Managers.Game.FeverState = Define.FeverState.Normal;
                    }
                    break;
            }
        }
    }   
}
