using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
//
//
// キャラクターにIDを持たせて管理
public enum ECharacterID
{
	CLICO,
	OBACHAN,
	KUIDAORE,
}
//
// Singleton CharacterselectObjectManager.CharacterselectObjectInstance.xxx の形でアクセス可能
// ここですべてのオブジェクトを操作、管理する
public class CharacterselectObjectManager : MonoBehaviour
{
	#region Singleton
	public static bool dontDs;
	private static CharacterselectObjectManager characterSelectControlInstance;
	public static CharacterselectObjectManager CharacterSelectControlInstance
	{
		get
		{
			if (characterSelectControlInstance == null)
			{
				Type t = typeof(CharacterselectObjectManager);

				characterSelectControlInstance = (CharacterselectObjectManager)FindObjectOfType(t);
				if (characterSelectControlInstance == null)
				{
					var _ins = new GameObject();
					_ins.name = "CharacterselectObjectInstance";
					characterSelectControlInstance = _ins.AddComponent<CharacterselectObjectManager>();
				}
			}

			return characterSelectControlInstance;
		}
	}
	#endregion
	// キャラクターのステータスを格納する(キャラごとに追加、インスペクターから設定)----------------------------
	public FighterStatus[] clicoStatus;
	public FighterStatus[] obachanStatus;
	public FighterStatus[] kuidaoreStatus;
	//-------------------------------------------------------------------------------------------------------------------

	// キャラクターの顔パネルを格納(キャラごとに追加し、ディスプレイの数分要素を作る。インスペクターから設定)
	public GameObject[] clicoPanel;
	public GameObject[] obachanPanel;
	public GameObject[] kuidaorePanel;
	//-------------------------------------------------------------------------------------------------------------------

	// キャラクターの名前を表示するパネルの画像(キャラごとに追加、インスペクターから設定)---------------------
	public Sprite clikoNamePanel;
	public Sprite obachanNamePanel;
	public Sprite kuidaoreNamePanel;
	//-------------------------------------------------------------------------------------------------------------------

	List<CharacterSelectObjectData> characterSelectObjectDatas = new List<CharacterSelectObjectData>();	// キャラクターごとの全データ(キャラごとにAwake()で追加)

	public Characterselect_Timer timer;     // カウントダウンタイマー(インスペクターから設定)

	[SerializeField]
	private CharacterSelectPlayer[] players;            // プレイヤーの数 = 要素数
	[SerializeField]
	private float sceneChangeCountDown = 0.0f;	// 全プレイヤーがキャラ決定した後、シーン移行するまでのカウントダウン
	[SerializeField]
	private bool[] selectFlag;									// 全プレイヤーがキャラを決定しているかどうかのフラグ

	// 各オブジェクトの辞書にデータを登録、初期化処理
	private void Awake()
	{
		sceneChangeCountDown = 0;
		selectFlag = new bool[players.Length];

		Sound.LoadBGM("BGM_Menu", "BGM_Menu");
		Sound.PlayBGM("BGM_Menu", 1, 1.0f, true);

		// キャラクターごとに追加
		characterSelectObjectDatas.Add(new CharacterSelectObjectData("cliko", ECharacterID.CLICO, clicoStatus, clikoNamePanel, clicoPanel));
		characterSelectObjectDatas.Add(new CharacterSelectObjectData("obachan", ECharacterID.OBACHAN, obachanStatus, obachanNamePanel, obachanPanel));
		characterSelectObjectDatas.Add(new CharacterSelectObjectData("kuidalre", ECharacterID.KUIDAORE, kuidaoreStatus, kuidaoreNamePanel, kuidaorePanel));

		// キャラモデルの生成、初期化処理
		for (int i = 0;i<players.Length;i++)
		{
			for(int j = 0;j < characterSelectObjectDatas.Count;j++)
			{
				players[i].characterModel.CreateCharacter(characterSelectObjectDatas[j]);
			}
			players[i].Init(characterSelectObjectDatas);
		}

		// カーテンを一気に下す処理
		CanvasController_CharacterSelect.CanvasControllerInstance.InitDownCurtain();
	}

	private void Update()
	{
		// ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;

		if (CanvasController_CharacterSelect.CanvasControllerInstance.curtainFlag == false)
		{
			// カーテンが上がるまでループ
			CanvasController_CharacterSelect.CanvasControllerInstance.curtainFlag = CanvasController_CharacterSelect.CanvasControllerInstance.UpCurtain();
			return;
		}
		if (timer.startFlag == false)
		{
			timer.TimerStart();
		}

		timer.TimerUpdate();
		// プレイヤーごとの処理
		for(int i = 0;i<players.Length;i++)
		{
			players[i].Update(characterSelectObjectDatas);
			AcceptCharacter(players[i].cursol.currentCharacter, i);
			selectFlag[i] = players[i].cursol.AcceptFlag;
		}
		// カウントがゼロになったらシーン遷移させる
		if (timer.currentTime <= 0 && sceneChangeCountDown == 0)
		{
			for (int i = 0; i < players.Length; i++)
			{
				if (players[i].cursol.ActiveFlag == true)
				{
					players[i].cursol.ActiveFlag = false;
					players[i].cursol.AcceptButton(players[i].cursol.acceptMthod);
				}
			}
			AcceptCharacter();
		}
		// 全プレイヤーが選択したらシーンを遷移させる
		for (int i = 0; i < selectFlag.Length; i++)
		{
			if (selectFlag[i] == false)
			{
				sceneChangeCountDown = 0;
				break;
			}
			AcceptCharacter();
		}
	}

	/// <summary>
	/// ゲームデータを保存しているオブジェクトにデータを送信
	/// </summary>
	/// <param name="_characterID">選択しているキャラのID</param>
	/// <param name="_playerNumber">プレイヤーの番号</param>
	private void AcceptCharacter(ECharacterID _characterID,int _playerNumber)
	{
		switch(_characterID)
		{
			case ECharacterID.CLICO:
				GameDataStrage.Instance.fighterStatuses[_playerNumber] = clicoStatus[players[_playerNumber].characterModel.currentModel.GetComponent<CharacterMeshData>().colorNumber-1];
				break;
			case ECharacterID.OBACHAN:
				GameDataStrage.Instance.fighterStatuses[_playerNumber] = obachanStatus[players[_playerNumber].characterModel.currentModel.GetComponent<CharacterMeshData>().colorNumber-1];
				break;
			case ECharacterID.KUIDAORE:
				GameDataStrage.Instance.fighterStatuses[_playerNumber] = kuidaoreStatus[players[_playerNumber].characterModel.currentModel.GetComponent<CharacterMeshData>().colorNumber - 1];
				break;
		}
	}
	/// <summary>
	/// シーンを遷移させる処理
	/// </summary>
	public void AcceptCharacter()
	{
		sceneChangeCountDown += Time.deltaTime;
		// 5秒待機してからシーン遷移
		if (sceneChangeCountDown > 5.0f)
		{
			for (int i = 0; i < players.Length; i++)
			{
				players[i].cursol.ActiveFlag = false;
			}
			if (CanvasController_CharacterSelect.CanvasControllerInstance.DownCurtain())
			{
				SceneManager.LoadScene("Battle");
			}
		}
	}
}
