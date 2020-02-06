#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;


public class AnimationEditor : EditorWindow
{
    public Object fbx = null;
    public TextAsset animationClipList = null;

    [MenuItem("Editor/AnimationClipSetter")]
    public static void Open()
    {
        var window = GetWindow<AnimationEditor>();
		window.minSize = new Vector2(300, 75);
    }

    private void OnGUI()
    {
        // FBX
        fbx = EditorGUILayout.ObjectField("FBX", fbx, typeof(Object), false);
        // FBX以外入らないようにする
		string path = AssetDatabase.GetAssetPath(fbx);
		if (Path.GetExtension(path.ToLower()) != ".fbx") fbx = null;
		// テキストファイル
		animationClipList = EditorGUILayout.ObjectField("AnimationClipのリスト", animationClipList, typeof(TextAsset), false) as TextAsset;
		// AnimationClipの分割
        if (GUILayout.Button("AnimationClipの分割", GUILayout.Height(20), GUILayout.ExpandWidth(true)))
		{
			if (fbx && animationClipList)
			{
				AnimationClipProcessor.SetModelImportSettings(path, animationClipList);
			}
		}
		// FighterSkillへのアタッチ
		if (GUILayout.Button("FighterSkillに設定", GUILayout.Height(20), GUILayout.ExpandWidth(true)))
		{
			if (fbx)
			{
				AnimationClipSetter.SetAnimationClipToFighterSkill(path);
			}
		}
	}
}
#endif