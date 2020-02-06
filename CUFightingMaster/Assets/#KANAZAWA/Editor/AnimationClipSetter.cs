#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class AnimationClipSetter : AssetPostprocessor
{
	/// <summary>
	/// FBXのAnimationClipを読み込みFighterSkillに取り付ける
	/// </summary>
	/// <param name="_importer">FBXのパス</param>
	public static void SetAnimationClipToFighterSkill(string _importer)
	{
		var animations = AssetDatabase.LoadAllAssetsAtPath(_importer);
		ModelImporter importer = AssetImporter.GetAtPath(_importer) as ModelImporter;
		for (int i = 0; i < importer.clipAnimations.Length; i++)
		{
			// オブジェクトからAnimationClipの情報だけを取る
			var animationList = System.Array.Find(animations, item => item is AnimationClip && item.name == importer.clipAnimations[i].name);
			AnimationClip clip = animationList as AnimationClip;
			// AnimationClipの名前でFighterSkillを検索する
			FighterSkill skill = Resources.Load<FighterSkill>("Skills/" + clip.name);
			// FighterSkillが見つからなかった場合作成する
			if (skill == null)
			{
				skill = FighterSkillCreater.CreateFighterSkill(clip.name);
			}
			skill.animationClip = clip;
		}
		AssetDatabase.Refresh();
	}
}

public class FighterSkillCreater : ScriptableObject
{
	/// <summary>
	/// FighterSkillを作成する
	/// </summary>
	/// <param name="_name">FighterSkillの名前</param>
	/// <returns></returns>
	public static FighterSkill CreateFighterSkill(string _name)
	{
		string path = "Assets/Resources/Skills/";
		FighterSkill skill = CreateInstance<FighterSkill>();
		AssetDatabase.CreateAsset(skill, path + _name + ".asset");
		return skill;
	}
}
#endif