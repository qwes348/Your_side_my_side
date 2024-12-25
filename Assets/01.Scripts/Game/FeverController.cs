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

        private void Update()
        {
            if (feverTimer >= 0f)
            {
                feverTimer -= Time.deltaTime;
                if (feverTimer <= 0f && Managers.Game.FeverState != Define.FeverState.Normal)
                {
                    // 피버 지속시간 만료
                    Managers.Game.Combo = 0;
                }
            }
        }

        public void FeverUpdate(int combo)
        {
            switch (Managers.Game.FeverState)
            {
                case Define.FeverState.Normal:
                    if (combo >= Define.FEVER_THRESHOLD)
                    {
                        Managers.Game.FeverState = Define.FeverState.Fever;
                        Managers.Audio.SetBgmPitch(1);
                    }
                    break;
                case Define.FeverState.Fever:
                    if (combo >= Define.SUPER_FEVER_THRESHOLD)
                    {
                        Managers.Game.FeverState = Define.FeverState.SuperFever;
                        Managers.Audio.SetBgmPitch(2);
                        // 피버 시간 갱신
                        feverTimer = Define.FEVER_DURATION;
                    }
                    else if (combo == 0)
                    {
                        Managers.Game.FeverState = Define.FeverState.Normal;
                        Managers.Audio.SetBgmPitch(0);
                    }
                    else
                    {
                        feverTimer = Define.FEVER_DURATION;
                    }
                    break;
                case Define.FeverState.SuperFever:
                    if (combo == 0)
                    {
                        Managers.Game.FeverState = Define.FeverState.Normal;
                        Managers.Audio.SetBgmPitch(0);
                    }
                    else
                    {
                        feverTimer = Define.FEVER_DURATION;
                    }
                    break;
            }
        }
    }   
}
