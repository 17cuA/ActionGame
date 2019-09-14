using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFunc : MonoBehaviour
{
    FuncKeyManager func;
    bool nowFlag = false;
    void Start()
    {
        if(FuncKeyManager.Instance != null)
        {
            func = FuncKeyManager.Instance;
            nowFlag = func.isOnCommandUI;
            if (func.isOnCommandUI)
            {
                GameManager.Instance.p1Command.gameObject.SetActive(false);
                GameManager.Instance.p2Command.gameObject.SetActive(false);
            }
            else
            {
                GameManager.Instance.p1Command.gameObject.SetActive(true);
                GameManager.Instance.p2Command.gameObject.SetActive(true);

            }
        }

    }

    void Update()
    {
        if (FuncKeyManager.Instance != null)
        {
            if (func.isOnCommandUI!=nowFlag)
            {
                if(func.isOnCommandUI)
                {
                    GameManager.Instance.p1Command.gameObject.SetActive(false);
                    GameManager.Instance.p2Command.gameObject.SetActive(false);
                }
                else
                {
                    GameManager.Instance.p1Command.gameObject.SetActive(true);
                    GameManager.Instance.p2Command.gameObject.SetActive(true);

                }
                nowFlag = func.isOnCommandUI;
            }
        }
    }
}
