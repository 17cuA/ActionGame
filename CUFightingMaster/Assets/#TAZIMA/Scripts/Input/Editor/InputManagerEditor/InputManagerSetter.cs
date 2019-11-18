//----------------------------------------------------------------------------
//作成者：田嶋颯
//
//InputManagerに設定を追加するスクリプト
//----------------------------------------------------------------------------
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


/// <summary>
/// InputManagerを自動的に設定してくれるクラス
/// </summary>
public class InputManagerSetter
{
	/// <summary>
	/// インプットマネージャーの設定をクリアします。
	/// </summary>
	public void ClearInputManager()
	{
		InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

		Debug.Log("設定を全てクリアします。");
		inputManagerGenerator.Clear();
	}
	/// <summary>
	/// インプットマネージャーを設定します。
	/// </summary>
	public void SetInputManager()
	{
		Debug.Log("インプットマネージャーの設定を開始します。");
		InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();
        //保存されている設定を読み込み
        ScriptableInputManager scriptableInputManager = AssetDatabase.LoadAssetAtPath<ScriptableInputManager>("Assets/#TAZIMA/Scripts/Input/ScriptableObjects/ScriptableInputManager.asset");

        //ファイルが存在しない場合作成しない
        if (scriptableInputManager == null) return;
        //設定したデータでInputManagerを作成する
        for (int i = 0; i < scriptableInputManager.SetPlayerNum; ++i)
        {
            AddPlayerInputSettings(inputManagerGenerator, scriptableInputManager, i);
        }
        Debug.Log(string.Format("{0}を設定しました。", scriptableInputManager.ControllerName));
        Debug.Log("インプットマネージャーの設定が完了しました。");
	}

    /// <summary>
    /// 入力設定を追加する
    /// </summary>
    /// <param name="_inputManagerGenerator">Input manager generator.</param>
    /// <param name="_playerNum">Player number.</param>
    private static void AddPlayerInputSettings(InputManagerGenerator _inputManagerGenerator, ScriptableInputManager _controller, int _player)
	{
		string upKey = "", downKey = "", leftKey = "", rightKey = "";
		GetAxisKey(out upKey, out downKey, out leftKey, out rightKey, _player);

		int joystickNum = _player + 1;

		#region コントローラーのボタンを設定
		//スティックの設定
		if (_controller.IsSetStick == true)
		{
			//横方向
			{
				var name = string.Format("{0}_Player{1}_Horizontal", _controller.ControllerName, _player);
				_inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 1));
				_inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, leftKey, rightKey, "", ""));
			}

			//縦方向
			{
				var name = string.Format("{0}_Player{1}_Vertical", _controller.ControllerName, _player);
				_inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 2));
				_inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, upKey, downKey, "", ""));
			}
		}

        //ボタン
        //設定したボタン数作成する
        for (int i = 0; i < _controller.ButtonNum; i++)
        {
			//ボタン名
			var name = "";
			//コントローラーで入力する為のボタン
            var button = "";
			if (_controller.ControllerName == "")
			{
				name = string.Format("Player{0}_{1}", _player, _controller.InputControllers[_player].Buttons[i].Name);
			}
			else
			{
				name = string.Format("{0}_Player{1}_{2}", _controller.ControllerName, _player, _controller.InputControllers[_player].Buttons[i].Name);
				button = string.Format("joystick {0} button {1}", joystickNum, _controller.InputControllers[_player].Buttons[i].InputButtonNum);
			}
            //デバッグ用のキー
            var key = _controller.InputControllers[_player].Buttons[i].AltButton;
            //キーを設定
            _inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, button, key));
        }
		#endregion
	}

	/// <summary>
	/// デフォルト設定を追加する（キャンセルなど）
	/// </summary>
	/// <param name="inputManagerGenerator">Input manager generator.</param>
	public void AddGlobalInputSettings()
	{
		InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();
		// 軸
		{
			var name = "Horizontal";
			inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "", ""));
		}
		{
			var name = "Vertical";
			inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "", ""));
		}
		// キャンセル
		{
			var name = "Cancel";
			inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "", ""));
		}
		// サブ
		{
			var name = "Submit";
			inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "", ""));
		}
	}

	/// <summary>
	/// デバッグ用のキーを設定する
	/// </summary>
	/// <param name="_upKey">Up key.</param>
	/// <param name="_downKey">Down key.</param>
	/// <param name="_leftKey">Left key.</param>
	/// <param name="_rightKey">Right key.</param>
	/// <param name="_attackKey1">Attack key1.</param>
	/// <param name="_attackKey2">Attack key2.</param>
	/// <param name="_attackKey3">Attack key3.</param>
	/// <param name="_playerNum">Player number.</param>
	private static void GetAxisKey(out string _upKey, out string _downKey, out string _leftKey, out string _rightKey, out string _attackKey1, out string _attackKey2, out string _attackKey3, int _player)
	{
		_upKey = "w";
		_downKey = "s";
		_leftKey = "a";
		_rightKey = "d";
		_attackKey1 = "f";
		_attackKey2 = "g";
		_attackKey3 = "h";

        switch (_player)
        {
            case 0:
                _upKey = "w";
                _downKey = "s";
                _leftKey = "a";
                _rightKey = "d";
                _attackKey1 = "f";
                _attackKey2 = "g";
                _attackKey3 = "h";
                break;
            case 1:
                _upKey = "up";
                _downKey = "down";
                _leftKey = "left";
                _rightKey = "right";
                _attackKey1 = "[1]";
                _attackKey2 = "[2]";
                _attackKey3 = "[3]";
                break;
            default:
                break;
        }
    }
    private static void GetAxisKey(out string _upKey, out string _downKey, out string _leftKey, out string _rightKey, int _player)
    {
        _upKey = "";
        _downKey = "";
        _leftKey = "";
        _rightKey = "";

        switch (_player)
        {
            case 0:
                _upKey = "w";
                _downKey = "s";
                _leftKey = "a";
                _rightKey = "d";
                break;
            case 1:
                _upKey = "up";
                _downKey = "down";
                _leftKey = "left";
                _rightKey = "right";
                break;
            default:
                break;
        }
    }
}
#endif