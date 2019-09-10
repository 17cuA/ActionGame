////---------------------------------------
//// 2画面同時処理
////---------------------------------------
//// 作成者:高野
//// 作成日:2019.08.24
////--------------------------------------
//// 更新履歴
//// 2019.08.24 作成
////--------------------------------------
//// 仕様 
////----------------------------------------
//// MEMO 
////----------------------------------------

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CanvasController_Title : MonoBehaviour
//{
//    public Canvas canvas_Display1;
//    public Canvas canvas_Display2;

//    [SerializeField] private ScreenFade screenFade_Display1;
//    [SerializeField] private ScreenFade screenFade_Display2;
//    [SerializeField] private CurtainMover curtainMover_1;
//    [SerializeField] private CurtainMover curtainMover_2;
//    [SerializeField] private MediaPlayer mediaPlayer_1;
//    [SerializeField] private MediaPlayer mediaPlayer_2;

//    // Start is called before the first frame update

//    private void Awake()
//    {
//		if (canvas_Display1 == null || canvas_Display2 == null)
//			Debug.LogError("参照ミス : CanvacControllerにCanvasを追加してください");

//		screenFade_Display1 = canvas_Display1.transform.Find("ScreenFade").GetComponent<ScreenFade>();
//        screenFade_Display2 = canvas_Display2.transform.Find("ScreenFade").GetComponent<ScreenFade>();
//        curtainMover_1 = canvas_Display1.transform.Find("Curtain").GetComponent<CurtainMover>();
//        curtainMover_2 = canvas_Display2.transform.Find("Curtain").GetComponent<CurtainMover>();
//        mediaPlayer_1 = canvas_Display1.transform.Find("MediaPlayer").GetComponent<MediaPlayer>();
//        mediaPlayer_2 = canvas_Display2.transform.Find("MediaPlayer").GetComponent<MediaPlayer>();
//    }

//    /// <summary>
//    /// 二画面を徐々に明るくする
//    /// </summary>
//    /// <returns>明るくなったらtrue</returns>
//    public bool StartFadeIn()
//    {
//        bool isEndFadeIn_1 = screenFade_Display1.StartFadeIn();
//        bool isEndFadeIn_2 = screenFade_Display2.StartFadeIn();

//        if (isEndFadeIn_1 && isEndFadeIn_2)
//            return true;
//        return false;
//    }

//    /// <summary>
//    /// 二画面を徐々に暗くする
//    /// </summary>
//    /// <returns>暗くなったらtrue</returns>
//    public bool StartFadeOut()
//    {
//        bool isEndFadeOut_1 = screenFade_Display1.StartFadeOut();
//        bool isEndFadeOut_2 = screenFade_Display2.StartFadeOut();

//        if (isEndFadeOut_1 && isEndFadeOut_2)
//            return true;
//        return false;
//    }

//    /// <summary>
//    /// 二画面を黒くする
//    /// </summary>
//    public void BrackOut()
//    {
//        screenFade_Display1.BrackOut();
//        screenFade_Display2.BrackOut();
//    }


//    /// <summary>
//    ///徐々に 幕を開ける
//    /// </summary>
//    public bool UpCurtain()
//    {
//        bool isEnd1 = curtainMover_1.UpCurtain();
//        bool isEnd2 = curtainMover_2.UpCurtain();

//        if (isEnd1 && isEnd2)
//            return true;
//        return false;
//    }

//    /// <summary>
//    ///徐々に幕を下ろす
//    /// </summary>
//    public bool DownCurtain()
//    {
//        bool isEnd1 = curtainMover_1.DownCurtain();
//        bool isEnd2 = curtainMover_2.DownCurtain();

//        if (isEnd1 && isEnd2)
//            return true;
//        return false;
//    }

//    /// <summary>
//    ///一気に幕を下ろす
//    /// </summary>
//    public void InitDownCurtain()
//    {
//        curtainMover_1.InitDownCurtain();
//        curtainMover_2.InitDownCurtain();
//    }

//    public void PlayVideo()
//    {
//        mediaPlayer_1.PlayVideo();
//        mediaPlayer_2.PlayVideo()
//    }

//    public void EndPlayVideo()
//    {
//        mediaPlayer_1.PlayVideo();
//        mediaPlayer_2.PlayVideo();
//    }
       
//    public void SetEndPlayCallBack( Action)
//}
