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

		// EVA
		EV_ATField,
		EV_NExtStage,
		EV_EVA_Attack1,
		EV_EVA_Attack2,
		EV_EVA_Attack3,
		EV_EVA_Attack4,
		EV_EVA_Command1,
		EV_EVA_Command2,
		EV_EVA_Deathblow,
		EV_EVA_Guard,
		EV_EVA_Start,
		EV_EVA_Victory,
		EV_SHITO_Attack1,
		EV_SHITO_Attack2,
		EV_SHITO_Attack3,
		EV_SHITO_Command1,
		EV_SHITO_Command2,
		EV_SHITO_Command3,
		EV_SHITO_Deathblow,
		EV_SHITO_Start,
		EV_SHITO_Victory,
		EV_Shinji_RoundStart
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
                    Sound.PlaySE("HitL", 1, 0.5f, 1);
                    break;
                case SoundsType.HitM:
                    Sound.PlaySE("HitS", 1, 0.5f, 1);
                    break;
                case SoundsType.HitS:
                    Sound.PlaySE("HitS", 1, 0.5f, 1);
                    break;
                case SoundsType.GuardL:
                    Sound.PlaySE("GuardL", 2, 0.5f, 1);
                    break;
                case SoundsType.GuardM:
                    Sound.PlaySE("GuardM", 2, 0.5f, 1);
                    break;
                case SoundsType.GuardS:
                    Sound.PlaySE("GuardS", 2, 0.5f, 1);
                    break;
                case SoundsType.JabL:
                    Sound.PlaySE("JabL", 3, 0.5f, 1);
                    break;
                case SoundsType.JabM:
                    Sound.PlaySE("JabM", 3, 0.5f, 1);
                    break;
                case SoundsType.JabS:
                    Sound.PlaySE("JabS", 3, 0.5f, 1);
                    break;
                case SoundsType.Squat:
                    Sound.PlaySE("Squat", 4, 0.4f, 1);
                    break;
                case SoundsType.Jump:
                    Sound.PlaySE("Jump", 4, 0.5f, 1);
                    break;
                case SoundsType.Step:
                    Sound.PlaySE("Step", 5, 0.5f, 1);
                    break;
                case SoundsType.Down:
                    Sound.PlaySE("Down", 6, 0.5f, 1);
                    break;
                case SoundsType.DownCancel:
                    Sound.PlaySE("DownCancel", 7, 0.5f, 1);
                    break;
                case SoundsType.GetUp:
                    Sound.PlaySE("GetUp", 8, 0.5f, 1);
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
				case SoundsType.EV_ATField:
					Sound.PlaySE("EV_ATField", 2, 1, 1);
					break;
				case SoundsType.EV_EVA_Attack1:
					Sound.PlaySE("EV_EVA_Attack1", 14, 1, 1);
					break;
				case SoundsType.EV_EVA_Attack2:
					Sound.PlaySE("EV_EVA_Attack2", 14, 1, 1);
					break;
				case SoundsType.EV_EVA_Attack3:
					Sound.PlaySE("EV_EVA_Attack3", 14, 1, 1);
					break;
				case SoundsType.EV_EVA_Attack4:
					Sound.PlaySE("EV_EVA_Attack4", 14, 1, 1);
					break;
				case SoundsType.EV_EVA_Deathblow:
					Sound.PlaySE("EV_EVA_Deathblow", 16, 1, 1);
					break;
				case SoundsType.EV_EVA_Victory:
					Sound.PlaySE("EV_EVA_Victory", 16, 1, 1);
					break;
				case SoundsType.EV_EVA_Command1:
					Sound.PlaySE("EV_EVA_Command1", 17, 1, 1);
					break;
				case SoundsType.EV_EVA_Command2:
					Sound.PlaySE("EV_EVA_Command2", 17, 1, 1);
					break;
				case SoundsType.EV_EVA_Start:
					Sound.PlaySE("EV_EVA_Start", 19, 1, 1);
					break;
				case SoundsType.EV_SHITO_Attack1:
					Sound.PlaySE("EV_SHITO_Attack1", 14, 0.6f, 1);
					break;
				case SoundsType.EV_SHITO_Attack2:
					Sound.PlaySE("EV_SHITO_Attack2", 14, 0.6f, 1);
					break;
				case SoundsType.EV_SHITO_Attack3:
					Sound.PlaySE("EV_SHITO_Attack3", 14, 0.6f, 1);
					break;
				case SoundsType.EV_SHITO_Deathblow:
					Sound.PlaySE("EV_SHITO_Deathblow", 16, 0.6f, 1);
					break;
				case SoundsType.EV_SHITO_Victory:
					Sound.PlaySE("EV_SHITO_Victory", 16, 0.6f, 1);
					break;
				case SoundsType.EV_SHITO_Command1:
					Sound.PlaySE("EV_SHITO_Command1", 17, 0.6f, 1);
					break;
				case SoundsType.EV_SHITO_Command2:
					Sound.PlaySE("EV_SHITO_Command2", 17, 0.6f, 1);
					break;
				case SoundsType.EV_SHITO_Command3:
					Sound.PlaySE("EV_SHITO_Command3", 17, 0.6f, 1);
					break;
				case SoundsType.EV_SHITO_Start:
					Sound.PlaySE("EV_SHITO_Start", 19, 0.6f, 1);
					break;
				case SoundsType.EV_Shinji_RoundStart:
					Sound.PlaySE("EV_Shinji_RoundStart", 18, 3, 1);
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
