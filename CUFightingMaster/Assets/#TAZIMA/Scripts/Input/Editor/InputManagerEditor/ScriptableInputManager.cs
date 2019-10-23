//----------------------------------------------------------------------------
//作成者：田嶋颯
//
//InputManagerに追加する為の設定を保存するクラス。
//----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableInputManager : ScriptableObject
{
	//コントローラー名
	[SerializeField]
	private string _controllerName;
	public string ControllerName
	{
		get { return _controllerName; }
#if UNITY_EDITOR
		set { _controllerName = value; }
#endif
	}

    //プレイヤー数
    [SerializeField]
    private int _playerNum;
    public int PlayerNum
    {
        get { return _playerNum; }
#if UNITY_EDITOR
        set { _playerNum = Mathf.Clamp(value, 1, 2); }
#endif
    }
    //プレイヤー数（設定用）
    [SerializeField]
    private int _setPlayerNum;
    public int SetPlayerNum
    {
        get { return _setPlayerNum; }
#if UNITY_EDITOR
        set { _setPlayerNum = Mathf.Clamp(value, 1, 2); }
#endif
    }

    //ボタン数
    [SerializeField]
	private int _buttonNum;
	public int ButtonNum
	{
		get { return _buttonNum; }
#if UNITY_EDITOR
		set { _buttonNum = Mathf.Clamp(value, 1, 16); }
#endif
	}

	//ボタン数（設定用）
	[SerializeField]
	private int _setButtonNum;
	public int SetButtonNum
	{
		get { return _setButtonNum; }
#if UNITY_EDITOR
		set { _setButtonNum = Mathf.Clamp(value, 1, 16); }
#endif
	}

    //1コントローラー当たりに設定できる最大ボタン数
    public static int MaxButtonNum = 16;
    //1ボタン当たりに設定する項目数
    public static int SetButtonInfo = 3;

	[SerializeField]
	private List<SettingControllerClass> _inputControllers;
	public List<SettingControllerClass> InputControllers
	{
		get { return _inputControllers; }
#if UNITY_EDITOR
		set { _inputControllers = value; }
#endif
	}

	//スティックの設定を追加するか
	[SerializeField]
	private bool _isSetStick;
	public bool IsSetStick
	{
		get { return _isSetStick; }
#if UNITY_EDITOR
		set { _isSetStick = value; }
#endif
	}

    #region コピー用
#if UNITY_EDITOR
    public void Copy(ScriptableInputManager sobj)
	{
		_controllerName = sobj.ControllerName;
        _playerNum = sobj.PlayerNum;
        _setPlayerNum = sobj.SetPlayerNum;
        _buttonNum = sobj.ButtonNum;
        _setButtonNum = sobj.SetButtonNum;

		_inputControllers = sobj.InputControllers;
		_isSetStick = sobj.IsSetStick;
	}
#endif
	#endregion
}

//コントローラー設定用
[System.Serializable]
public class SettingControllerClass
{
	[SerializeField]
	public List<SettingButtonClass> Buttons;
}
　
//ボタン設定用
[System.Serializable]
public class SettingButtonClass
{
	//ボタンの名前
	[SerializeField]
	public string Name;
	//ボタンのラベルを判別するのに使用
	[SerializeField]
	public int InputButtonNum;
	//デバッグ用のキーを設定するのに使用
	[SerializeField]
	public string AltButton;
}
