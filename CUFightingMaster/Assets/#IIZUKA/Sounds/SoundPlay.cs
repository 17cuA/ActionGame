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

		// CV_Kuidaore
		Kuidaore_CharacterSelect,
		Kuidaore_GetUp_DownCancel,
		Kuidaore_HitL,
		Kuidaore_HitM,
		Kuidaore_HitS_Down,
		Kuidaore_JobL,
		Kuidaore_JobM_Throw,
		Kuidaore_JobS,
		Kuidaore_Jump_Step,
		Kuidaore_RoundDraw,
		Kuidaore_RoundLoss,
		Kuidaore_RoundWin,
		Kuidaore_Special1,
		Kuidaore_Special2,
		Kuidaore_Special3,
		Kuidaore_Special4,
		Kuidaore_Thrown,
		Kuidaore_RoundStart,

		Oba_Hit,
		Clico_Tackle,
        Beams,
        Nice,
        Great,
        Excellent,

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
                    Sound.PlaySE("HitL", 1, 1, 1);
                    break;
                case SoundsType.HitM:
                    Sound.PlaySE("HitS", 1, 1, 1);
                    break;
                case SoundsType.HitS:
                    Sound.PlaySE("HitS", 1, 1, 1);
                    break;
                case SoundsType.GuardL:
                    Sound.PlaySE("GuardL", 2, 1, 1);
                    break;
                case SoundsType.GuardM:
                    Sound.PlaySE("GuardM", 2, 1, 1);
                    break;
                case SoundsType.GuardS:
                    Sound.PlaySE("GuardS", 2, 1, 1);
                    break;
                case SoundsType.JabL:
                    Sound.PlaySE("JabL", 3, 0.8f, 1);
                    break;
                case SoundsType.JabM:
                    Sound.PlaySE("JabM", 3, 0.8f, 1);
                    break;
                case SoundsType.JabS:
                    Sound.PlaySE("JabS", 3, 0.8f, 1);
                    break;
                case SoundsType.Squat:
                    Sound.PlaySE("Squat", 4, 0.4f, 1);
                    break;
                case SoundsType.Jump:
                    Sound.PlaySE("Jump", 4, 0.6f, 1);
                    break;
                case SoundsType.Step:
                    Sound.PlaySE("Step", 5, 0.6f, 1);
                    break;
                case SoundsType.Down:
                    Sound.PlaySE("Down", 6, 1, 1);
                    break;
                case SoundsType.DownCancel:
                    Sound.PlaySE("DownCancel", 7, 0.6f, 1);
                    break;
                case SoundsType.GetUp:
                    Sound.PlaySE("GetUp", 8, 0.6f, 1);
                    break;
                case SoundsType.Sutan:
                    Sound.PlaySE("Sutan", 1, 0.3f, 1);
                    break;
                case SoundsType.Special1:
                    Sound.PlaySE("Special1", 9, 0.5f, 1);
                    break;
                case SoundsType.Special1_Hit:
                    Sound.PlaySE("Special1_Hit", 10, 0.5f, 1);
                    break;
                case SoundsType.Ca_Hit:
                    Sound.PlaySE("Ca_Hit", 11, 0.5f, 1);
                    break;


                case SoundsType.Oba_CharacterSelect:
                    Sound.PlaySE("Oba_CharacterSelect", 1, 0.5f, 1);
                    break;
                case SoundsType.Oba_GetUp_DownCancel:
                    Sound.PlaySE("Oba_GetUp_DownCancel", 12, 0.5f, 1);
                    break;
                case SoundsType.Oba_HitL:
                    Sound.PlaySE("HitS", 13, 1, 1);
                    break;
                case SoundsType.Oba_HitM:
                    Sound.PlaySE("HitS", 13, 1, 1);
					break;
                case SoundsType.Oba_HitS_Down:
                    Sound.PlaySE("HitS", 13, 1, 1);
                    break;

				case SoundsType.Oba_Hit:
					Sound.PlaySE("Oba_HitS_Down", 13, 0.3f, 1);
					break;


				case SoundsType.Oba_JobL:
					Sound.PlaySE("Oba_JobL", 14, 0.5f, 1);
					break;
				case SoundsType.Oba_JobM_Throw:
					Sound.PlaySE("Oba_JobM_Throw", 14, 0.5f, 1);
					break;
				case SoundsType.Oba_JobS:
					Sound.PlaySE("Oba_JobS", 14, 0.5f, 1);
					break;
				case SoundsType.Oba_Jump_Step:
                    Sound.PlaySE("Oba_Jump_Step", 15, 0.5f, 1);
                    break;
                case SoundsType.Oba_RoundDraw:
                    Sound.PlaySE("Oba_RoundDraw", 16, 0.5f, 1);
                    break;
                case SoundsType.Oba_RoundLoss:
                    Sound.PlaySE("Oba_RoundLoss", 16, 1, 1);
                    break;
                case SoundsType.Oba_RoundWin:
                    Sound.PlaySE("Oba_RoundWin", 16, 0.5f, 1);
                    break;
                case SoundsType.Oba_Special1:
                    Sound.PlaySE("Oba_Special1", 17, 0.7f, 1);
                    break;
                case SoundsType.Oba_Special2:
                    Sound.PlaySE("Oba_Special2", 14, 0.7f, 1);
                    break;
                case SoundsType.Oba_Special3:
                    Sound.PlaySE("Oba_Special3", 17, 1, 1);
                    break;
                case SoundsType.Oba_Special4:
                    Sound.PlaySE("Oba_Special4", 17, 0.7f, 1);
                    break;
                case SoundsType.Oba_Thrown:
                    Sound.PlaySE("Oba_Thrown", 18, 0.5f, 1);
                    break;
                case SoundsType.Oba_RoundStart:
						Sound.PlaySE("Oba_RoundStart", 19, 1, 1);
                    break;

                case SoundsType.Clico_CharacterSelect:
                    Sound.PlaySE("Clico_CharacterSelect", 1, 0.5f, 1);
                    break;
                case SoundsType.Clico_GetUp_DownCancel:
                    Sound.PlaySE("Clico_GetUp_DownCancel", 12, 0.5f, 1);
                    break;
                case SoundsType.Clico_HitL:
                    Sound.PlaySE("Clico_HitL", 13, 0.3f, 1);
                    break;
                case SoundsType.Clico_HitM:
                    Sound.PlaySE("Clico_HitM", 13, 0.3f, 1);
                    break;
                case SoundsType.Clico_HitS_Down:
                    Sound.PlaySE("Clico_HitS_Down", 13, 0.3f, 1);
                    break;
                case SoundsType.Clico_JobL:
                    Sound.PlaySE("Clico_JobL", 14, 0.5f, 1);
                    break;
                case SoundsType.Clico_JobM_Throw:
                    Sound.PlaySE("Clico_JobM_Throw", 14, 0.5f, 1);
                    break;
                case SoundsType.Clico_JobS:
                    Sound.PlaySE("Clico_JobS", 14, 0.5f, 1);
                    break;
                case SoundsType.Clico_Jump_Step:
                    Sound.PlaySE("Clico_Jump_Step", 15, 0.5f, 1);
                    break;
                case SoundsType.Clico_RoundDraw:
                    Sound.PlaySE("Clico_RoundDraw", 16, 0.5f, 1);
                    break;
                case SoundsType.Clico_RoundLoss:
                    Sound.PlaySE("Clico_RoundLoss", 16, 1, 1);
                    break;
                case SoundsType.Clico_RoundWin:
                    Sound.PlaySE("Clico_RoundWin", 16, 0.5f, 1);
					break;
                case SoundsType.Clico_Special1:
                    Sound.PlaySE("Clico_Special1", 17, 0.7f, 1);
                    break;
                case SoundsType.Clico_Special2:
                    Sound.PlaySE("Clico_Special2", 17, 0.7f, 1);
                    break;
                case SoundsType.Clico_Special3:
                    Sound.PlaySE("Clico_Special3", 17, 0.7f, 1);
                    break;
                case SoundsType.Clico_Special4:
                    Sound.PlaySE("Clico_Special4", 17, 0.7f, 1);
                    break;
                case SoundsType.Clico_Thrown:
                    Sound.PlaySE("Clico_Thrown", 18, 0.5f, 1);
                    break;
                case SoundsType.Clico_RoundStart:
					Sound.PlaySE("Clico_RoundStart", 18, 1, 1);
					break;
				case SoundsType.Clico_Tackle:
					Sound.PlaySE("Clico_Tackle", 19, 0.5f, 1);
					break;
				case SoundsType.Kuidaore_CharacterSelect:
					Sound.PlaySE("Kuidaore_CharacterSelect", 1, 1f, 1);
					break;
				case SoundsType.Kuidaore_GetUp_DownCancel:
					Sound.PlaySE("Kuidaore_GetUp_DownCancel", 12, 1f, 1);
					break;
				case SoundsType.Kuidaore_HitL:
					Sound.PlaySE("Kuidaore_HitL", 13, 1f, 1);
					break;
				case SoundsType.Kuidaore_HitM:
					Sound.PlaySE("Kuidaore_HitM", 13, 1f, 1);
					break;
				case SoundsType.Kuidaore_HitS_Down:
					Sound.PlaySE("Kuidaore_HitS_Down", 13, 1f, 1);
					break;
				case SoundsType.Kuidaore_JobL:
					Sound.PlaySE("Kuidaore_JobL", 14, 1f, 1);
					break;
				case SoundsType.Kuidaore_JobM_Throw:
					Sound.PlaySE("Kuidaore_JobM_Throw", 14, 1f, 1);
					break;
				case SoundsType.Kuidaore_JobS:
					Sound.PlaySE("Kuidaore_JobS", 14, 1f, 1);
					break;
				case SoundsType.Kuidaore_Jump_Step:
					Sound.PlaySE("Kuidaore_Jump_Step", 15, 1f, 1);
					break;
				case SoundsType.Kuidaore_RoundDraw:
					Sound.PlaySE("Kuidaore_RoundDraw", 16, 1f, 1);
					break;
				case SoundsType.Kuidaore_RoundLoss:
					Sound.PlaySE("Kuidaore_RoundLoss", 16, 1, 1);
					break;
				case SoundsType.Kuidaore_RoundWin:
					Sound.PlaySE("Kuidaore_RoundWin", 16, 1f, 1);
					break;
				case SoundsType.Kuidaore_Special1:
					Sound.PlaySE("Kuidaore_Special1", 17, 1f, 1);
					break;
				case SoundsType.Kuidaore_Special2:
					Sound.PlaySE("Kuidaore_Special2", 17, 1f, 1);
					break;
				case SoundsType.Kuidaore_Special3:
					Sound.PlaySE("Kuidaore_Special3", 17, 1f, 1);
					break;
				case SoundsType.Kuidaore_Special4:
					Sound.PlaySE("Kuidaore_Special4", 17, 1f, 1);
					break;
				case SoundsType.Kuidaore_Thrown:
					Sound.PlaySE("Kuidaore_Thrown", 18, 1f, 1);
					break;
				case SoundsType.Kuidaore_RoundStart:
					Sound.PlaySE("Kuidaore_RoundStart", 18, 1, 1);
					break;

				case SoundsType.Beams:
                    Sound.PlaySE("SE_Beams", 18, 0.5f, 1);
                    break;
                case SoundsType.Nice:
                    Sound.PlaySE("SV_Nice", 19, 1, 1);
                    break;
                case SoundsType.Great:
                    Sound.PlaySE("SV_Great", 19, 0.5f, 1);
                    break;
                case SoundsType.Excellent:
                    Sound.PlaySE("SV_Excellent", 19, 0.5f, 1);
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
