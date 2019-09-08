//---------------------------------------
// 幕の移動
//---------------------------------------
// 作成者:高野
// 作成日:2019.09.08
//--------------------------------------
// 更新履歴
// :2019.09.08 作成
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainMover : MonoBehaviour
{
   [SerializeField] private bool isUp = false;
    [SerializeField] private bool isDown = false;
    [SerializeField] private bool isEndMove = false;

    private float curtainSpeed = 5.0f;

    public RectTransform rectTransform;

    public bool UpCurtain()
    {
        if (rectTransform.localPosition.y > 1110)
        {
            return true;
        }
        rectTransform.localPosition += new Vector3(0.0f, curtainSpeed, 0.0f);
        return false;
    }

    public bool DownCurtain()
    {
        if (rectTransform.localPosition.y < 25)
        {
            return true;
        }
        rectTransform.localPosition -= new Vector3(0.0f, curtainSpeed, 0.0f);
        return false;
    }

    public void InitDownCurtain()
    {
        rectTransform.localPosition = new Vector3(0.0f, 25.0f, 0.0f);
    }

    private void Update()
    {
        //    if(isEndMove == false)
        //    {
        //        //カーテンを上げるフラグがONだったら
        //        if (isUp == true)
        //        { 
        //            rectTransform.localPosition += new Vector3(0.0f, curtainSpeed, 0.0f);
        //            if(rectTransform.localPosition.y > 1080)
        //            {
        //                isUp = false;
        //                isEndMove = true;
        //            }
        //        }
        //        else if (isDown == true)
        //        {
        //            rectTransform.localPosition -= new Vector3(0.0f, curtainSpeed, 0.0f);
        //            if (rectTransform.localPosition.y < 0)
        //            {
        //                isDown = false;
        //                isEndMove = true;
        //            }
        //        }
        //    }
        //    if (Input.GetKeyDown("x"))
        //    {
        //        UpCurtain();
        //    }
        //    if (Input.GetKeyDown("c"))
        //    {
        //        DownCurtain();
        //    }
        //}
    }
}
