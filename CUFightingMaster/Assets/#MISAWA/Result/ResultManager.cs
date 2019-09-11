//---------------------------------------
// Result管理
//---------------------------------------
// 作成者:三沢
// 作成日:2019.08.14
//--------------------------------------
// 更新履歴
// 2019.08.14 作成
//--------------------------------------
// 仕様 
// 情報を受け取り、2つのCanvasに対応できるよう
// ResultControllerに投げる
//----------------------------------------
// MEMO 
// 勝敗判定(完成？)
// 勝敗時にランダムでキャラクターテキスト表示(未作成)
//----------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameObject[] animationUIManagers = new GameObject[2];
    public GameObject[] winOrlose = new GameObject[2];
    public Sprite win;
    public Sprite lose;
    public AnimationClip[] animationClips;

    public AnimationClip resultLose_clico;
    public AnimationClip resultWin_clico;
    public AnimationClip resultLose_oba;
    public AnimationClip resultWin_oba;

    public Canvas canvas_1;
    [SerializeField] private ResultController resultController_1;

    public Canvas canvas_2;
    [SerializeField] private ResultController resultController_2;

    [SerializeField] private CanvasController_Result canvasController_Result;

    public GameObject[] targetPos = new GameObject[2];

    public CinemaController cinemaController;

    private Action currentUpdate;

    void Awake()
    {
        canvasController_Result.InitDownCurtain();

        resultController_1 = canvas_1.transform.Find("ResultController").GetComponent<ResultController>();
        resultController_2 = canvas_2.transform.Find("ResultController").GetComponent<ResultController>();

        var obj = Instantiate(GameDataStrage.Instance.fighterStatuses[0].fighter, targetPos[0].transform.position, Quaternion.identity);
        obj.gameObject.layer = LayerMask.NameToLayer(CommonConstants.Layers.Player_One);
        var obj2 = Instantiate(GameDataStrage.Instance.fighterStatuses[1].fighter, targetPos[1].transform.position, Quaternion.identity);
        obj.gameObject.layer = LayerMask.NameToLayer(CommonConstants.Layers.Player_Two);

        //if (1Pが勝ったら)
        // {
        //    obj.GetComponent<NomalAnimationPlayer>().SetPlayAnimation(勝利アニメーション);
        //    obj2.GetComponent<NomalAnimationPlayer>().SetPlayAnimation(敗北アニメーション);
        //}
        //else
        //{
        //    obj.GetComponent<NomalAnimationPlayer>().SetPlayAnimation(敗北アニメーション);
        //    obj2.GetComponent<NomalAnimationPlayer>().SetPlayAnimation(勝利アニメーション);
        //}

        currentUpdate = UpCurtain;


    }

    void Update()
    {
        //ポーズ処理
        if (Mathf.Approximately(Time.timeScale, 0f)) return;

        currentUpdate();

    }

    //カーテンが上がる
    void UpCurtain()
    {
        if (canvasController_Result.UpCurtain())
        {
            if (cinemaController.isPlay == false)
            {
                currentUpdate = PlayUIAnime;
            }
        }

    }

    //ResultAnime
    void PlayUIAnime()
    {
        if (GameDataStrage.Instance.winFlag_PlayerOne)
        {
            winOrlose[0].GetComponent<Image>().sprite = win;
            winOrlose[1].GetComponent<Image>().sprite = lose;
            winOrlose[0].GetComponent<NomalAnimationPlayer>().SetPlayAnimation(animationClips[0], 1.0f, 0);
            winOrlose[1].GetComponent<NomalAnimationPlayer>().SetPlayAnimation(animationClips[1], 1.0f, 0);
        }
        else if (GameDataStrage.Instance.winFlag_PlayerTwo)
        {
            winOrlose[1].GetComponent<Image>().sprite = win;
            winOrlose[0].GetComponent<Image>().sprite = lose;
            winOrlose[0].GetComponent<NomalAnimationPlayer>().SetPlayAnimation(animationClips[1], 1.0f, 0);
            winOrlose[1].GetComponent<NomalAnimationPlayer>().SetPlayAnimation(animationClips[0], 1.0f, 0);
        }
        canvasController_Result.PlayUIAnime();
        currentUpdate = ResultUpdate;
    }

    void ResultUpdate()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
        {
            currentUpdate = DownCurtain;
        }
    }

    void DownCurtain()
    {
        if (canvasController_Result.DownCurtain())
        {
            currentUpdate = FadeOut;
        }
    }

    void FadeOut()
    {
        if (canvasController_Result.StartFadeOut())
        {
            SceneManager.LoadScene("JECLogo");
        }

    }
}
