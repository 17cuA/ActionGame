#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class BulletEditorParameter : ScriptableSingleton<BulletEditorParameter>
{
	public BulletEditor window = null;
}
public class BulletEditor : EditorWindow
{
	public BulletHitBox skill = null;
	private Vector2 scrollPos;

	#region ウィンドウオープン
	public static void Open(BulletHitBox bs)
	{
		if (BulletEditorParameter.instance.window == null)
		{
			BulletEditorParameter.instance.window = (BulletEditor)CreateInstance(typeof(BulletEditor));
			BulletEditorParameter.instance.window.skill = bs;
			BulletEditorParameter.instance.window.Show();
		}
		else
		{
			BulletEditorParameter.instance.window.skill = bs;
		}
	}
	#endregion

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		CustomLabel(skill.name, Color.blue, Color.white, 20, FontStyle.Bold);
		if (GUILayout.Button("保存", GUILayout.Height(50), GUILayout.Width(80)))
		{
			//ダーティとしてマークする(変更があった事を記録する)
			EditorUtility.SetDirty(skill);

			//保存する
			AssetDatabase.SaveAssets();
		}
		EditorGUILayout.EndHorizontal();
		TabDraw();
		scrollPos = GUILayout.BeginScrollView(scrollPos);
		//Undoに対応
		Undo.RecordObject(skill, "skillstatus");
		switch (_tab)
		{
			case Tab.設定:
				SettingTabDraw();
				break;
			case Tab.当たり判定:
				HitBoxTabDraw();
				break;
		}
		GUILayout.EndScrollView();
		//エディタ全体の再描画
		EditorApplication.QueuePlayerLoopUpdate();
	}

	#region カスタムラベル_CustomLabel()
	void CustomLabel(string text, Color textColor, Color backColor, int fontSize, FontStyle fontStyle = FontStyle.Bold)
	{
		Color beforeBackColor = GUI.backgroundColor;

		GUIStyle guiStyle = new GUIStyle();
		GUIStyleState styleState = new GUIStyleState();

		styleState.textColor = textColor;
		styleState.background = Texture2D.whiteTexture;
		GUI.backgroundColor = backColor;
		guiStyle.normal = styleState;
		guiStyle.fontSize = fontSize;
		GUILayout.Label(text, guiStyle); //labelFieldだとうまくいかない

		GUI.backgroundColor = beforeBackColor;
	}
	#endregion

	#region 設定_Tab()
	private void SettingTabDraw()
	{
		skill.maxFrame = EditorGUILayout.IntField("フレーム数", skill.maxFrame);
		skill.isLoop = EditorGUILayout.Toggle("ループ", skill.isLoop);
	}
	#endregion

	#region 当たり判定_Tab
	private class FoldOutFlags
	{
		public bool foldOutFlag = false;
		public bool statusFlag = false;
		public bool effectFlag = false;
		public bool downMoveFlag = false;
	}
	private List<FoldOutFlags> foldOutFlags = new List<FoldOutFlags>();
	private void HitBoxTabDraw()
	{
		int i = 0;
		if (GUILayout.Button("当たり判定作成", GUILayout.Width(100), GUILayout.Height(30)))
		{
			skill.customHitBox.Add(new FighterSkill.CustomHitBox());
		}
		List<int> removeNumber = new List<int>();
		//カスタム当たり判定設定
		foreach (FighterSkill.CustomHitBox box in skill.customHitBox)
		{
			if (foldOutFlags.Count < i + 1)
			{
				while (foldOutFlags.Count < i + 1)
				{
					foldOutFlags.Add(new FoldOutFlags());
				}
			}
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.BeginHorizontal();
			//mode選択
			box.mode = (HitBoxMode)EditorGUILayout.EnumPopup(HitBoxMode.Bullet);
			if (GUILayout.Button("×", GUILayout.Width(20))) removeNumber.Add(i);
			EditorGUILayout.EndHorizontal();
			bool temp = foldOutFlags[i].foldOutFlag;
			//FoldOut
			foldOutFlags[i].foldOutFlag = FoldOutHitBox(box.frameHitBoxes, i.ToString(), ref temp, box);
			if (box != null)
			{
				if ((foldOutFlags[i].foldOutFlag) && (foldOutFlags[i].statusFlag = CustomUI.Foldout("ステータス", foldOutFlags[i].statusFlag)))
				{
					if (box.mode == HitBoxMode.HitBox|| box.mode == HitBoxMode.Bullet)
					{
						EditorGUILayout.BeginHorizontal();
						box.hitPoint = (HitPoint)EditorGUILayout.EnumPopup("上中下", box.hitPoint);
						box.hitStrength = (HitStrength)EditorGUILayout.EnumPopup("強弱", box.hitStrength);
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.BeginHorizontal();
						box.isDown = EditorGUILayout.Toggle("飛ばし（ダウン）技", box.isDown);
						box.isThrow = EditorGUILayout.Toggle("投げ技", box.isThrow);
						box.hitStop = EditorGUILayout.IntField("ヒットストップ値", box.hitStop);
						EditorGUILayout.EndHorizontal();
						if (box.isDown)
						{
							EditorGUILayout.BeginHorizontal();
							box.isFaceDown = EditorGUILayout.Toggle("うつ伏せダウン", box.isFaceDown);
							box.isPassiveNotPossible = EditorGUILayout.Toggle("受け身不可", box.isPassiveNotPossible);
							EditorGUILayout.EndHorizontal();
							if (foldOutFlags[i].downMoveFlag = CustomUI.Foldout("ダウン技移動量", foldOutFlags[i].downMoveFlag))
							{
								MovesSetting(ref box.isContinue, ref box.movements, ref box.gravityMoves);
							}

						}
						if (box.isThrow)
						{
							EditorGUILayout.BeginHorizontal();
							box.throwSkill = EditorGUILayout.ObjectField("投げモーション", box.throwSkill, typeof(FighterSkill), false) as FighterSkill;
							box.enemyThrowSkill = EditorGUILayout.ObjectField("投げられモーション", box.enemyThrowSkill, typeof(FighterSkill), false) as FighterSkill;
							EditorGUILayout.EndHorizontal();
							if (GUILayout.Button("投げダメージフレーム", GUILayout.Width(150), GUILayout.Height(20)))
							{
								box.throwDamages.Add(new FighterSkill.ThrowDamage());
							}
							for (int ef = 0; ef < box.throwDamages.Count; ef++)
							{
								//削除
								bool f = false;
								EditorGUILayout.BeginHorizontal();
								box.throwDamages[ef].frame = EditorGUILayout.IntField("フレーム", box.throwDamages[ef].frame);
								box.throwDamages[ef].damage = EditorGUILayout.IntField("ダメージ", box.throwDamages[ef].damage);
								if (GUILayout.Button("×", GUILayout.Width(20)))
								{
									f = true;
								}
								EditorGUILayout.EndHorizontal();
								if (f)
								{
									box.throwDamages.Remove(box.throwDamages[ef]);
								}
							}
						}
						EditorGUILayout.BeginHorizontal();
						box.damage = EditorGUILayout.IntField("ダメージ量", box.damage);
						box.stanDamage = EditorGUILayout.IntField("スタン値", box.stanDamage);
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.BeginHorizontal();
						box.knockBack = EditorGUILayout.FloatField("ノックバック値", box.knockBack);
						box.plusGauge = EditorGUILayout.IntField("ゲージ増加量", box.plusGauge);
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.BeginHorizontal();
						box.airKnockBack = EditorGUILayout.FloatField("空中ノックバック値", box.airKnockBack);
						box.guardKnockBack = EditorGUILayout.FloatField("ガードバック値", box.guardKnockBack);
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.BeginHorizontal();
						if (!box.isDown)
						{
							EditorGUILayout.BeginHorizontal();
							box.hitRigor = EditorGUILayout.IntField("ヒット硬直", box.hitRigor);
							EditorGUILayout.EndHorizontal();
						}
						box.guardHitRigor = EditorGUILayout.IntField("ガード硬直", box.guardHitRigor);
						EditorGUILayout.EndHorizontal();
					}
				}
				if ((foldOutFlags[i].foldOutFlag) && (foldOutFlags[i].effectFlag = CustomUI.Foldout("エフェクト", foldOutFlags[i].effectFlag)))
				{
					EditorGUILayout.BeginVertical("Box");
					if (box.mode == HitBoxMode.HitBox)
					{
						if (GUILayout.Button("ヒットエフェクト作成", GUILayout.Width(150), GUILayout.Height(20)))
						{
							box.hitEffects.Add(new FighterSkill.HitEffects());
						}
						for (int ef = 0; ef < box.hitEffects.Count; ef++)
						{
							EditorGUILayout.BeginVertical("Box");
							//削除
							bool f = false;
							EditorGUILayout.BeginHorizontal();
							box.hitEffects[ef].effect = EditorGUILayout.ObjectField("エフェクト", box.hitEffects[ef].effect, typeof(GameObject), true) as GameObject;
							box.hitEffects[ef].guardEffect = EditorGUILayout.ObjectField("ガードエフェクト", box.hitEffects[ef].guardEffect, typeof(GameObject), true) as GameObject;
							if (GUILayout.Button("×", GUILayout.Width(20)))
							{
								f = true;
							}
							EditorGUILayout.EndHorizontal();
							box.hitEffects[ef].position = EditorGUILayout.Vector3Field("ポジション", box.hitEffects[ef].position);
							EditorGUILayout.EndHorizontal();
							if (f)
							{
								box.hitEffects.Remove(box.hitEffects[ef]);
							}
						}
					}
					EditorGUILayout.EndHorizontal();
				}

			}

			i++;
			EditorGUILayout.EndVertical();
		}
		//削除
		for (int j = 0; j < removeNumber.Count; j++)
		{
			skill.customHitBox.RemoveAt(removeNumber[j]);
		}
	}
	//移動量設定欄
	private void MovesSetting(ref bool _continue, ref List<FighterSkill.Move> _move, ref List<FighterSkill.GravityMove> _grav)
	{
		_continue = EditorGUILayout.Toggle("制動継続", _continue);
		if (GUILayout.Button("移動作成", GUILayout.Width(80)))
		{
			_move.Add(new FighterSkill.Move());
		}
		for (int i = 0; i < _move.Count; i++)
		{
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.BeginHorizontal();
			bool removeFrag = false;
			_move[i].startFrame = EditorGUILayout.IntField("スタートフレーム", _move[i].startFrame);
			//削除ボタン
			if (GUILayout.Button("×", GUILayout.Width(20)))
			{
				removeFrag = true;
			}
			//削除
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginVertical("Box");
			//Vector3入力
			_move[i].movement = EditorGUILayout.Vector3Field("移動量", _move[i].movement);
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			if (removeFrag) _move.RemoveAt(i);
		}
		if (!_continue)
		{
			if (GUILayout.Button("制動作成", GUILayout.Width(80)))
			{
				_grav.Add(new FighterSkill.GravityMove());
			}
			for (int i = 0; i < _grav.Count; i++)
			{
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.BeginHorizontal();
				bool removeFrag = false;
				_grav[i].startFrame = EditorGUILayout.IntField("スタートフレーム", _grav[i].startFrame);
				//削除ボタン
				if (GUILayout.Button("×", GUILayout.Width(20)))
				{
					removeFrag = true;
				}
				//削除
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginVertical("Box");
				//Vector3入力
				_grav[i].movement = EditorGUILayout.Vector3Field("移動量", _grav[i].movement);
				//Vector3入力
				_grav[i].limitMove = EditorGUILayout.Vector3Field("移動量限界", _grav[i].limitMove);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				if (removeFrag) _grav.RemoveAt(i);
			}
		}
	}
	//ヒットボックス個々（FoldOutした中身）
	private bool FoldOutHitBox(List<FighterSkill.FrameHitBox> frameHitBox, string label, ref bool frag, FighterSkill.CustomHitBox cus = null)
	{
		if (frag = CustomUI.Foldout(label, frag))
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(frameHitBox.Count.ToString());
			if (GUILayout.Button("判定作成", GUILayout.Width(80)))
			{
				frameHitBox.Add(new FighterSkill.FrameHitBox());
			}
			EditorGUILayout.EndHorizontal();
			//スライダーその他の作成
			for (int i = 0; i < frameHitBox.Count; i++)
			{
				EditorGUILayout.BeginVertical("Box");
				//数値の入れ替え
				float frameStart = frameHitBox[i].startFrame;
				float frameEnd = frameHitBox[i].endFrame;
				EditorGUILayout.BeginHorizontal();
				//左
				frameStart = (int)EditorGUILayout.FloatField(frameStart, GUILayout.Width(30));
				if (frameStart > frameEnd) frameStart = frameEnd - 1;
				if (frameStart < 0) frameStart = 0;
				//スライダー
				EditorGUILayout.MinMaxSlider(ref frameStart, ref frameEnd, 0, skill.maxFrame);
				//右
				frameEnd = (int)EditorGUILayout.FloatField(frameEnd, GUILayout.Width(30));
				if (frameEnd < frameStart) frameEnd = frameEnd - 1;
				if (frameEnd > skill.maxFrame) frameEnd = skill.maxFrame;
				//削除
				bool f = false;
				if (GUILayout.Button("×", GUILayout.Width(20)))
				{
					f = true;
				}
				EditorGUILayout.EndHorizontal();

				frameHitBox[i].startFrame = (int)frameStart;
				frameHitBox[i].endFrame = (int)frameEnd;

				//当たり判定の設定
				HitFoldOut(frameHitBox[i]);

				EditorGUILayout.EndVertical();
				if (f)
				{
					frameHitBox.Remove(frameHitBox[i]);
				}

			}
		}
		return frag;
	}

	//当たり判定
	private void HitFoldOut(FighterSkill.FrameHitBox hitBox_)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Position");
		hitBox_.hitBox.localPosition.x = EditorGUILayout.FloatField("X", hitBox_.hitBox.localPosition.x);
		hitBox_.hitBox.localPosition.y = EditorGUILayout.FloatField("Y", hitBox_.hitBox.localPosition.y);
		hitBox_.hitBox.localPosition.z = EditorGUILayout.FloatField("Z", hitBox_.hitBox.localPosition.z);
		EditorGUILayout.EndVertical();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("サイズ");
		hitBox_.hitBox.size.x = EditorGUILayout.FloatField("X", hitBox_.hitBox.size.x);
		hitBox_.hitBox.size.y = EditorGUILayout.FloatField("Y", hitBox_.hitBox.size.y);
		hitBox_.hitBox.size.z = EditorGUILayout.FloatField("Z", hitBox_.hitBox.size.z);
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();

	}

	#endregion



	#region タブ_Styles
	private enum Tab
	{
		設定,
		当たり判定,
	}
	private Tab _tab = Tab.設定;
	// Style定義
	private static class Styles
	{
		private static GUIContent[] _tabToggles = null;
		public static GUIContent[] TabToggles
		{
			get
			{
				if (_tabToggles == null)
				{
					_tabToggles = System.Enum.GetNames(typeof(Tab)).Select(x => new GUIContent(x)).ToArray();
				}
				return _tabToggles;
			}
		}

		public static readonly GUIStyle TabButtonStyle = "LargeButton";

		// GUI.ToolbarButtonSize.FitToContentsも設定できる
		public static readonly GUI.ToolbarButtonSize TabButtonSize = GUI.ToolbarButtonSize.Fixed;
	}
	private void TabDraw()
	{
		//タブ表示
		using (new EditorGUILayout.HorizontalScope())
		{
			GUILayout.FlexibleSpace();
			// タブを描画する
			_tab = (Tab)GUILayout.Toolbar((int)_tab, Styles.TabToggles, Styles.TabButtonStyle, Styles.TabButtonSize);
			GUILayout.FlexibleSpace();
		}
	}
	#endregion


}
#endif
