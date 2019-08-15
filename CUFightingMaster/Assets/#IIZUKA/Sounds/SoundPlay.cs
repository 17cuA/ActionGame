using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public enum SoundsType
    {
        HitW,
        HitM,
        HitS,
        GuardW,
        GuardM,
        GuardS,
        PunchW,
        PunchM,
        PunchS,
        Jump,
        Step,
        Down,
        GetUp,
        Sutan,
        Special1,
        Special1_Hit,
        Ca_Hit,
    }
    public SoundsType soundsType;

    int i = 0;

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
                case SoundsType.HitW:
                    Sound.PlaySe("HitW", 1, 1);
                    break;
                case SoundsType.HitM:
                    Sound.PlaySe("HitM", 1, 1);
                    break;
                case SoundsType.HitS:
                    Sound.PlaySe("HitS", 1, 1);
                    break;
                case SoundsType.GuardW:
                    Sound.PlaySe("GuardW", 1, 1);
                    break;
                case SoundsType.GuardM:
                    Sound.PlaySe("GuardM", 1, 1);
                    break;
                case SoundsType.GuardS:
                    Sound.PlaySe("GuardS", 1, 1);
                    break;
                case SoundsType.PunchW:
                    Sound.PlaySe("PunchW", 1, 1);
                    break;
                case SoundsType.PunchM:
                    Sound.PlaySe("PunchM", 1, 1);
                    break;
                case SoundsType.PunchS:
                    Sound.PlaySe("PunchS", 1, 1);
                    break;
                case SoundsType.Jump:
                    Sound.PlaySe("Jump", 1, 1);
                    break;
                case SoundsType.Step:
                    Sound.PlaySe("Step", 1, 1);
                    break;
                case SoundsType.Down:
                    Sound.PlaySe("Down", 1, 1);
                    break;
                case SoundsType.GetUp:
                    Sound.PlaySe("GetUp", 1, 1);
                    break;
                case SoundsType.Sutan:
                    Sound.PlaySe("Sutan", 1, 1);
                    break;
                case SoundsType.Special1:
                    Sound.PlaySe("Special1", 1, 1);
                    break;
                case SoundsType.Special1_Hit:
                    Sound.PlaySe("Special1_Hit", 1, 1);
                    break;
                case SoundsType.Ca_Hit:
                    Sound.PlaySe("Ca_Hit", 1, 1);
                    break;
            }
            i++;
        }
    }
}
