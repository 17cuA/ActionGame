/*------------------------------------------------------------------------------------------------------------------------------------
 * まずはエディタ拡張のお決まりとして、Editorという名前をつけたフォルダを用意する必要があります。
 * Unityプロジェクト以下/Assets/などにEditorフォルダを作成しておいてください。
 *
 * ※Ｅｄｉｔｏｒフォルダの場所は、Assets以下ならどこのサブフォルダでも大丈夫です。
 * たとえば、Assets/ゲームタイトル/Editor/とか。
 *
 * 独自にウィンドウを作成する場合はUnityEditor.EditorWindowを継承して必要なGUIを置いていく形になります。
 *
 * ----------------------------------------------------チェックポイント！----------------------------------------------------
 * ・エディタ拡張関連のファイルはEditorフォルダを作ってそこに入れる。
 * ・ウィンドウはEditorWindowから派生させて作る。
 * ・using UnityEditor;をお忘れなく。
 * ・MenuItemでメニューからメソッドを呼び出せるようになる
 * ・ staticメソッドじゃないとダメなので注意！
 * ・ たまに私も忘れて小一時間悩んだり(ﾎﾞｿｯ
 * ・EditorWindow.GetWindow()で表示＆取得
 * ・ 表示されているなら既存のものを返すだけ、ないなら新しく作って表示もしてくれる
 * ・OnGUI()でGUIをいろいろ配置
 * ・いろいろなGUIの表示方法についてはまたの機会に解説いたします！
------------------------------------------------------------------------------------------------------------------------------------ */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// typeof で渡されたクラスがインスペクタに表示される際に、
// このカスタムエディタクラスが適用され、
// 自由にカスタマイズすることが可能になります
[CustomEditor(typeof(EditorExWindow))]
public class EditorExWindow : ScriptableWizard
{
	public enum Mode
	{
		Setup,          // UIを作れる環境を用意する
		Confirmation,   // UIの変更を許可するか別ウィンドウで確認する
		Running,        // すべての手順を終え、UIの設定をいじれるモードへ
	}

	// スクリプト処理のステップを管理
	public Mode mode = Mode.Setup;



	public static EditorExWindow window;
	private Object obj;
	private GameObject gameobj;
	[MenuItem("見本/EditorEx")]
	static void Open()
	{
		if (window == null)
		{
			window = DisplayWizard<EditorExWindow>("EditorExWindow");
		}
	}
	Vector2 leftScrollPos = Vector2.zero;

	bool toggle = false;
	string textField = "";
	string textArea = "";
	string password = "";
	float horizontalScrollbar = 0.0f;
	float verticalScrollbar = 0.0f;
	float horizontalSlider = 0.0f;
	float verticalSlider = 0.0f;
	int toolbar = 0;
	int selectionGrid = 0;

	void OnGUI()
	{
		toggle = GUILayout.Toggle(toggle, "Toggle");
		if (toggle == true)
		{
			mode = Mode.Running;
		}
		else
		{
			mode = Mode.Setup;
		}
		switch (mode)
		{
			case Mode.Setup:

				//エディターをスクロールさせる(開始位置)-------------------------------------------------
				//ここから下に書かれるものはエディター上でスクロール範囲に含まれるできる
				leftScrollPos = EditorGUILayout.BeginScrollView(leftScrollPos, GUI.skin.box);


				EditorGUILayout.LabelField("ようこそ！　Unityエディタ拡張の沼へ！");

				GUILayout.Label("Label : GUILayoutはUnityEngine側なので、ランタイムでもそのまま使える系");

				if (GUILayout.Button("Button"))
					Debug.Log("Button!");

				if (GUILayout.RepeatButton("RepeatButton"))
					Debug.Log("RepeatButton!");


				GUILayout.Label("TextField");
				textField = GUILayout.TextField(textField);

				GUILayout.Label("TextArea");
				textArea = GUILayout.TextArea(textArea);

				GUILayout.Label("PasswordField");
				password = GUILayout.PasswordField(password, '*');

				GUILayout.Label("HorizontalScrollbar");
				float horizontalSize = 10.0f;// sizeはバーのサイズ(0～100のスクロールバーで10なので、全体に対して10分の1サイズ)
				horizontalScrollbar = GUILayout.HorizontalScrollbar(horizontalScrollbar, horizontalSize, 0.0f, 100.0f);

				GUILayout.Label("VerticalScrollbar");
				float verticalSize = 10.0f;// sizeはバーのサイズ(0～100のスクロールバーで10なので、全体に対して10分の1サイズ)
				verticalScrollbar = GUILayout.VerticalScrollbar(verticalScrollbar, verticalSize, 0.0f, 100.0f);

				GUILayout.Label("HorizontalSlider");
				horizontalSlider = GUILayout.HorizontalSlider(horizontalSlider, 0.0f, 100.0f);

				GUILayout.Label("VerticalSlider");
				verticalSlider = GUILayout.VerticalSlider(verticalSlider, 0.0f, 100.0f);

				GUILayout.Label("Toolbar");
				toolbar = GUILayout.Toolbar(toolbar, new string[] { "Tool1", "Tool2", "Tool3" });

				GUILayout.Label("SelectionGrid");
				selectionGrid = GUILayout.SelectionGrid(selectionGrid, new string[] { "Grid 1", "Grid 2", "Grid 3", "Grid 4" }, 2);

				GUILayout.Box("Box");

				GUILayout.Label("ここからSpace");
				GUILayout.Space(100);
				GUILayout.Label("ここまでSpace");

				GUILayout.Label("ここからFlexibleSpace");
				GUILayout.FlexibleSpace();
				GUILayout.Label("ここまでFlexibleSpace");
        
				//エディターをスクロールさせる(開始位置)-------------------------------------------------
				//ここから下に書かれるものはエディター上でスクロール範囲に含まれるできる
				EditorGUILayout.EndScrollView();

				break;
			case Mode.Confirmation:
				break;
			case Mode.Running:
				break;
			default:
				break;
		}
	}
}
