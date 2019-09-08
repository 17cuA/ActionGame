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
    public float curtainSpeed = 10.0f;

    [SerializeField] public RectTransform rectTransform;

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
}
//    private void Update()
//    {
//       // if (Input.GetKeyDown("x"))
//        {
//    //        if(DownCurtain())
//    //        {
//    //            Debug.Log("a");
//    //        }
//    //    }
//    //    if (Input.GetKeyDown("c"))
//    //    {
//    //        DownCurtain();
//    //    }
//    //}
//}

