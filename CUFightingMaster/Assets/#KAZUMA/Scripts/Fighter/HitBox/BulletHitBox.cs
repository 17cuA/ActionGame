﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Fighting/飛び道具")]
public class BulletHitBox : ScriptableObject
{
	public int maxFrame = 10;
	public bool isLoop = false;
	public List<FighterSkill.CustomHitBox> customHitBox = new List<FighterSkill.CustomHitBox>();//カスタム

	#region EDITOR_
#if UNITY_EDITOR
	[CustomEditor(typeof(BulletHitBox))]
	public class BulletSkillInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("技設定画面を開く"))
			{
				BulletEditor.Open((BulletHitBox)target);
			}
		}
	}
#endif
	#endregion

}
