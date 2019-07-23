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
    [SerializeField] private InGameUIController inGameUIController;
    [SerializeField] private CharacterCreater characterCreater;
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private SceneController sceneController;

    [SerializeField] private CharacterStatus characterStatus_P1;
    [SerializeField] private CharacterStatus characterStatus_P2;

    public int playerMaxhp = 100;
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
		if (cinemaController.isPlay == false && screenFade.StartFadeOut() == true)
            currentUpdate = StartRound;
    }

    /// <summary>
    /// ラウンド開始
    /// </summary>
    private void StartRound()
    {
        //ゲーム中のUI生成
        inGameUIController.PlayBatlleRound();

        //画面が明るくなったら
        if (screenFade.StartFadeIn() == true)
        {
            //ラウンド開始時のUI生成
            if (inGameUIController.PlayStartRound(gameRoundCount) == false)
            {
                //タイマーの開始
                inGameUIController.StartCoundDouwn();
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
        inGameUIController.DisplayPlayerHp(GameManager.Instance.Player_one.HP, GameManager.Instance.Player_two.HP);

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
        else if (inGameUIController.DoEndCountDown() == false)
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
        if (inGameUIController.PlayFinishRound__KO() == false)
        {
            currentUpdate = DoGameFinish;
        }
    }

    /// <summary>
    /// TimeOverでラウンド終了
    /// </summary>
    private void FinishRound_TimeOver()
    {
        if (inGameUIController.PlayFinishRound_TimeOver() == false)
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
        if (screenFade.StartFadeOut() == true)
        {
            //キャラクターのHPのリセット
            GameManager.Instance.Player_one.HP = 100;
            GameManager.Instance.Player_two.HP = 100;
            inGameUIController.ResetUIParameter();

            //hpの初期化
            inGameUIController.DisplayPlayerHp(GameManager.Instance.Player_one.HP, GameManager.Instance.Player_two.HP);

            //ラウンドカウンターの更新
            inGameUIController.UpdateWinCounter(getRoundCount_p1, getRoundCount_p2);

            //キャラクターポジションのリセット

            currentUpdate = StartRound;
        }
    }

    /// <summary>
    /// 勝敗判定
    /// </summary>
    private void GameVictory()
    {
        //ラウンドカウンターの更新
        inGameUIController.UpdateWinCounter(getRoundCount_p1, getRoundCount_p2);

        if (getRoundCount_p1 > getRoundCount_p2)
        {
            if (inGameUIController.DisplayVictory_winP1() == false)
                currentUpdate = GameFinish;
        }
        else
        {
            if(inGameUIController.DisplayVictory_winP2() == false)
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
        inGameUIController = GameObject.Find("InGameUIController").GetComponent<InGameUIController>();
        screenFade = GameObject.Find("ScreenFade").GetComponent<ScreenFade>();
        //sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();

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
