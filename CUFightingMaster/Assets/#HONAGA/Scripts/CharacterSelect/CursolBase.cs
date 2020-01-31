using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カーソルのベースクラス。これを継承させてアップデートすればカーソルを作れる
/// </summary>
/// <typeparam name="T">Enumで属性を作る</typeparam>
/// <typeparam name="Gamobject">動かしたいオブジェクトの型を入れる(基本GameObjectで大丈夫)</typeparam>
public class CursolBase<TEnum, TGameObject> : MonoBehaviour where TEnum : System.Enum where TGameObject : UnityEngine.Object
{
	// 動けるかどうかのフラグ
	private bool activeFlag;
	public bool ActiveFlag { get { return activeFlag; } set { activeFlag = value; } }

	// 縦方向への移動があるかのフラグ
	private bool inputFlagDirectionY;
	public bool InputFlagDirectionY { get { return inputFlagDirectionY; } set { inputFlagDirectionY = value; } }
	// 横方向への移動があるかのフラグ
	private bool inputFlagDirectionX;
	public bool InputFlagDirectionX { get { return inputFlagDirectionX; } set { inputFlagDirectionX = value; } }
	// 選択したかのフラグ
	private bool acceptFlag;
	public bool AcceptFlag { get { return acceptFlag; } set { acceptFlag = value; } }
	// 入力を受け取る変数
	[SerializeField]
	private Vector2 inputDirection;
	// 1Pか2Pか3Pか、プレイヤーの番号
	public int playerNumber;
	// コントローラーの名前
	public string controllerName;
	// カーソルの待機時間
	[SerializeField]
	private float moveCursorFrame;
	// カーソルの最大待機時間(この時間を超えたら移動できる)
	[SerializeField]
	private float limitCursorFrame;

	private SEnum<TEnum> currentNumber;
	private TGameObject moveobject;

	/// <summary>
	/// カーソルの初期化処理
	/// </summary>
	/// <param name="_inputFlagY">カーソルが縦に動くか</param>
	/// <param name="_inputFlagX">横に動くか切り替えるためのフラグ</param>
	/// <param name="_playerNumber">プレイヤーの番号</param>
	/// <param name="_controllerName">コントローラーの名前</param>
	/// <param name="_limitCursolFrame">カーソルが動くまでの待機時間</param>
	/// <param name="_enum">ジェネリックのEnum。上位クラスで宣言したEnumの型にする</param>
	/// <param name="_moveObject">カーソルとして動かすオブジェクト(基本GameObject型で動かす)</param>
	public void InitCursol(bool _inputFlagY, bool _inputFlagX, int _playerNumber, string _controllerName, float _limitCursolFrame, TEnum _enum, GameObject _moveObject)
	{
		AcceptFlag = false;
		activeFlag = true;
		InputFlagDirectionY = _inputFlagY;
		InputFlagDirectionX = _inputFlagX;
		playerNumber = _playerNumber;
		limitCursorFrame = _limitCursolFrame;
		currentNumber = new SEnum<TEnum>(_enum);
		moveobject = _moveObject as TGameObject;
		SetController();
	}
	/// <summary>
	/// カーソルのアップデート(このメソッドを毎フレーム呼び出す)
	/// </summary>
	/// <param name="_movePosition">動かす位置のオブジェクト</param>
	public void CursolUpdate(GameObject[] _movePosition)
	{
		moveCursorFrame += Time.deltaTime;

		InputCursol();
		if (inputDirection != Vector2.zero && AcceptFlag == false)
		{
			if (moveCursorFrame >= limitCursorFrame)
			{
				InputCursolDirection(_movePosition);
			}
		}
		if (Input.GetButtonDown(string.Format("{0}Player{1}_Attack1", controllerName, playerNumber)))
		{
			AcceptButton();
		}
	}
	/// <summary>
	///	プレイヤーごとにコントローラーをセット
	/// </summary>
	public void SetController()
	{
		var controllerNames = Input.GetJoystickNames();
		if (playerNumber < controllerNames.Length)
		{
			if (controllerNames[playerNumber] != "")
			{
				controllerName = string.Format("{0}_", controllerNames[playerNumber]);
			}
		}
	}
	/// <summary>
	/// コントローラーの入力をとる
	/// </summary>
	public void InputCursol()
	{
		moveCursorFrame += Time.deltaTime;

		if (InputFlagDirectionX == true) inputDirection.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNumber));
		if (InputFlagDirectionY == true) inputDirection.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNumber));

		// オーバーフロー防止
		if (moveCursorFrame > 1.0f)
		{
			moveCursorFrame = 1.0f;
		}
	}
	/// <summary>
	/// カーソルを移動させる核になる
	/// </summary>
	/// <param name="_movePosition">動かす位置になるオブジェクト</param>
	public void InputCursolDirection(GameObject[] _movePosition)
	{
		if (inputDirection.x > 0)
		{
			currentNumber++;
		}
		else if (inputDirection.x < 0)
		{
			currentNumber--;
		}

		if (inputDirection.y > 0)
		{
			currentNumber++;
		}
		else if (inputDirection.y < 0)
		{
			currentNumber--;
		}
		CursolMove(_movePosition[Convert.ToInt32(currentNumber.currentNumber)]);

		moveCursorFrame = 0.0f;
	}
	/// <summary>
	/// カーソルの位置(posirion)を移動させる
	/// </summary>
	/// <param name="_characterSelectObjectData">キャラデータ(設定されているキャラごとのパネルの位置へ移動させる)</param>
	public void CursolMove(GameObject _movePosition)
	{
		Debug.Log("move");
		(moveobject as GameObject).transform.position = _movePosition.transform.position;
		Sound.LoadSE("Menu_MoveCursor", "Se_menu_moveCursor");
		Sound.PlaySE("Menu_MoveCursor", 1, 0.8f);
	}
	/// <summary>
	/// 決定の処理(クラスごとに処理書き換えて使う)
	/// </summary>
	public virtual void AcceptButton()
	{
		if (AcceptFlag == true) return; // 既に決定していた場合早期リターン
		Sound.LoadSE("Menu_Decision", "Se_menu_decision");
		Sound.PlaySE("Menu_Decision", 1, 1);
		AcceptFlag = true;
	}
}

/// <summary>
/// Generic型でEnumを操作するためのクラス
/// </summary>
/// <typeparam name="T"></typeparam>
class SEnum<T> where T : System.Enum
{
	public T currentNumber;
	public SEnum(T _initNumber)
	{
		currentNumber = _initNumber;
	}
	/// <summary>
	/// インクリメント(++)
	/// </summary>
	/// <param name="sEnum"></param>
	/// <returns></returns>
	public static SEnum<T> operator ++(SEnum<T> sEnum)
	{
		// Enumの要素をArray型に変換する
		var tempEnumValueArray = System.Enum.GetValues(typeof(T));
		// Enumの番号をintに変換する
		int currentNumberInt = Convert.ToInt32(sEnum.currentNumber);
		int valueNumberPosition = 0;
		// 今選択しているキャラの番号を一致させる
		foreach (int tempInt in tempEnumValueArray)
		{
			if (tempInt == currentNumberInt) break;
			valueNumberPosition++;
		}
		// tempEnumValueArrayの一致している要素名をストリングに変換し、Parseメソッドを使用してEnumに変換する
		// この時Arrayの要素を一つずらしているので次の要素を返す
		if (valueNumberPosition >= tempEnumValueArray.Length - 1)
		{
			valueNumberPosition--;
		}
		sEnum.currentNumber = (T)System.Enum.Parse(typeof(T), tempEnumValueArray.GetValue(valueNumberPosition + 1).ToString());
		return sEnum;
	}
	/// <summary>
	/// インクリメント(--)
	/// </summary>
	/// <param name="sEnum"></param>
	/// <returns></returns>
	public static SEnum<T> operator --(SEnum<T> sEnum)
	{
		// Enumの要素をArray型に変換する
		var tempEnumValueArray = System.Enum.GetValues(typeof(T));
		// Enumの番号をintに変換する
		int currentNumberInt = Convert.ToInt32(sEnum.currentNumber);
		int valueNumberPosition = 0;
		// 今選択しているキャラの番号を一致させる
		foreach (int tempInt in tempEnumValueArray)
		{
			if (tempInt == currentNumberInt) break;
			valueNumberPosition++;
		}
		// tempEnumValueArrayの一致している要素名をストリングに変換し、Parseメソッドを使用してEnumに変換する
		// この時Arrayの要素を一つずらしているので次の要素を返す
		if (valueNumberPosition == 0)
		{
			valueNumberPosition++;
		}
		sEnum.currentNumber = (T)System.Enum.Parse(typeof(T), tempEnumValueArray.GetValue(valueNumberPosition - 1).ToString());
		return sEnum;
	}
}

