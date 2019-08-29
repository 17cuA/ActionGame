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

public class CanvasController_Title : MonoBehaviour
{
    public Canvas canvas_Display1;
    public Canvas canvas_Display2;

    [SerializeField] private ScreenFade screenFade_Display1;
    [SerializeField] private ScreenFade screenFade_Display2;

    // Start is called before the first frame update

    private void Awake()
    {
        screenFade_Display1 = canvas_Display1.transform.Find("ScreenFade").GetComponent<ScreenFade>();
        screenFade_Display2 = canvas_Display2.transform.Find("ScreenFade").GetComponent<ScreenFade>();
    }

    /// <summary>
    /// 二画面を徐々に明るくする
    /// </summary>
    /// <returns>明るくなったらtrue</returns>
    public bool StartFadeIn()
    {
        bool isEndFadeIn_1 = screenFade_Display1.StartFadeIn();
        bool isEndFadeIn_2 = screenFade_Display2.StartFadeIn();

        if (isEndFadeIn_1 && isEndFadeIn_2)
            return true;
        return false;
    }

    /// <summary>
    /// 二画面を徐々に暗くする
    /// </summary>
    /// <returns>暗くなったらtrue</returns>
    public bool StartFadeOut()
    {
        bool isEndFadeOut_1 = screenFade_Display1.StartFadeOut();
        bool isEndFadeOut_2 = screenFade_Display2.StartFadeOut();

        if (isEndFadeOut_1 && isEndFadeOut_2)
            return true;
        return false;
    }

    /// <summary>
    /// 二画面を黒くする
    /// </summary>
    public void BrackOut()
    {
        screenFade_Display1.BrackOut();
        screenFade_Display2.BrackOut();
    }
}
