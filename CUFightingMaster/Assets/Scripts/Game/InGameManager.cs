//---------------------------------------
// Gameシーン中の操作
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.12
//--------------------------------------
// 更新履歴
// 2019.07.12 作成
//--------------------------------------
// 仕様 
// 試合中にある場面を、区切って関数にしています。
// その関数を、デリゲードでcurrentUpdateに委託しています。
//----------------------------------------
// MEMO 
// 2画面対応について (0726 金沢)
// Canvasをまとめて操作するCanvasControllerの関数を呼び出し
// 各Canvasに表示・切り替えするように変更
//----------------------------------------
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{

    [SerializeField] private int gameRoundCount = 0;     //ラウンド数をカウント
    [SerializeField] private int winRound = 3;
    [SerializeField] private int getRoundCount_p1 = 0;   //プレイヤー1が取ったラウンド数
    [SerializeField] private int getRoundCount_p2 = 0;   //プレイヤー2が取ったラウンド数

    //Updateする場面
    private Action currentUpdate;

    //参照先
    [SerializeField] private CinemaController cinemaController;
    [SerializeField] private CharacterCreater characterCreater;
	//[SerializeField] private SceneController sceneController;
	[SerializeField] private CanvasController canvasController;

	[SerializeField] private CharacterStatus characterStatus_P1;
    [SerializeField] private CharacterStatus characterStatus_P2;

	public GameObject player1;
	public GameObject player2;

    /// <summary>
    /// 試合開始 
    /// </summary>
    private void StartGame()
    {
		Sound.AllSoundLod();
		Sound.PlayBgm("Bgm01", 0.5f, 1);
		//キャラクター生成(07/15一時的、生成処理が出来上がり次第、生成に変更してください)
		//if (characterStatus_P1 == null)
		//    characterStatus_P1 = GameObject.Find("Temp_Player01").GetComponent<CharacterStatus>();
		//if (characterStatus_P2 == null)
		//    characterStatus_P2 = GameObject.Find("Temp_Player02").GetComponent<CharacterStatus>();

		//BGMの再生(未実装)

		//カットシーンの再生（未実装）

		//カットシーンの再生が終わり、暗くなったら
		if (cinemaController.isPlay == false && canvasController.Check_StartFadeOut() == true)
            currentUpdate = StartRound;
    }

    /// <summary>
    /// ラウンド開始
    /// </summary>
    private void StartRound()
    {
        //ゲーム中のUI生成
        canvasController.Start_PlayBattleRound();

        //画面が明るくなったら
        if (canvasController.Check_StartFadeIn() == true)
        {
            //ラウンド開始時のUI生成
            if (canvasController.Start_PlayStartRound(gameRoundCount) == false)
            {
                //タイマーの開始
                canvasController.Start_CountDown();
                currentUpdate = BatlleRound;
            }
        }
    }

    /// <summary>
    /// ラウンド中
    /// </summary>
    private void BatlleRound()
    {
        //UIのhp表示の更新
        canvasController.Display_PlayerHP(GameManager.Instance.Player_one.HP, GameManager.Instance.Player_two.HP);

        //(どちらかのHPが0になったら
        if (GameManager.Instance.Player_one.HP <= 0 || GameManager.Instance.Player_two.HP <= 0)
        {
            //勝敗判定
            if (GameManager.Instance.Player_one.HP <= 0 && GameManager.Instance.Player_two.HP <= 0)
            {
                //DoubleKO
            }
            else if (GameManager.Instance.Player_one.HP > GameManager.Instance.Player_two.HP)
            {
                getRoundCount_p1++;
            }
            else
            {
                getRoundCount_p2++;
            }
            currentUpdate = FinishRound_KO;
        }

        //TimeOverになったら
        else if (canvasController.Check_EndCountDown() == false)
        {
            //勝敗判定
            if (GameManager.Instance.Player_one.HP == GameManager.Instance.Player_two.HP)
            {
                //DoubleKO
            }
            //勝敗判定
            else if (GameManager.Instance.Player_one.HP > GameManager.Instance.Player_two.HP)
            {
                getRoundCount_p1++;
            }
            else
            {
                getRoundCount_p2++;
            }
            currentUpdate = FinishRound_TimeOver;
        }
    }

    /// <summary>
    /// KOラウンド終了
    /// </summary>
    private void FinishRound_KO()
    {
        Debug.Log("FinishRound_KO");
        if (canvasController.Play_FinishRound_KO() == false)
        {
            currentUpdate = DoGameFinish;
        }
    }

    /// <summary>
    /// TimeOverでラウンド終了
    /// </summary>
    private void FinishRound_TimeOver()
    {
        if (canvasController.Play_FinishRound_TimeOver() == false)
        {
            currentUpdate = DoGameFinish;
        }
    }

    /// <summary>
    /// ゲームを終了するか、次のラウンドへ進むかの判定
    /// </summary>
    private void DoGameFinish()
    {
        //ゲームが終了するか判定
        if (getRoundCount_p1 >= winRound || getRoundCount_p2 >= winRound)
        {
            currentUpdate = GameVictory;
			Sound.StopBgm();
        }
        else
        {
            gameRoundCount++;
            currentUpdate = ResetParameter;
        }
    }

    /// <summary>
    /// 各パラメータのリセット
    /// </summary>
    private void ResetParameter()
    {
        //画面を暗くする
        if (canvasController.Check_StartFadeOut() == true)
        {
            //キャラクターのHPのリセット
            GameManager.Instance.Player_one.HP = 100;
            GameManager.Instance.Player_two.HP = 100;
            canvasController.Reset_UIParameter();

            //hpの初期化
            canvasController.Display_PlayerHP(GameManager.Instance.Player_one.HP, GameManager.Instance.Player_two.HP);

            //ラウンドカウンターの更新
            canvasController.Update_WinCounter(getRoundCount_p1, getRoundCount_p2);

			//キャラクターポジションのリセット(何故か1追加されたり減るため3.5f)
			player1.transform.position = new Vector3(-3.5f, 0, 0);
			player2.transform.position = new Vector3(3.5f, 0, 0);

            currentUpdate = StartRound;
        }
    }

    /// <summary>
    /// 勝敗判定
    /// </summary>
    private void GameVictory()
    {
        //ラウンドカウンターの更新
        canvasController.Update_WinCounter(getRoundCount_p1, getRoundCount_p2);

        if (getRoundCount_p1 > getRoundCount_p2)
        {
            if (canvasController.Display_Victory_winP1() == false)
                currentUpdate = GameFinish;
        }
        else
        {
            if(canvasController.Display_Victory_winP2() == false)
                currentUpdate = GameFinish;
        }
    }

    /// <summary>
    ///  試合終了
    /// </summary>
    private void GameFinish()
    {
        SceneManager.LoadScene("Title");
    }

    private void Awake()
    {
		cinemaController = GameObject.Find("CinemaControll").GetComponent<CinemaController>();
		//sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
		canvasController = GameObject.Find("CanvasController").GetComponent<CanvasController>();
        //一時的
        //characterStatus_P1 = GameObject.Find("Temp_Player01").GetComponent<CharacterStatus>();
        //characterStatus_P2 = GameObject.Find("Temp_Player02").GetComponent<CharacterStatus>();
    }

    void Start()
    {
        currentUpdate = StartGame;
    }

    void Update()
    {
        currentUpdate();

        //DebugKey
        if (Input.GetKeyDown("z"))
        {
            GameManager.Instance.Player_one.HP -= 5;
        }
        if (Input.GetKeyDown("x"))
        {
            GameManager.Instance.Player_one.HP -= 5;
        }
        if(Input.GetKeyDown("c"))
        {
            currentUpdate = ResetParameter;
            gameRoundCount = 0;
            getRoundCount_p1 = 0;
            getRoundCount_p2 = 0;
        }
    }
}
