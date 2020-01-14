using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//
//
// キャラクターにIDを持たせて管理
public enum ECharacterID
{
	CLICO,
	OBACHAN,
	//KUIDAORE,
}
//
// Singleton CharacterselectObject.CharacterselectObjectInstance.xxx の形でアクセス可能
// ここですべてのオブジェクトを操作、管理する
public class CharacterselectObject : MonoBehaviour
{
	#region Singleton
	public static bool dontDs;
	private static CharacterselectObject characterSelectControlInstance;
	public static CharacterselectObject CharacterSelectControlInstance
	{
		get
		{
			if (characterSelectControlInstance == null)
			{
				Type t = typeof(CharacterselectObject);

				characterSelectControlInstance = (CharacterselectObject)FindObjectOfType(t);
				if (characterSelectControlInstance == null)
				{
					var _ins = new GameObject();
					_ins.name = "CharacterselectObjectInstance";
					characterSelectControlInstance = _ins.AddComponent<CharacterselectObject>();
				}
			}

			return characterSelectControlInstance;
		}
	}
	#endregion
	public FighterStatus[] clicoStatus;
	public FighterStatus[] obachanStatus;
	//public FighterStatus[] kuidaoreStatus;
	public GameObject[] clicoPanel;
	public GameObject[] obachanPanel;
	public GameObject[] kuidaorePanel;
	public Sprite clikoNamePanel;
	public Sprite obachanNamePanel;
	public Sprite kuidaoreNamePanel;

	List<CharacterSelectObjectData> characterSelectObjectDatas = new List<CharacterSelectObjectData>();

	public Characterselect_Timer timer;

	public CharacterSelectPlayer Player1 = new CharacterSelectPlayer();
	public CharacterSelectPlayer Player2 = new CharacterSelectPlayer();


	// 各オブジェクトの辞書にデータを登録、初期化処理
	private void Awake()
	{
		// キャラクターごとに追加
		characterSelectObjectDatas.Add(new CharacterSelectObjectData("cliko", ECharacterID.CLICO, clicoStatus, clikoNamePanel, clicoPanel));
		characterSelectObjectDatas.Add(new CharacterSelectObjectData("obachan", ECharacterID.OBACHAN, obachanStatus, obachanNamePanel, obachanPanel));
		//characterSelectObjectDatas.Add(new CharacterSelectObjectData("kuidalre",ECharacterID.KUIDAORE, kuidaoreStatus, kuidaoreNamePanel, kuidaorePanel,characterInstancePos));

		for (int i = 0; i < characterSelectObjectDatas.Count; i++)
		{
			Player1.characterModel.CreateCharacter(characterSelectObjectDatas[i]);
			Player2.characterModel.CreateCharacter(characterSelectObjectDatas[i]);
		}
		Player1.Init(characterSelectObjectDatas);
		Player2.Init(characterSelectObjectDatas);
		CanvasController_CharacterSelect.CanvasControllerInstance.InitDownCurtain();

		timer.Start();
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

		timer.Update();
		Player1.Update(characterSelectObjectDatas);
		Player2.Update(characterSelectObjectDatas);

		//if(p1CursolData.AcceptFlag)
		//{
		//    p1CharacterModelData.ChangeAnimation(p1CursolData.currentCharacter);
		//}
		//if (p2CursolData.AcceptFlag)
		//{
		//    p2CharacterModelData.ChangeAnimation(p2CursolData.currentCharacter);
		//}

	}
}
