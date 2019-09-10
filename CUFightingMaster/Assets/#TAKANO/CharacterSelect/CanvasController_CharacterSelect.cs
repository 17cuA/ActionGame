//---------------------------------------
// 2画面同時処理
//---------------------------------------
// 作成者:高野
// 作成日:2019.08.24
//--------------------------------------
// 更新履歴
// 2019.08.24 作成
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
//----------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController_CharacterSelect : MonoBehaviour
{
    public Canvas canvas_Display1;
    public Canvas canvas_Display2;

    //[SerializeField] private ScreenFade screenFade_Display1;
    //[SerializeField] private ScreenFade screenFade_Display2;
    [SerializeField] private CurtainMover curtainMover_1;
    [SerializeField] private CurtainMover curtainMover_2;

    // Start is called before the first frame update

    private void Awake()
    {
        //if (canvas_Display1 == null || canvas_Display2 == null)
        //    Debug.LogError("参照ミス : CanvacControllerにCanvasを追加してください");

        curtainMover_1 = canvas_Display1.transform.Find("Curtain").GetComponent<CurtainMover>();
        curtainMover_2 = canvas_Display2.transform.Find("Curtain").GetComponent<CurtainMover>();
    }

    /// <summary>
    /// 二画面を黒くする
    /// </summary>
    //public void BrackOut()
    //{
    //    screenFade_Display1.BrackOut();
    //    screenFade_Display2.BrackOut();
    //}

    /// <summary>
    ///徐々に 幕を開ける
    /// </summary>
    public bool UpCurtain()
    {
        bool isEnd1 = curtainMover_1.UpCurtain();
        bool isEnd2 = curtainMover_2.UpCurtain();

        if (isEnd1 && isEnd2)
            return true;
        return false;
    }

    /// <summary>
    ///徐々に幕を下ろす
    /// </summary>
    public bool DownCurtain()
    {
        bool isEnd1 = curtainMover_1.DownCurtain();
        bool isEnd2 = curtainMover_2.DownCurtain();

        if (isEnd1 && isEnd2)
            return true;
        return false;
    }

    /// <summary>
    ///一気に幕を下ろす
    /// </summary>
    public void InitDownCurtain()
    {
        curtainMover_1.InitDownCurtain();
        curtainMover_2.InitDownCurtain();
    }
}
