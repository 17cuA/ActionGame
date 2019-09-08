//----------------------------------------------------------------------------
//作成者：田嶋颯
//
//エディターウィンドウを表示して、値の設定を行えるようにするスクリプト。
//----------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class CreateInputManagerWindow : EditorWindow
{
    //inputManagerSetterクラス
    private InputManagerSetter inputManagerSetter = new InputManagerSetter();

    /// <summary>
    /// ScriptableInputManagerの変数
    /// </summary>
    private ScriptableInputManager _saveObj = null, _obj = null;
    //スクロールバー用位置変数
    Vector2 scrollPos = Vector2.zero;
    //設定するプレイヤーを変更する為の変数
    private string[] playerTab = { "プレイヤー1", "プレイヤー2" };
    private int playerTabNum = 0;
	private bool[,] isOpen;

	/// <summary>
	/// アセットパス
	/// </summary>
	private static readonly string ASSET_PATH = "Assets/Resources/ScriptableInputManager.asset";
    [MenuItem("Editor/InputManagerSetter")]
    static void Create()
    {
        //ウィンドウ生成
        CreateInputManagerWindow window = GetWindow<CreateInputManagerWindow>("CreateInputManager");
        window.minSize = new Vector2(380, 280);
	}

    /// <summary>
    /// GUI作成
    /// </summary>
    private void OnGUI()
    {
        if (_obj == null)
        {
            //読み込み
            Import();
            _obj.InputControllerButtons = null;
        }

        Color defaultColor = GUI.backgroundColor;
        #region 基本設定
        using (new GUILayout.VerticalScope())
        {
            #region InputManager設定
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("InputManager設定");
            }
            GUI.backgroundColor = defaultColor;
            using (new GUILayout.HorizontalScope())
            {
                //リセット
                if (GUILayout.Button("リセット"))
                {
                    inputManagerSetter.ClearInputManager();
                }
                //セーブした設定をセット
                if (GUILayout.Button("セーブした設定をセット"))
                {
                    inputManagerSetter.SetInputManager();
                }
            }
            if (GUILayout.Button("プログラムで設定したInputManagerをセット"))
            {
                inputManagerSetter.AutoSetInputManager();
            }
			if (GUILayout.Button("デフォルトの設定をセット"))
            {
                inputManagerSetter.AddGlobalInputSettings();
            }
            #endregion
            #region ロード&セーブ設定
            //ロード&セーブ
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("ロード&セーブ");
            }
            GUI.backgroundColor = defaultColor;
            using (new GUILayout.HorizontalScope())
            {
                //読み込みボタン
                if (GUILayout.Button("ロード"))
                {
                    Import();
                }
                //書き込みボタン
                if (GUILayout.Button("セーブ"))
                {
                    Export();
                }
            }
            #endregion
        }

        using (new GUILayout.VerticalScope())
        {
            #region 基本設定
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("基本設定");
            }
            GUI.backgroundColor = defaultColor;

            _obj.ControllerName = EditorGUILayout.TextField("コントローラー名", _obj.ControllerName);
            _obj.ButtonNum = EditorGUILayout.IntField("ボタン設定数", _obj.ButtonNum);
            _obj.PlayerNum = EditorGUILayout.IntField("プレイヤー数", _obj.PlayerNum);
			_obj.IsSetStick = EditorGUILayout.ToggleLeft("設定時に入力軸の設定を自動追加", _obj.IsSetStick);

            //コントローラー名表示ボタン
            if (GUILayout.Button("デバッグログに接続されているコントローラーを表示"))
            {
                OutputControllerName();
            }
            #endregion
        }
        #endregion
        #region コントローラー設定
        using (new GUILayout.VerticalScope())
        {
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("コントローラー設定（ボタン設定数を変更すると初期化されます）");
            }
            GUI.backgroundColor = defaultColor;
            #region ボタン設定
            //スクロール
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUI.skin.box);
            {
                //各ボタンの設定
                //コントローラーをセット
                SetController();
                //設定するプレイヤーの表示を変更する処理
                //プレイヤーを1人しか設定しない場合は設定を戻し表示しない
                if (_obj.SetPlayerNum == 1)
                {
                    if (playerTabNum != 0)  playerTabNum = 0;
                }
                else
                {
                    var index = playerTab.Length > 0 ? EditorGUILayout.Popup("設定するプレイヤー", playerTabNum, playerTab) : -1;
                    //変更された場合表示を変える
                    if (index != playerTabNum)  playerTabNum = index;
                }
                //ボタン分ループ
                for (int i = 0; i < _obj.SetButtonNum; i++)
                {
					//開いている場合ボタン設定できるようにする
					isOpen[playerTabNum, i] = EditorGUILayout.Foldout(isOpen[playerTabNum, i], string.Format("ボタン{0}", i + 1));
					if (isOpen[playerTabNum, i])
					{
						EditorGUI.indentLevel++;
						//1ボタン当たりに設定する項目数分ループ
						for (int j = 0; j < ScriptableInputManager.SetButtonInfo; j++)
						{
							//各ボタンをセット
							switch (j)
							{
								case 0:
									_obj.InputControllerButtons[playerTabNum][i].Name = EditorGUILayout.TextField("名前", _obj.InputControllerButtons[playerTabNum][i].Name);
									break;
								case 1:
									_obj.InputControllerButtons[playerTabNum][i].InputButtonNum =
										Mathf.Clamp(EditorGUILayout.IntField("ボタン", _obj.InputControllerButtons[playerTabNum][i].InputButtonNum), 0, 15);
									break;
								case 2:
									_obj.InputControllerButtons[playerTabNum][i].AltButton = EditorGUILayout.TextField("デバッグキー", _obj.InputControllerButtons[playerTabNum][i].AltButton);
									break;
							}
						}
						EditorGUI.indentLevel--;
					}
				}
            }
            EditorGUILayout.EndScrollView();
            #endregion
        }
        #endregion
    }

    #region コントローラの名前確認
    /// <summary>
    /// 接続が確認されているコントローラーの名前をデバッグログで表示するメソッド
    /// </summary>
    private void OutputControllerName()
    {
        var text = "";
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            //接続されていればコントローラー名、接続されていなければそれを知らせるログを表示
            if (Input.GetJoystickNames()[i] == "") text = "接続されていません";
            else text = Input.GetJoystickNames()[i];
            Debug.Log(string.Format("接続{0} => {1}", i, text));
        }
    }
    #endregion
    #region コントローラーのボタン設定用変数の初期化を行う
    /// <summary>
    /// 基本設定に応じて変数の初期化を行う
    /// </summary>
    private void SetController()
    {
		//リストの作成及び追加、削除を行ったときの表示エラーを回避するための初期化
		if (_obj.InputControllerButtons == null || _obj.PlayerNum != _obj.SetPlayerNum || _obj.ButtonNum != _obj.SetButtonNum)
        {
            //設定用変数に入力用変数の値を格納
            _obj.SetPlayerNum = _obj.PlayerNum;
            _obj.SetButtonNum = _obj.ButtonNum;
			//必要な初期化を行う
			isOpen = new bool[_obj.SetPlayerNum, _obj.SetButtonNum];
			var playerList = new List<List<ScriptableInputManager.InputControllerButton>>();
            //プレイヤー分ループ
            for (int i = 0; i < _obj.SetPlayerNum; i++)
            {
                var controllerList = new List<ScriptableInputManager.InputControllerButton>();
                //ボタン分ループ
                for (int j = 0; j < _obj.SetButtonNum; j++)
                {
                    var bottonList = new ScriptableInputManager.InputControllerButton();
					controllerList.Add(bottonList);
					isOpen[i, j] = false;
				}
                playerList.Add(controllerList);
            }
            _obj.InputControllerButtons = playerList;
        }
    }
    #endregion
    #region ロード
    /// <summary>
    /// ロード用メソッド
    /// </summary>
    private void Import()
    {
        if (_obj == null)
        {
            _obj = CreateInstance<ScriptableInputManager>();
            _saveObj = CreateInstance<ScriptableInputManager>();
        }

		ScriptableInputManager sample = AssetDatabase.LoadAssetAtPath<ScriptableInputManager>(ASSET_PATH);
		//ファイルが存在しない場合作成する
		if (sample == null)
		{
			sample = CreateInstance<ScriptableInputManager>();
			AssetDatabase.CreateAsset(sample, ASSET_PATH);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			return;
		}
        //コピーする
        _obj.Copy(sample);
	}
    #endregion
    #region 保存
    /// <summary>
    /// セーブ用メソッド
    /// </summary>
    private void Export()
    {
        //読み込み
        ScriptableInputManager sample = AssetDatabase.LoadAssetAtPath<ScriptableInputManager>(ASSET_PATH);
        if (sample == null)
        {
            sample = CreateInstance<ScriptableInputManager>();
        }

        //新規の場合は作成
        if (!AssetDatabase.Contains(_saveObj as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            //アセット作成
            AssetDatabase.CreateAsset(_saveObj, ASSET_PATH);
        }

        //コピー
        _saveObj.Copy(_obj);
        sample.Copy(_saveObj);

		////インスペクターから設定できないようにする
		_obj.hideFlags = HideFlags.NotEditable;
		//更新通知
		EditorUtility.SetDirty(_saveObj);
		//保存
		AssetDatabase.SaveAssets();
		//エディタを最新の状態にする
		AssetDatabase.Refresh();
	}
    #endregion
}
