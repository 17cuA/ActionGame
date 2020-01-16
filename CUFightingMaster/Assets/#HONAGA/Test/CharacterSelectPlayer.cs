using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
// プレイヤーの数だけインスタンスを作成
public class CharacterSelectPlayer
{
	public string playerName;
	public CharacterSelectCursol cursol = new CharacterSelectCursol();	// カーソルオブジェクトの本体
	public NamePanel namePanel = new NamePanel();							// キャラの名前を表示しているパネルの本体
	public CharacterModel characterModel = new CharacterModel();		// キャラクターモデルを管理するオブジェクトの本体

	/// <summary>
	/// 各オブジェクトの初期化処理
	/// </summary>
	/// <param name="_characterSelectObjectDatas">全キャラクターのデータ</param>
	public void Init(List<CharacterSelectObjectData> _characterSelectObjectDatas)
	{
		cursol.CursolInit(_characterSelectObjectDatas, characterModel.ChangeAnimation, characterModel.ChangeColor);
		characterModel.ChangeActive((int)cursol.currentCharacter);
	}
	/// <summary>
	/// 各オブジェクトのアップデートをまとめている処理
	/// </summary>
	/// <param name="_characterSelectObjectDatas">全キャラクターのデータ</param>
	public void Update(List<CharacterSelectObjectData> _characterSelectObjectDatas)
	{
		cursol.Update();
		namePanel.ChangeName(_characterSelectObjectDatas[(int)cursol.currentCharacter].NamePanel);
		characterModel.Update((int)cursol.currentCharacter);
		//characterModel.ChangeActive((int)cursol.currentCharacter);
	}
}
#region カーソルのオブジェクト
// カーソルの位置を変更するクラス
[System.Serializable]
public class CharacterSelectCursol
{
	[SerializeField]
	private bool activeFlag ;					// カーソルを動かすかどうかのフラグ
	public delegate void AcceptMethod(int _selectCharaNumber);     // キャラが決定されたときの別オブジェクトの処理を委譲
	AcceptMethod acceptMthod;                    // キャラ決定時の処理の本体

	public delegate void ColorChangeMethod();     // キャラの色が変更されたときの別オブジェクトの処理を委譲
	ColorChangeMethod changeMethod;     // 色変更時の処理の本体

	List<CharacterSelectObjectData> characterSelectObjectDatas;     // キャラクターオブジェクトのデータ
	public Vector2 inputDeirection;	// 移動の移動の方向
	public int playerNumber = 0;		// プレイヤーの番号(Inspecterでインスタンスごとに設定)
	public string controllerName;		// コントローラーの名前(自動設定)
	public float moveCursorFrame;	// カーソルが移動していない時間
	public float limitCursorFrame;		// カーソルが移動できるようになるためのリミット
	public ECharacterID currentCharacter = ECharacterID.CLICO;      // クリコで初期化しておく
	public GameObject cursol;			// 動かすカーソルのオブジェクト(Inspecterでインスタンスごとに設定)
	[SerializeField] private bool acceptFlag = false;
	public bool AcceptFlag { get { return acceptFlag; } set { acceptFlag = value; } }

	/// <summary>
	/// カーソルの初期化処理
	/// </summary>
	/// <param name="_characterSelectObjectDatas">このゲームに使う全キャラクターのデータ</param>
	/// <param name="_acceptMthod">決定した時の処理、キャンセルした時の処理</param>
	/// <param name="_changeMthod">カラーを変更する時の処理</param>
	public void CursolInit(List<CharacterSelectObjectData> _characterSelectObjectDatas, AcceptMethod _acceptMthod, ColorChangeMethod _changeMthod)
	{
		activeFlag = true;
		var controllerNames = Input.GetJoystickNames();
		if (playerNumber < controllerNames.Length)
		{
			if (controllerNames[playerNumber] != "")
			{
				controllerName = string.Format("{0}_", controllerNames[playerNumber]);
			}
		}
		changeMethod = _changeMthod;
		acceptMthod = _acceptMthod;
		characterSelectObjectDatas = _characterSelectObjectDatas;
	}
	// CursolのUpdateの処理
	public void Update()
	{
		// カーソルが動けない場合早期リターンで処理を止める
		if (activeFlag == false) return;
		// カーソルが動いた後のクールタイム
		moveCursorFrame += Time.deltaTime;

		// 入力ごとの処理-------------------------------------------------------------------------------------------------------------------
		if (Input.GetButtonDown(string.Format("{0}Player{1}_Attack1", controllerName, playerNumber)))
		{
			AcceptButton(acceptMthod);
		}
		if (Input.GetButtonDown(string.Format("{0}Player{1}_Attack2", controllerName, playerNumber)))
		{
			CanselButton(acceptMthod);
		}
		if (Input.GetButtonDown(string.Format("{0}Player{1}_Attack3", controllerName, playerNumber))&&AcceptFlag == false)
		{
			changeMethod();
		}
		//-------------------------------------------------------------------------------------------------------------------------------------

		inputDeirection.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNumber));
		inputDeirection.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNumber));

		// 入力があり、決定していない時のみカーソルを動かす
		if (inputDeirection != Vector2.zero && AcceptFlag == false)
		{
			if (moveCursorFrame >= limitCursorFrame)
			{
				currentCharacter = InputCursolDirection(currentCharacter, inputDeirection);
				CursolMove(characterSelectObjectDatas[(int)currentCharacter]);
			}
		}
		// オバーフロー防止のため追加
		if (moveCursorFrame > 1.0f)
		{
			moveCursorFrame = 1.0f;
		}
	}
	/// <summary>
	/// キャラを決定した時の処理
	/// </summary>
	/// <param name="_func">決定した時の処理(別クラスのキャンセル処理を請け負っている)</param>
	public void AcceptButton(AcceptMethod _func)
	{
		if (AcceptFlag == true) return;	// 既に決定していた場合早期リターン
		Sound.LoadSE("Menu_Decision", "Se_menu_decision");
		Sound.PlaySE("Menu_Decision", 1, 1);
		AcceptFlag = true;
		_func((int)currentCharacter);
	}
	/// <summary>
	/// 選択しているキャラのキャンセル処理
	/// </summary>
	/// <param name="_func">キャンセルした時の処理(別クラスのキャンセル処理を請け負っている)</param>
	public void CanselButton(AcceptMethod _func)
	{
		if (AcceptFlag == false) return;
		Sound.LoadSE("Menu_Cancel", "Se_menu_cancel");
		Sound.PlaySE("Menu_Cancel", 1, 1);
		AcceptFlag = false;
		_func((int)currentCharacter);
	}

	//public void ChangeColorButton(ColorChangeMethod _func)
	//{
	//	_func();
	//}
	/// <summary>
	/// カーソルの位置(posirion)を移動させる
	/// </summary>
	/// <param name="_characterSelectObjectData">キャラデータ(設定されているキャラごとのパネルの位置へ移動させる)</param>
	public void CursolMove(CharacterSelectObjectData _characterSelectObjectData)
	{
		cursol.transform.position = _characterSelectObjectData.PanelPosition[playerNumber].transform.position;
	}

	/// <summary>
	/// カーソル移動の核部分。選択しているキャラのIDを変更
	/// </summary>
	/// <param name="_selectCharacter">選択しているキャラのID</param>
	/// <param name="_inputDir">移動する方向</param>
	/// <returns></returns>
	public ECharacterID InputCursolDirection(ECharacterID _selectCharacter, Vector2 _inputDir)
	{
		if (playerNumber == 1) _inputDir *= -1;
		Sound.LoadSE("Menu_MoveCursor", "Se_menu_moveCursor");
		Sound.PlaySE("Menu_MoveCursor", 1, 0.8f);
		//左右移動（-1が左、1が右）
		_selectCharacter += (int)_inputDir.x;
		// 左端のキャラを選択しているときに、左の入力があった場合、右端のキャラにする
		if (_selectCharacter < 0)
		{
			_selectCharacter = (ECharacterID)System.Enum.GetNames(typeof(ECharacterID)).Length - 1;
		}
		// 右端のキャラを選択しているときに、右の入力があった場合、左端のキャラにする
		else if (_selectCharacter == (ECharacterID)Enum.GetNames(typeof(ECharacterID)).Length)
		{
			_selectCharacter = (ECharacterID)0;
		}
		moveCursorFrame = 0;
		return _selectCharacter;
	}
}
#endregion
#region キャラ名前パネルのオブジェクト
// キャラクターの名前パネルを変更するクラス
[System.Serializable]
public class NamePanel
{
	public GameObject charaNamePanel;

	/// <summary>
	/// 引数に渡ってきたスプライトの画像に名前パネルを変更する処理
	/// </summary>
	/// <param name="_selectCharacterSprite">変更するスプライト</param>
	public void ChangeName(Sprite _selectCharacterSprite)
	{
		charaNamePanel.GetComponent<Image>().sprite = _selectCharacterSprite;
	}
}
#endregion
#region キャラモデルの管理
// キャラクターモデルの生成、アニメーションの変更
[System.Serializable]
public class CharacterModel
{
	public int playerNumber;								// プレイヤーの番号
	public GameObject characterInstancePos;		// キャラを生成する位置(インスペクターからプレイヤーごとに設定)
	public List<AnimationData> animationDatas = new List<AnimationData>();		// アニメーションを操作するオブジェクト
	public List<GameObject> modelDatas = new List<GameObject>();					// モデルのアクセス可能ためのオブジェクト
	public GameObject currentModel;																	// 今選んでいるキャラクターモデル

	/// <summary>
	/// キャラモデルの生成。FighterStutasに設定しているモデルをプレイヤーの番号ごとに生成
	/// </summary>
	/// <param name="_characterSelectObjectDatas">キャラクターのデータ(名前、モデルデータなど)</param>
	public void CreateCharacter(CharacterSelectObjectData _characterSelectObjectDatas)
	{
		// プレイヤーの番号で生成するオブジェクトを変更
		if (playerNumber == 1)
		{
			var temp = GameObject.Instantiate(_characterSelectObjectDatas.Model[0].PlayerModel, characterInstancePos.transform.position, characterInstancePos.transform.rotation);
			modelDatas.Add(temp);
			animationDatas.Add(temp.GetComponent<AnimationData>());
			temp.active = false;
		}
		if (playerNumber == 2)
		{
			var temp = GameObject.Instantiate(_characterSelectObjectDatas.Model[0].PlayerModel2, characterInstancePos.transform.position, characterInstancePos.transform.rotation);
			modelDatas.Add(temp);
			animationDatas.Add(temp.GetComponent<AnimationData>());
			temp.active = false;
		}
	}
	/// <summary>
	/// Update()の処理
	/// </summary>
	/// <param name="_selectCharaNumber">選択しているキャラのID</param>
	public void Update(int _selectCharaNumber)
	{
		ChangeActive(_selectCharaNumber);
		ChangeAnimation(_selectCharaNumber);
		for(int i =0;i<animationDatas.Count;i++)
		{
			animationDatas[i].Update();
		}
	}
	/// <summary>
	/// キャラモデルのアクティブを切り替えて画面から消す(出す)処理
	/// </summary>
	/// <param name="_selectCharaNumber">選択しているキャラのID</param>
	public void ChangeActive(int _selectCharaNumber)
	{
		for (int i = 0; i < modelDatas.Count; i++)
		{
			modelDatas[i].active = false;
		}
		modelDatas[_selectCharaNumber].active = true;
		currentModel = modelDatas[_selectCharaNumber];
	}
	/// <summary>
	/// ボタンを押したらマテリアルを切り替ええ、モデルの色を変更する処理
	/// </summary>
	public void ChangeColor()
	{
		currentModel.GetComponent<CharacterMeshData>().ChangeMaterials();
	}
	/// <summary>
	/// 決定、通常時のアニメーションを選択状況で変更する処理
	/// </summary>
	/// <param name="_selectNumber">選択しているキャラの処理</param>
	public void ChangeAnimation(int _selectNumber)
	{
		animationDatas[_selectNumber].AnimFlag = true;
	}
}
#endregion