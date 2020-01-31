﻿//----------------------------------------------------------------------------
//ファイル名：
//作成者　　：田嶋颯
//作成日　　：20190729
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
    private ScriptableInputManager  _obj = null;	//設定用変数、保存用変数
    Vector2 scrollPos = Vector2.zero;								//スクロールバー用位置変数
	private string[] playerTab = { "プレイヤー1", "プレイヤー2" };	//設定するプレイヤーを変更する為の変数
	private int playerTabNum = 0;
	private bool[,] isOpen;

	/// <summary>
	/// アセットパス
	/// </summary>
	private static readonly string ASSET_PATH = "Assets/#TAZIMA/Scripts/Input/ScriptableObjects/ScriptableInputManager.asset";
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
                if (GUILayout.Button("　　　 リセット 　　　"))
                {
                    inputManagerSetter.ClearInputManager();
                }
                //セーブした設定をセット
                if (GUILayout.Button("セーブした設定をセット"))
                {
                    inputManagerSetter.SetInputManager();
                }
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
			_obj.PlayerNum = EditorGUILayout.IntField("プレイヤー数", _obj.PlayerNum);
			_obj.ButtonNum = EditorGUILayout.IntField("ボタン設定数", _obj.ButtonNum);
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
					isOpen[playerTabNum ,i] = EditorGUILayout.Foldout(isOpen[playerTabNum, i], string.Format("ボタン{0}", i + 1));
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
									_obj.InputControllers[playerTabNum].Buttons[i].Name 
                                        = EditorGUILayout.TextField("名前", _obj.InputControllers[playerTabNum].Buttons[i].Name);
									break;
								case 1:
									_obj.InputControllers[playerTabNum].Buttons[i].InputButtonNum
                                        = Mathf.Clamp(EditorGUILayout.IntField("ボタン", _obj.InputControllers[playerTabNum].Buttons[i].InputButtonNum), 0, 15);
									break;
								case 2:
									_obj.InputControllers[playerTabNum].Buttons[i].AltButton
                                        = EditorGUILayout.TextField("デバッグキー", _obj.InputControllers[playerTabNum].Buttons[i].AltButton);
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
		//初期化
		if (_obj.InputControllers == null)
		{
			//リスト作成
			_obj.InputControllers = new List<SettingControllerClass>
			{
				new SettingControllerClass()
			};
			_obj.InputControllers[0].Buttons = new List<SettingButtonClass>
			{
				new SettingButtonClass()
			};

			_obj.SetPlayerNum = _obj.InputControllers.Count;
			_obj.SetButtonNum = _obj.InputControllers[0].Buttons.Count;
			isOpen = new bool[_obj.SetPlayerNum, _obj.SetButtonNum];
		}
		//リストを追加、削除したときの処理
		else if (_obj.PlayerNum != _obj.SetPlayerNum || _obj.ButtonNum != _obj.SetButtonNum)
		{
			//追加
			if (_obj.PlayerNum < _obj.SetPlayerNum)
			{
				for (int i = 0; i < _obj.SetPlayerNum - _obj.PlayerNum; i++)
				{
					_obj.InputControllers.Add(new SettingControllerClass());
				}
			}
			//削除
			else if (_obj.PlayerNum > _obj.SetPlayerNum)
			{
				for (int i = 0; i < _obj.PlayerNum - _obj.SetPlayerNum; i++)
				{
					_obj.InputControllers.RemoveAt(_obj.InputControllers.Count - 1);
				}
			}


		}

		if (_obj.InputControllers == null || _obj.PlayerNum != _obj.SetPlayerNum || _obj.ButtonNum != _obj.SetButtonNum)
        {
            //設定用変数に入力用変数の値を格納
            _obj.SetPlayerNum = _obj.PlayerNum;
            _obj.SetButtonNum = _obj.ButtonNum;
			//必要な初期化を行う
			isOpen = new bool[_obj.SetPlayerNum , _obj.SetButtonNum];
			var controllerList = new List<SettingControllerClass>();
			//プレイヤー分ループ
			for (int i = 0; i < _obj.SetPlayerNum; i++)
            {
				var controller = new SettingControllerClass();
				controller.Buttons = new List<SettingButtonClass>();
				//ボタン分ループ
				for (int j = 0; j < _obj.SetButtonNum; j++)
                {
                    var button = new SettingButtonClass();
					controller.Buttons.Add(button);
					isOpen[i, j] = false;
				}
				controllerList.Add(controller);
			}
            _obj.InputControllers = controllerList;
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
		isOpen = new bool[_obj.SetPlayerNum, _obj.SetButtonNum];
		Debug.Log("ロード完了");
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
        if (!AssetDatabase.Contains(sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            //アセット作成
            AssetDatabase.CreateAsset(sample, ASSET_PATH);
        }

		//コピー
		sample.Copy(_obj);

		////インスペクターから設定できないようにする
		sample.hideFlags = HideFlags.NotEditable;
		//更新通知
		EditorUtility.SetDirty(sample);
		//保存
		AssetDatabase.SaveAssets();
		//エディタを最新の状態にする
		AssetDatabase.Refresh();
		Debug.Log("セーブ完了");
	}
    #endregion
}
