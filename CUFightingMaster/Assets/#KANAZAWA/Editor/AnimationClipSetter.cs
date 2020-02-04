using UnityEngine;
using UnityEditor;

public class AnimationClipSetter : AssetPostprocessor
{
	public static void SetAnimationClipToFighterSkill(string _importer)
	{
		var animations = AssetDatabase.LoadAllAssetsAtPath(_importer);
		ModelImporter importer = AssetImporter.GetAtPath(_importer) as ModelImporter;

		for (int i = 0; i < importer.clipAnimations.Length; i++)
		{
			var animationList = System.Array.Find(animations, item => item is AnimationClip && item.name == importer.clipAnimations[i].name);
			AnimationClip clip = animationList as AnimationClip;
			var skill = Resources.Load<FighterSkill>("Skills/" + clip.name);
			if (skill != null)
			{
				skill.animationClip = clip;
			}
		}
	}
}
