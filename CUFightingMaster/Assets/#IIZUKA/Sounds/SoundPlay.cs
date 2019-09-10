using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public enum SoundsType
    {
        // SE
        None,
        HitL,
        HitM,
        HitS,
        GuardL,
        GuardM,
        GuardS,
        JabL,
        JabM,
        JabS,
        Squat,
        Jump,
        Step,
        Down,
        DownCancel,
        GetUp,
        Sutan,
        Special1,
        Special1_Hit,
        Ca_Hit,

        // CV_Oba
        Oba_CharacterSelect,
        Oba_GetUp_DownCancel,
        Oba_HitL,
        Oba_HitM,
        Oba_HitS_Down,
        Oba_JobL,
        Oba_JobM_Throw,
        Oba_JobS,
        Oba_Jump_Step,
        Oba_RoundDraw,
        Oba_RoundLoss,
        Oba_RoundWin,
        Oba_Special1,
        Oba_Special2,
        Oba_Special3,
        Oba_Special4,
        Oba_Thrown,
        Oba_RoundStart,

        // CV_Culico
        Clico_CharacterSelect,
        Clico_GetUp_DownCancel,
        Clico_HitL,
        Clico_HitM,
        Clico_HitS_Down,
        Clico_JobL,
        Clico_JobM_Throw,
        Clico_JobS,
        Clico_Jump_Step,
        Clico_RoundDraw,
        Clico_RoundLoss,
        Clico_RoundWin,
        Clico_Special1,
        Clico_Special2,
        Clico_Special3,
        Clico_Special4,
        Clico_Thrown,
        Clico_RoundStart,
    }
    public SoundsType soundsType;

    int i = 0;
    int timeCount = 0;
    public bool destroyFlag = true;
    public int frame = 30;

    void Start()
    {
        Sound.AllSoundLod();
    }

    void Update()
    {
        if (i == 0)
        {
            switch (soundsType)
            {
                case SoundsType.None:
                    break;
                case SoundsType.HitL:
                    Sound.PlaySe("HitL", 1, 1, 1);
                    break;
                case SoundsType.HitM:
                    Sound.PlaySe("HitS", 1, 1, 1);
                    break;
                case SoundsType.HitS:
                    Sound.PlaySe("HitS", 1, 1, 1);
                    break;
                case SoundsType.GuardL:
                    Sound.PlaySe("GuardL", 2, 1, 1);
                    break;
                case SoundsType.GuardM:
                    Sound.PlaySe("GuardM", 2, 1, 1);
                    break;
                case SoundsType.GuardS:
                    Sound.PlaySe("GuardS", 2, 1, 1);
                    break;
                case SoundsType.JabL:
                    Sound.PlaySe("JabL", 3, 0.7f, 1);
                    break;
                case SoundsType.JabM:
                    Sound.PlaySe("JabM", 3, 0.7f, 1);
                    break;
                case SoundsType.JabS:
                    Sound.PlaySe("JabS", 3, 0.7f, 1);
                    break;
                case SoundsType.Squat:
                    Sound.PlaySe("Squat", 4, 0.7f, 1);
                    break;
                case SoundsType.Jump:
                    Sound.PlaySe("Jump", 4, 0.5f, 1);
                    break;
                case SoundsType.Step:
                    Sound.PlaySe("Step", 5, 0.7f, 1);
                    break;
                case SoundsType.Down:
                    Sound.PlaySe("Down", 6, 1, 1);
                    break;
                case SoundsType.DownCancel:
                    Sound.PlaySe("DownCancel", 7, 0.7f, 1);
                    break;
                case SoundsType.GetUp:
                    Sound.PlaySe("GetUp", 8, 1, 1);
                    break;
                case SoundsType.Sutan:
                    Sound.PlaySe("Sutan", 1, 0.7f, 1);
                    break;
                case SoundsType.Special1:
                    Sound.PlaySe("Special1", 9, 1, 1);
                    break;
                case SoundsType.Special1_Hit:
                    Sound.PlaySe("Special1_Hit", 10, 1, 1);
                    break;
                case SoundsType.Ca_Hit:
                    Sound.PlaySe("Ca_Hit", 11, 1, 1);
                    break;


                case SoundsType.Oba_CharacterSelect:
                    Sound.PlaySe("Oba_CharacterSelect", 1, 1, 1);
                    break;
                case SoundsType.Oba_GetUp_DownCancel:
                    Sound.PlaySe("Oba_GetUp_DownCancel", 12, 1, 1);
                    break;
                case SoundsType.Oba_HitL:
                    Sound.PlaySe("HitS", 13, 1, 1);
                    break;
                case SoundsType.Oba_HitM:
                    Sound.PlaySe("HitS", 13, 1, 1);
					break;
                case SoundsType.Oba_HitS_Down:
                    Sound.PlaySe("HitS", 13, 1, 1);
                    break;
				case SoundsType.Oba_JobL:
					Sound.PlaySe("Oba_JobL", 14, 1, 1);
					break;
				case SoundsType.Oba_JobM_Throw:
					Sound.PlaySe("Oba_JobM_Throw", 14, 1, 1);
					break;
				case SoundsType.Oba_JobS:
					Sound.PlaySe("Oba_JobS", 14, 1, 1);
					break;
				case SoundsType.Oba_Jump_Step:
                    Sound.PlaySe("Oba_Jump_Step", 15, 1, 1);
                    break;
                case SoundsType.Oba_RoundDraw:
                    Sound.PlaySe("Oba_RoundDraw", 16, 1, 1);
                    break;
                case SoundsType.Oba_RoundLoss:
                    Sound.PlaySe("Oba_RoundLoss", 16, 1, 1);
                    break;
                case SoundsType.Oba_RoundWin:
                    Sound.PlaySe("Oba_RoundWin", 16, 1, 1);
                    break;
                case SoundsType.Oba_Special1:
                    Sound.PlaySe("Oba_Special1", 17, 1, 1);
                    break;
                case SoundsType.Oba_Special2:
                    Sound.PlaySe("Oba_Special2", 17, 1, 1);
                    break;
                case SoundsType.Oba_Special3:
                    Sound.PlaySe("Oba_Special3", 17, 1, 1);
                    break;
                case SoundsType.Oba_Special4:
                    Sound.PlaySe("Oba_Special4", 17, 1, 1);
                    break;
                case SoundsType.Oba_Thrown:
                    Sound.PlaySe("Oba_Thrown", 18, 1, 1);
                    break;
                case SoundsType.Oba_RoundStart:
                    Sound.PlaySe("Oba_RoundStart", 19, 1, 1);
                    break;

                case SoundsType.Clico_CharacterSelect:
                    Sound.PlaySe("Clico_CharacterSelect", 1, 1, 1);
                    break;
                case SoundsType.Clico_GetUp_DownCancel:
                    Sound.PlaySe("Clico_GetUp_DownCancel", 12, 1, 1);
                    break;
                case SoundsType.Clico_HitL:
                    Sound.PlaySe("Clico_HitL", 13, 1, 1);
                    break;
                case SoundsType.Clico_HitM:
                    Sound.PlaySe("Clico_HitM", 13, 1, 1);
                    break;
                case SoundsType.Clico_HitS_Down:
                    Sound.PlaySe("Clico_HitS_Down", 13, 1, 1);
                    break;
                case SoundsType.Clico_JobL:
                    Sound.PlaySe("Clico_JobL", 14, 1, 1);
                    break;
                case SoundsType.Clico_JobM_Throw:
                    Sound.PlaySe("Clico_JobM_Throw", 14, 1, 1);
                    break;
                case SoundsType.Clico_JobS:
                    Sound.PlaySe("Clico_JobS", 14, 1, 1);
                    break;
                case SoundsType.Clico_Jump_Step:
                    Sound.PlaySe("Clico_Jump_Step", 15, 1, 1);
                    break;
                case SoundsType.Clico_RoundDraw:
                    Sound.PlaySe("Clico_RoundDraw", 16, 1, 1);
                    break;
                case SoundsType.Clico_RoundLoss:
                    Sound.PlaySe("Clico_RoundLoss", 16, 1, 1);
                    break;
                case SoundsType.Clico_RoundWin:
                    Sound.PlaySe("Clico_RoundWin", 16, 1, 1);
                    break;
                case SoundsType.Clico_Special1:
                    Sound.PlaySe("Clico_Special1", 17, 1, 1);
                    break;
                case SoundsType.Clico_Special2:
                    Sound.PlaySe("Clico_Special2", 17, 1, 1);
                    break;
                case SoundsType.Clico_Special3:
                    Sound.PlaySe("Clico_Special3", 17, 1, 1);
                    break;
                case SoundsType.Clico_Special4:
                    Sound.PlaySe("Clico_Special4", 17, 1, 1);
                    break;
                case SoundsType.Clico_Thrown:
                    Sound.PlaySe("Clico_Thrown", 18, 1, 1);
                    break;
                case SoundsType.Clico_RoundStart:
                    Sound.PlaySe("Clico_RoundStart", 19, 1, 1);
                    break;

            }
            i++;
        }

        if (destroyFlag)
        {
            DestroyObject(frame);
        }
    }

    void DestroyObject(int frame)
    {
        timeCount += 1;
        if (timeCount == frame)
        {
            Destroy(gameObject);
        }
    }
}
