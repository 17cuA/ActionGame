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
	public GameObject obj;
	public GameObject obj2;

	public GameObject[] camelas = new GameObject[4];
	public GameObject[] timelines = new GameObject[4];

    public GameObject[] animationUIManagers = new GameObject[2];
    public GameObject[] winOrlose = new GameObject[2];
    public Sprite win;
    public Sprite lose;
    public AnimationClip[] animationClips;
	public AnimationClip[] FighterClips = new AnimationClip[4];

    public Canvas canvas_1;
    [SerializeField] private ResultController resultController_1;

    public Canvas canvas_2;
    [SerializeField] private ResultController resultController_2;

    [SerializeField] private CanvasController_Result canvasController_Result;

    public GameObject[] targetPos = new GameObject[2];

	public CinemaController[] cinemaControllers = new CinemaController[4];
    public CinemaController cinemaController;

    private Action currentUpdate;

	private float sceneChangeTime = 10;
	private float time;

    void Awake()
    {
        canvasController_Result.InitDownCurtain();

        resultController_1 = canvas_1.transform.Find("ResultController").GetComponent<ResultController>();
        resultController_2 = canvas_2.transform.Find("ResultController").GetComponent<ResultController>();

		// キャラの生成
		obj = Instantiate(GameDataStrage.Instance.fighterStatuses[0].PlayerModel, targetPos[0].transform.position, targetPos[0].transform.rotation);
		obj2 = Instantiate(GameDataStrage.Instance.fighterStatuses[1].PlayerModel2, targetPos[1].transform.position, targetPos[0].transform.rotation);

		// １Pが勝ったら
		if (GameDataStrage.Instance.winFlag_PlayerOne == true)
		{
			// １Pの勝利カメラと２Pの敗北カメラをアクティブ
			camelas[0].SetActive(true);
			camelas[1].SetActive(false);
			camelas[2].SetActive(false);
			camelas[3].SetActive(true);

			// ２Pの敗北モーションをセット
			obj2.GetComponent<Animationdata>().ResultAnimation(FighterClips[3],0.5f,FighterClips[7]);
			obj2.GetComponent<Animationdata>().resultFlag = true;

			if (GameDataStrage.Instance.fighterStatuses[0].PlayerID == 0)
			{
				obj.GetComponent<Animationdata>().ResultAnimation(FighterClips[0],0.5f, FighterClips[4]);
				obj.GetComponent<Animationdata>().resultFlag = true;
				// 1Pのクリコ勝利タイムラインを表示
				timelines[0].SetActive(true);
				timelines[1].SetActive(false);
				timelines[2].SetActive(false);
				timelines[3].SetActive(false);
				cinemaController = cinemaControllers[0];
			}
			else if (GameDataStrage.Instance.fighterStatuses[0].PlayerID == 1)
			{
				obj.GetComponent<Animationdata>().ResultAnimation(FighterClips[2],0.5f, FighterClips[6]);
				obj.GetComponent<Animationdata>().resultFlag = true;
				// 1Pのおばちゃん勝利タイムラインを表示
				timelines[0].SetActive(false);
				timelines[1].SetActive(true);
				timelines[2].SetActive(false);
				timelines[3].SetActive(false);
				cinemaController = cinemaControllers[1];

			}
		}
		//2Pが勝ったら
		else if (GameDataStrage.Instance.winFlag_PlayerTwo == true)
		{
			// 1Pの敗北カメラと2Pの勝利カメラをアクティブ
			camelas[0].SetActive(false);
			camelas[1].SetActive(true);
			camelas[2].SetActive(true);
			camelas[3].SetActive(false);

			// １Pの敗北アニメーションをセット
			obj.GetComponent<Animationdata>().ResultAnimation(FighterClips[1],0.5f, FighterClips[5]);
			obj.GetComponent<Animationdata>().resultFlag = true;

			// 2Pのキャラがクリコだったら
			if (GameDataStrage.Instance.fighterStatuses[1].PlayerID == 0)
			{
				obj2.GetComponent<Animationdata>().ResultAnimation(FighterClips[0],0.5f, FighterClips[4]);
				obj2.GetComponent<Animationdata>().resultFlag = true;

				// 2Pのクリコ勝利タイムラインを表示
				timelines[0].SetActive(false);
				timelines[1].SetActive(false);
				timelines[2].SetActive(true);
				timelines[3].SetActive(false);
				cinemaController = cinemaControllers[2];
			}

			else if (GameDataStrage.Instance.fighterStatuses[1].PlayerID == 1)
			{
				obj2.GetComponent<Animationdata>().ResultAnimation(FighterClips[2],0.5f, FighterClips[6]);
				obj2.GetComponent<Animationdata>().resultFlag = true;

				// 2Pのおばちゃん勝利タイムラインを表示
				timelines[0].SetActive(false);
				timelines[1].SetActive(false);
				timelines[2].SetActive(false);
				timelines[3].SetActive(true);
				cinemaController = cinemaControllers[3];

			}
		}
		else if (GameDataStrage.Instance.winFlag_PlayerOne == false && GameDataStrage.Instance.winFlag_PlayerTwo == false)
		{
			obj.GetComponent<Animationdata>().ResultAnimation(FighterClips[1],0.5f, FighterClips[5]);
			obj2.GetComponent<Animationdata>().ResultAnimation(FighterClips[3],0.5f, FighterClips[7]);
            obj.GetComponent<Animationdata>().resultFlag = true;
            obj2.GetComponent<Animationdata>().resultFlag = true;

            camelas[0].SetActive(false);
			camelas[1].SetActive(true);
			camelas[2].SetActive(false);
			camelas[3].SetActive(true);
			cinemaController = null;
		}



		currentUpdate = UpCurtain;

		time = sceneChangeTime;
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
			if (cinemaController != null||cinemaController.isPlay == false)
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
        else if (GameDataStrage.Instance.winFlag_PlayerOne == false && GameDataStrage.Instance.winFlag_PlayerTwo == false)
        {
            winOrlose[1].GetComponent<Image>().sprite = lose;
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
		time -= Time.deltaTime;

		if (time < 0)
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
