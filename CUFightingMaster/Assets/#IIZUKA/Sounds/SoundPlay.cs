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

		// CV_OBA
		Oba_CharacterSelect,
		Oba_GetUp,
		Oba_HitL,
		Oba_HitM,
		Oba_HitS,
		Oba_JobL,
		Oba_JobM,
		Oba_JobS,
		Oba_Jump,
		Oba_RoundDraw,
		Oba_RoundLoss,
		Oba_RoundWin,
		Oba_Special1,
		Oba_Special2,
		Oba_Special3,
		Ova_Special4,
		Oba_Thrown,
		Ova_RoundStart,

		// CV_kuriko
		kuriko_CharacterSelect,
		kuriko_GetUp,
		kuriko_HitL,
		kuriko_HitM,
		kuriko_HitS,
		kuriko_obL,
		kuriko_JobM,
		kuriko_JobS,
		kuriko_Jump,
		kuriko_RoundDraw,
		kuriko_RoundLoss,
		kuriko_RoundWin,
		kuriko_Special1,
		kuriko_Special2,
		kuriko_Special3,
		Okuriko_Special4,
		kuriko_Thrown,
		kuriko_RoundStart,
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
				// SE
                case SoundsType.HitL:
                    Sound.PlaySe("HitL", 1, 1, 1);
                    break;
                case SoundsType.HitM:
                    Sound.PlaySe("HitM", 1, 1, 1);
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
				// CV_Oba
				case SoundsType.Oba_CharacterSelect:
					Sound.PlaySe("Oba_CharacterSelect", 1, 1, 1);
					break;
				case SoundsType.Oba_GetUp:
					Sound.PlaySe("Oba_GetUp", 12, 1, 1);
					break;
				case SoundsType.Oba_HitL:
					Sound.PlaySe("Oba_HitL", 13, 1, 1);
					break;
				case SoundsType.Oba_HitM:
					Sound.PlaySe("Oba_HitM", 13, 1, 1);
					break;
				case SoundsType.Oba_HitS:
					Sound.PlaySe("Oba_HitS", 13, 1, 1);
					break;
				case SoundsType.Oba_JobL:
					Sound.PlaySe("Oba_JobL", 14, 1, 1);
					break;
				case SoundsType.Oba_JobM:
					Sound.PlaySe("Oba_JobM", 14, 1, 1);
					break;
				case SoundsType.Oba_JobS:
					Sound.PlaySe("Oba_JobS", 14, 1, 1);
					break;
				case SoundsType.Oba_Jump:
					Sound.PlaySe("Oba_Jump", 15, 1, 1);
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
				case SoundsType.Ova_Special4:
					Sound.PlaySe("Ova_Special4", 17, 1, 1);
					break;
				case SoundsType.Oba_Thrown:
					Sound.PlaySe("Oba_Thrown", 18, 1, 1);
					break;
				case SoundsType.Ova_RoundStart:
					Sound.PlaySe("Ova_RoundStart", 19, 1, 1);
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
