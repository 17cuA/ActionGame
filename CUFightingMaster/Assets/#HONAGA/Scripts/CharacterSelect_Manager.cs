//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using CUEngine.Pattern;

//public class CharacterSelect_Manager : SingletonMono<CharacterSelect_Manager>
//{
//	#region 変数宣言
//	public AnimationUIManager[] animation_Ready = new AnimationUIManager[4];

//	public Cursor_CharacterSelect cursor1_1;    // Display1の1P用カーソル
//	public Cursor_CharacterSelect cursor1_2;    // Display1の2P用カーソル
//	public Cursor_CharacterSelect cursor2_1;    // Display2の1P用カーソル
//	public Cursor_CharacterSelect cursor2_2;    // Display2の2P用カーソル

//	public bool curtainFlag = false;            // 幕が上がるフラグ

//	[SerializeField] static int charaMax = 4;

//	public GameObject[] previewModel = new GameObject[charaMax * 2];                // 生成したキャラクターモデルを入れておく変数（２画面２プレイヤーのため、２倍生成）
//	public Animationdata[] nomalAnimationPlayers = new Animationdata[charaMax * 2]; // 生成したキャラクターモデルのアニメーション情報を入れておく変数（２画面２プレイヤーのため、２倍生成）
//	public GameObject[] createCharaPos = new GameObject[charaMax];                  // キャラクターモデルを生成する位置
//	public Sprite[] SelectCharacterNamePanels = new Sprite[charaMax];               // キャラクターの名前のパネルを格納しておく変数

//	[SerializeField] private bool[] characterSelectBool = { false, false, false, false };    // キャラクターを選択したかどうかの判定
//	public bool[] CharacterSelectBool
//	{
//		get { return characterSelectBool; }
//	}
//	public bool sceneChangeJughe;   // シーンの変更を許可

//	public FighterStatus[] currentCharacter = new FighterStatus[charaMax]; // ファイターの情報を格納する変数

//	[SerializeField] private bool panelAnimFlag;    // キャラクターパネルのアニメーション許可
//	[SerializeField] private int animFlagCount;     // １回だけアニメーションをさせたいので、作った変数  
//	[SerializeField] private float fadeFrame;       // シーンを変更させるためのフレーム（時間）
//	public float FadeFrame
//	{
//		get { return fadeFrame; }
//	}
//	public NomalAnimationPlayer[] timerAnim = new NomalAnimationPlayer[2];
//	public AnimationClip timerTrimClip;
//	private float canselTime;
//	private bool canselFrame;
//	#endregion

//	void Start()
//	{
//		canselTime = 0.0f;
//		canselFrame = false;
//		QualitySettings.vSyncCount = 0;
//		Application.targetFrameRate = 60;
//		// 飯塚追加-------------------------------------------
//		Sound.LoadBGM("BGM_Menu", "BGM_Menu");
//		Sound.PlayBGM("BGM_Menu", 1, 1.0f, true);
//		// ---------------------------------------------------

//		panelAnimFlag = false;
//		animFlagCount = 1;
//		fadeFrame = 0;
//		sceneChangeJughe = false;
//		if (currentCharacter[0] == null)
//		{
//			return;
//		}
//		// キャラモデルの生成
//		CreateModels();

//	}

//	void Update()
//	{
//		// カーソルのUpdate
//		CursorsUpdate();

//		// 1Pのキャラ選択
//		CharacterSelection1P();
//		// 2Pのキャラ選択
//		CharacterSelection2P();

//		// ディスプレイごとにカーソルがキャラクターを選択しているかのフラグを生成
//		CreateCursorFlags();

//		// カーソル選択
//		CursorSelections();

//		// 1Pと2Pがキャラを選択したら、フラグをtrueにする
//		CharaterDecisions();

//		// キャンセル処理
//		CancelProcessing();

//		// キャラ選択した後の時間を計測、managerからシーンの変更を許可
//		SceneChange();

//		// パネルのアニメーションを再生
//		PanelAnimationsPlay();
//	}

//	#region モデルの生成
//	void CreateModels()
//	{
//		for (int i = 0; i < charaMax; i++)
//		{
//			if ((i + 1) % 2 == 1)
//			{
//				//Clico
//				previewModel[i] = Instantiate(currentCharacter[i].PlayerModel, createCharaPos[0].transform.position, createCharaPos[0].transform.rotation);
//				previewModel[i + charaMax] = Instantiate(currentCharacter[i].PlayerModel2, createCharaPos[1].transform.position, Quaternion.identity);
//				nomalAnimationPlayers[i] = previewModel[i].GetComponent<Animationdata>();
//				nomalAnimationPlayers[i + charaMax] = previewModel[i + charaMax].GetComponent<Animationdata>();
//				nomalAnimationPlayers[i + charaMax].ScaleObject.transform.localScale = new Vector3(1, 1, -1);
//				nomalAnimationPlayers[i + charaMax].RotationObject.transform.rotation = Quaternion.Euler(0, 0, 0);
//			}
//			if ((i + 1) % 2 == 0)
//			{
//				//Obachan
//				previewModel[i] = Instantiate(currentCharacter[i].PlayerModel, createCharaPos[0].transform.position, createCharaPos[0].transform.rotation);
//				previewModel[i + charaMax] = Instantiate(currentCharacter[i].PlayerModel2, createCharaPos[1].transform.position, Quaternion.identity);
//				nomalAnimationPlayers[i] = previewModel[i].GetComponent<Animationdata>();
//				nomalAnimationPlayers[i + charaMax] = previewModel[i + charaMax].GetComponent<Animationdata>();
//				nomalAnimationPlayers[i + charaMax].ScaleObject.transform.localScale = new Vector3(1, 1, -1);
//				nomalAnimationPlayers[i + charaMax].RotationObject.transform.rotation = Quaternion.Euler(0, 0, 0);
//			}
//		}
//	}
//	#endregion

//	#region カーソルのUpdate
//	void CursorsUpdate()
//	{
//		cursor1_1.CursorUpdate();
//		cursor1_2.CursorUpdate();
//		cursor2_1.CursorUpdate();
//		cursor2_2.CursorUpdate();
//	}
//	#endregion

//	#region 1Pのキャラクターセレクト
//	void CharacterSelection1P()
//	{
//		if (cursor1_1 == null)
//		{
//			return;
//		}
//		// 1Pの選択されているキャラの設定(selectDirが0ならグリコ)
//		switch (cursor1_1.selectDir)
//		{
//			// クリコに変更
//			case 0:
//				// GameDataStrageの選択されているキャラをグリコにする
//				GameDataStrage.Instance.fighterStatuses[0] = currentCharacter[0];
//				if (SelectCharacterNamePanels[0] == null)
//				{
//					return;
//				}
//				PreviewModelsActiveSet(0, 1);
//				break;
//			// 別カラーのクリコに変更
//			case 1:
//				// GameDataStrageの選択されているキャラを別グリコにする
//				GameDataStrage.Instance.fighterStatuses[0] = currentCharacter[1];
//				if (SelectCharacterNamePanels[1] == null)
//				{
//					return;
//				}
//				PreviewModelsActiveSet(1, 1);
//				break;
//			// おばちゃんに変更
//			case 2:
//				// GameDataStrageの選択されているキャラをおばちゃんにする
//				GameDataStrage.Instance.fighterStatuses[0] = currentCharacter[2];
//				if (SelectCharacterNamePanels[1] == null)
//				{
//					return;
//				}
//				PreviewModelsActiveSet(2, 1);
//				break;
//			// 別おばちゃんに変更
//			case 3:
//				// GameDataStrageの選択されているキャラを別おばちゃんにする
//				GameDataStrage.Instance.fighterStatuses[0] = currentCharacter[3];
//				if (SelectCharacterNamePanels[1] == null)
//				{
//					return;
//				}
//				PreviewModelsActiveSet(3, 1);
//				break;
//			default:
//				break;
//		}
//	}
//	#endregion

//	#region 2Pのキャラクターセレクト
//	void CharacterSelection2P()
//	{
//		if (cursor1_2 == null)
//		{
//			return;
//		}
//		// 2Pの選択されているキャラの設定(selectDir２未満ならグリコ)
//		switch (cursor1_2.selectDir)
//		{
//			// クリコに変更
//			case 0:
//				// GameDataStrageの選択されているキャラをグリコにする
//				GameDataStrage.Instance.fighterStatuses[1] = currentCharacter[0];
//				if (SelectCharacterNamePanels[1] == null)
//				{
//					return;
//				}
//				PreviewModelsActiveSet(0, 2);
//				break;
//			// 別カラーのクリコに変更
//			case 1:
//				// GameDataStrageの選択されているキャラを別グリコにする
//				GameDataStrage.Instance.fighterStatuses[1] = currentCharacter[1];
//				if (SelectCharacterNamePanels[1] == null)
//				{
//					return;
//				}
//				// それ以外をfalse
//				PreviewModelsActiveSet(1, 2);
//				break;
//			// おばちゃんに変更
//			case 2:
//				// GameDataStrageの選択されているキャラをおばちゃんにする
//				GameDataStrage.Instance.fighterStatuses[1] = currentCharacter[2];
//				if (SelectCharacterNamePanels[1] == null)
//				{
//					return;
//				}
//				PreviewModelsActiveSet(2, 2);
//				break;
//			// 別カラーのおばちゃんに変更
//			case 3:
//				// GameDataStrageの選択されているキャラを別グリコにする
//				GameDataStrage.Instance.fighterStatuses[1] = currentCharacter[3];
//				if (SelectCharacterNamePanels[1] == null)
//				{
//					return;
//				}
//				// それ以外をfalse
//				PreviewModelsActiveSet(3, 2);
//				break;
//			default:
//				break;
//		}
//	}
//	#endregion

//	#region カーソルがキャラクターを選択しているかのフラグの生成
//	void CreateCursorFlags()
//	{
//		characterSelectBool[0] = cursor1_1.Determining_decision;    // Display1の1Pカーソルがキャラクターを選択しているかのフラグ
//		characterSelectBool[1] = cursor1_2.Determining_decision;    // Display1の2Pカーソルがキャラクターを選択しているかのフラグ
//		characterSelectBool[2] = cursor2_1.Determining_decision;    // Display2の1Pカーソルがキャラクターを選択しているかのフラグ
//		characterSelectBool[3] = cursor2_2.Determining_decision;    // Display2の2Pカーソルがキャラクターを選択しているかのフラグ
//	}
//	#endregion

//	#region 1Pと2Pのカーソル選択
//	void CursorSelections()
//	{
//		if (characterSelectBool[0] == true)
//		{
//			for (int i = 0; i < charaMax; i++)
//			{
//				nomalAnimationPlayers[i].animFrag = true;
//			}
//			if (animation_Ready[0].isStart == false && animation_Ready[2].isStart == false)
//			{
//				animation_Ready[0].isInterruption = false;
//				animation_Ready[2].isInterruption = false;
//				animation_Ready[0].isStart = true;
//				animation_Ready[2].isStart = true;
//			}
//		}
//		else
//		{
//			for (int i = 0; i < charaMax; i++)
//			{
//				nomalAnimationPlayers[i].animFrag = false;
//			}
//		}

//		if (CharacterSelectBool[1] == true)
//		{
//			for (int i = 4; i < 8; i++)
//			{
//				nomalAnimationPlayers[i].animFrag = true;
//			}
//			if (animation_Ready[1].isStart == false && animation_Ready[3].isStart == false)
//			{
//				animation_Ready[1].isInterruption = false;
//				animation_Ready[3].isInterruption = false;
//				animation_Ready[1].isStart = true;
//				animation_Ready[3].isStart = true;
//			}
//		}
//		else
//		{
//			for (int i = 4; i < 8; i++)
//			{
//				nomalAnimationPlayers[i].animFrag = false;
//			}
//		}

//		if (characterSelectBool[0] == false && characterSelectBool[2] == false)
//		{
//			animation_Ready[0].isInterruption = true;
//			animation_Ready[2].isInterruption = true;
//		}

//		if (characterSelectBool[1] == false && characterSelectBool[3] == false)
//		{
//			animation_Ready[1].isInterruption = true;
//			animation_Ready[3].isInterruption = true;
//		}
//	}
//	#endregion

//	#region 1Pと2Pのキャラクター選択後
//	void CharaterDecisions()
//	{
//		if (characterSelectBool[0] && characterSelectBool[1] && characterSelectBool[2] && characterSelectBool[3])
//		{
//			canselFrame = true;
//		}
//		else
//		{
//			fadeFrame = 0;
//			canselFrame = false;
//		}
//	}
//	#endregion

//	#region キャンセル処理
//	void CancelProcessing()
//	{
//		if (canselFrame == true)
//		{
//			canselTime += Time.deltaTime;
//		}
//		else
//		{
//			canselTime = 0.0f;
//		}
//		if (canselTime >= 3.0f)
//		{
//			panelAnimFlag = true;
//			cursor1_1.determining_All = true;
//			cursor1_2.determining_All = true;
//			cursor2_1.determining_All = true;
//			cursor2_2.determining_All = true;
//		}
//	}
//	#endregion

//	#region モデルの決定
//	/// <summary>
//	/// selectCharacterが１ならグリコ、２なら別グリコ、3ならおばちゃん、４なら別おばちゃん
//	/// </summary>
//	/// <param name="selectCharacterNum">キャラクターの番号</param>
//	/// <param name="playerNum">プレイヤーが選択している番号</param>
//	void PreviewModelsActiveSet(int selectCharacterNum, int playerNum)
//	{
//		if (playerNum == 1)
//		{
//			for (int i = 0; i < charaMax; i++)
//			{
//				if (previewModel[i] != null && previewModel[i].active == true)
//				{
//					if (i != selectCharacterNum)
//					{
//						previewModel[i].SetActive(false);
//					}
//				}
//			}
//			if (!previewModel[selectCharacterNum].activeSelf)
//			{
//				previewModel[selectCharacterNum].SetActive(true);
//			}
//		}
//		if (playerNum == 2)
//		{
//			for (int i = 0; i < charaMax; i++)
//			{
//				if (previewModel[i + 4] != null && previewModel[i + 4].active == true)
//				{
//					if (i != selectCharacterNum)
//					{
//						previewModel[i + 4].SetActive(false);
//					}
//				}
//			}
//			if (!previewModel[selectCharacterNum + 4].activeSelf)
//			{
//				previewModel[selectCharacterNum + 4].SetActive(true);
//			}
//		}
//	}
//	#endregion

//	#region シーン変更の許可
//	void SceneChange()
//	{
//		if (panelAnimFlag)
//		{
//			fadeFrame += Time.deltaTime;
//			sceneChangeJughe = true;
//		}
//	}
//	#endregion

//	#region パネルのAnimation再生
//	// パネルのAnimation再生
//	void PanelAnimationsPlay()
//	{
//		if (panelAnimFlag == true && animFlagCount > 0)
//		{
//			if (cursor1_1.characterPanels[0] == null)
//			{
//				return;
//			}
//			// １回しか再生させたくないので、カウントの減算
//			animFlagCount--;
//			cursor1_1.characterPanels[0].GetComponent<Animation>().Play();  // Display1の一番左のパネル
//			cursor1_1.characterPanels[1].GetComponent<Animation>().Play();  // Display1の左から２番目のパネル
//			cursor1_1.characterPanels[2].GetComponent<Animation>().Play();  // Display1の右から２番目のパネル
//			cursor1_1.characterPanels[3].GetComponent<Animation>().Play();  // Display1の一番右のパネル

//			cursor2_1.characterPanels[0].GetComponent<Animation>().Play();  // Display2の一番左のパネル
//			cursor2_1.characterPanels[1].GetComponent<Animation>().Play();  // Display2の左から２番目のパネル
//			cursor2_1.characterPanels[2].GetComponent<Animation>().Play();  // Display2の右から２番目のパネル
//			cursor2_1.characterPanels[3].GetComponent<Animation>().Play();  // Display2の一番右のパネル

//			timerAnim[0].SetPlayAnimation(timerTrimClip, 1.0f, 0);
//			timerAnim[1].SetPlayAnimation(timerTrimClip, 1.0f, 0);
//		}
//	}
//	#endregion
//}