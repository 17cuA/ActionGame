using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class CharacterSelectPlayer
{
	public CharacterSelectCursol cursol = new CharacterSelectCursol();	// カーソルオブジェクトの本体
	public NamePanel namePanel = new NamePanel();							// キャラの名前を表示しているパネルの本体
	public CharacterModel characterModel = new CharacterModel();		// キャラクターモデルを管理するオブジェクトの本体

	// 各オブジェクトの初期化処理
	public void Init(List<CharacterSelectObjectData> _characterSelectObjectDatas)
	{
		cursol.CursolInit(_characterSelectObjectDatas, characterModel.ChangeAnimation, characterModel.ChangeColor);
		characterModel.ChangeActive((int)cursol.currentCharacter);
	}
	// 各オブジェクトのアップデートをまとめているアップデートの処理
	public void Update(List<CharacterSelectObjectData> _characterSelectObjectDatas)
	{
		cursol.Update();
		namePanel.ChangeName(_characterSelectObjectDatas[(int)cursol.currentCharacter].NamePanel);
		characterModel.ChangeActive((int)cursol.currentCharacter);
	}
}
#region カーソルのオブジェクト
// カーソルの位置を変更するクラス
[System.Serializable]
public class CharacterSelectCursol
{
	public delegate void AcceptMethod(int _selectCharaNumber);     // キャラが決定されたときの別オブジェクトの処理を委譲
	AcceptMethod acceptMthod;                    // キャラ決定時の処理の本体

	public delegate void ColorChangeMethod(int _selectCharaNumber);		// キャラの色が変更されたときの別オブジェクトの処理を委譲
	ColorChangeMethod changeMethod;		// 色変更時の処理の本体

	List<CharacterSelectObjectData> characterSelectObjectDatas;     // キャラクターオブジェクトのデータ
	public Vector2 inputDeirection;		// 移動の移動の方向
	public int playerNumber = 0;			// プレイヤーの番号(Inspecterでインスタンスごとに設定)
	public string controllerName;          // コントローラーの名前(自動設定)
	public float moveCursorFrame;		// カーソルが移動していない時間
	public float limitCursorFrame;			// カーソルが移動できるようになるためのリミット
	public ECharacterID currentCharacter = ECharacterID.CLICO;      // クリコで初期化しておく
	public GameObject cursol;				// 動かすカーソルのオブジェクト(Inspecterでインスタンスごとに設定)
	private bool acceptFlag = false;
	public bool AcceptFlag { get { return acceptFlag; } set { acceptFlag = value; } }

	// コントローラーの名前をプレイヤーのコントローラーごとに設定
	public void CursolInit(List<CharacterSelectObjectData> _characterSelectObjectDatas, AcceptMethod _acceptMthod, ColorChangeMethod _changeMthod)
	{
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
		moveCursorFrame += Time.deltaTime;
		if (AcceptBotton(acceptMthod))
		{
			CharacterSelect();
		}
		inputDeirection.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNumber));
		inputDeirection.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNumber));

		if (inputDeirection != Vector2.zero && AcceptFlag == false)
		{
			if (moveCursorFrame >= limitCursorFrame)
			{
				currentCharacter = InputCursolDirection(currentCharacter, inputDeirection);
				CursolMove(characterSelectObjectDatas[(int)currentCharacter]);
			}
		}
	}
	// キャラが決定されたか判定し、決定されたら引数で渡された処理を行う
	public bool AcceptBotton(AcceptMethod func)
	{
		if (Input.GetButtonDown(string.Format("{0}Player{1}_Attack1", controllerName, playerNumber)))
		{
			AcceptFlag = true;
			func((int)currentCharacter);
			return true;
		}
		else
		{
			AcceptFlag = false;
			return false;
		}
	}
	// カーソルの位置を変更する(あとでVector3.leapに変更)
	public void CursolMove(CharacterSelectObjectData _characterSelectObjectData)
	{
		cursol.transform.position = _characterSelectObjectData.PanelPosition[playerNumber].transform.position;
	}

	// カーソル移動の核部分
	public ECharacterID InputCursolDirection(ECharacterID _selectCharacter, Vector2 _inputDir)
	{
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
	// キャラ決定時に呼び出す処理
	public void CharacterSelect()
	{
		if (AcceptFlag == false)
		{
			AcceptFlag = true;
			Sound.LoadSE("Menu_Decision", "Se_menu_decision");
			Sound.PlaySE("Menu_Decision", 1, 1);
		}
		else
		{
			AcceptFlag = false;
			Sound.LoadSE("Menu_Cancel", "Se_menu_cancel");
			Sound.PlaySE("Menu_Cancel", 1, 1);
		}
	}
}
#endregion
#region キャラ名前パネルのオブジェクト
// キャラクターの名前パネルを変更するクラス
[System.Serializable]
public class NamePanel
{
	public GameObject charaNamePanel;

	// 名前パネルの画像を切り替える処理
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
	public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();	// マテリアルを変更するために各モデルの情報を取得
	public int playerNumber;								// プレイヤーの番号
	public GameObject characterInstancePos;		// キャラを生成する位置(インスペクターからプレイヤーごとに設定)
	public List<AnimationData> animationDatas = new List<AnimationData>();	// アニメーションを操作するオブジェクト
	public List<GameObject> modelDatas = new List<GameObject>();				// モデルのアクセス可能ためのオブジェクト
	public Material[] materials;																			// プレイヤーごとにマテリアルが違うため、このオブジェクトにインスペクターで設定
	// キャラクターモデルの生成
	public void CreateCharacter(CharacterSelectObjectData _characterSelectObjectDatas)
	{
		// プレイヤーの番号で生成するオブジェクトを変更
		if (playerNumber == 1)
		{
			var temp = GameObject.Instantiate(_characterSelectObjectDatas.Model[0].PlayerModel, characterInstancePos.transform.position, characterInstancePos.transform.rotation);
			modelDatas.Add(temp);
			animationDatas.Add(temp.GetComponent<AnimationData>());
			skinnedMeshRenderers.Add(temp.GetComponent<SkinnedMeshRenderer>());
		}
		if (playerNumber == 2)
		{
			var temp = GameObject.Instantiate(_characterSelectObjectDatas.Model[0].PlayerModel2, characterInstancePos.transform.position, characterInstancePos.transform.rotation);
			modelDatas.Add(temp);
			animationDatas.Add(temp.GetComponent<AnimationData>());
			skinnedMeshRenderers.Add(temp.GetComponent<SkinnedMeshRenderer>());
		}
	}
	public void ChangeActive(int _selectCharaNumber)
	{
		for (int i = 0; i < modelDatas.Count; i++)
		{
			modelDatas[i].active = false;
		}
		modelDatas[_selectCharaNumber].active = true;
	}
	// 色の変更処理(ボタン押したら呼び出す。
	public void ChangeColor(int _selectCharaNumber)
	{
		skinnedMeshRenderers[_selectCharaNumber].material = materials[_selectCharaNumber];
	}
	// キャラクターを決定したら呼び出す処理
	public void ChangeAnimation(int _selectNumber)
	{
		animationDatas[_selectNumber].ChangeAnimation(true);
	}
}
#endregion