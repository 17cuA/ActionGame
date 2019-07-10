/*------------------------------------------------------------------------------------------------------------------------------------
 *BeginScrollView～EndScrollViewで、範囲内のサイズに合わせて自動的に表示する機能
 * 基本はBeginVertical～EndVerticalなどと同じようにBeginScrollView～EndScrollViewで囲って使用します。
 * 現在の位置を保持しておくためにVector2の受け渡しが必要なのでそこは注意してください。
 * 
 * それぞれのSizeを弄ると数が増えますが、表示されている範囲外まで延びると自動的にスクロールバーが出るのがわかるかと思います。
 * ちなみに、今回はサンプルのため事前にBeginVerticalなどで範囲指定してそこにSizeのSliderとScrollViewを置きましたが、
 * ScrollViewだけでも自動的に縦並びになりますので、場合によってVertical指定は省略できます。
 * 
 * ----------------------------------------------------チェックポイント！----------------------------------------------------
 * ・GUILayout.HorizontalScrollbarなどよりEditorGUILayout.BeginScrollView～EndScrollViewを使うほうが楽で便利。
 * ・囲っている範囲内のサイズは自動的に調整されるので、特別なことをしない限りサイズ指定がいらない。
 * ・ScrollViewの範囲内は自動的に縦並びになる。
 ------------------------------------------------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorExWindowScrollView : ScriptableWizard
{
	public static EditorExWindowScrollView window;
	[MenuItem("見本/ScrollView")]
	static void Open()
	{
		if (window == null)
		{
			window = DisplayWizard<EditorExWindowScrollView>("ScrollView");
		}
	}
	int leftSize = 0;
	Vector2 leftScrollPos = Vector2.zero;
	int rightSize = 0;
	Vector2 rightScrollPos = Vector2.zero;

	Vector2 allScroollPos = Vector2.zero;


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

    int Min = 0;
    int Max = 50;

	[SerializeField]
	struct UI
	{
		Object obj;
		Texture2D texture2;
		Vector3 pos;
	}
	List<Texture2D> UIimages = new List<Texture2D>();
	
	void OnGUI()
	{
		// ビュー全体のスクロール管理--------------------------------------------------------------------------------
		allScroollPos = EditorGUILayout.BeginScrollView(allScroollPos, GUI.skin.box);
		//--------------------------------------------------------------------------------
		EditorGUILayout.LabelField("ようこそ！　Unityエディタ拡張の沼へ！");

		// 横にレイヤーを並べる構造へ変更する
		EditorGUILayout.BeginHorizontal(GUI.skin.box);

		// ボックス内のレイヤーを構成--------------------------------------------------------------------------------
		SettingUI();
		//SyncList();
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUILayout.LabelField("右側");

		rightSize = EditorGUILayout.IntSlider("Size", rightSize, Min, Max, GUILayout.ExpandWidth(false));

		// 右側のスクロール
		rightScrollPos = EditorGUILayout.BeginScrollView(rightScrollPos, GUI.skin.box);
		{
			// スクロール範囲

			for (int y = 0; y < rightSize; y++)
			{
				EditorGUILayout.BeginHorizontal(GUI.skin.box);
				{
					// ここの範囲は横並び

					EditorGUILayout.PrefixLabel("Index " + y);

					// 下に行くほどボタン数増やす
					for (int i = 0; i < y + 1; i++)
					{
						// ボタン(横幅100px)
						if (GUILayout.Button("Button" + i, GUILayout.Width(100)))
						{
							Debug.Log("Button" + i + "押したよ");
						}
					}
				}
				EditorGUILayout.EndHorizontal();
			}
			// こんな感じで横幅固定しなくても、範囲からはみ出すときにスクロールバー出してくれる。
		}
		EditorGUILayout.EndScrollView();

		EditorGUILayout.EndVertical();

		EditorGUILayout.EndHorizontal();

	
		EditorGUILayout.BeginVertical(GUI.skin.box);

		EditorGUILayout.LabelField("ようこそ！　Unityエディタ拡張の沼へ！");

		GUILayout.Label("Label : GUILayoutはUnityEngine側なので、ランタイムでもそのまま使える系");

		if (GUILayout.Button("Button"))
			Debug.Log("Button!");

		if (GUILayout.RepeatButton("RepeatButton"))
			Debug.Log("RepeatButton!");

		toggle = GUILayout.Toggle(toggle, "Toggle");

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

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();

	}

	/// <summary>
	/// UIを設置
	/// leftSizeで変数の数を調整
	/// leftScrollPosでスクロール処理を行う
	/// </summary>
	void SettingUI()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(300));
		EditorGUILayout.LabelField("左側");

		leftSize = EditorGUILayout.IntSlider("Size", leftSize, 0, 50, GUILayout.ExpandWidth(false));

		// 左側のスクロールビュー(横幅300px)
		leftScrollPos = EditorGUILayout.BeginScrollView(leftScrollPos, GUILayout.Height(300));
		{
			// スクロール範囲
			for (int i = 0; i < leftSize; i++)
			{

				/*UIimages[i] = (Texture2D)*/EditorGUILayout.ObjectField("UI画像" + i,null, typeof(Texture2D), false);
				EditorGUILayout.Vector3Field("座標" + i, Vector3.zero);
			}
		}
		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}

	void SyncList()
	{
		if (UIimages.Count != leftSize)
		{
			UIimages = new List<Texture2D>(leftSize);
			Debug.Log(UIimages.Count);
			Debug.Log(leftSize);
		}

	}
}