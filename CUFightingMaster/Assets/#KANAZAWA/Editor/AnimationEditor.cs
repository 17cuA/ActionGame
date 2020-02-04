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
		window.minSize = new Vector2(300, 55);
    }

    private void OnGUI()
    {
        // FBX
        fbx = EditorGUILayout.ObjectField("FBX", fbx, typeof(Object), false);
        // FBX以外入らないようにする
		string path = AssetDatabase.GetAssetPath(fbx);
		if (Path.GetExtension(path.ToLower()) != ".fbx") fbx = null;
		// 取得したパスからオブジェクトのAssetImporterを取得
		ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;
		// テキストファイル
		animationClipList = EditorGUILayout.ObjectField("AnimationClipList", animationClipList, typeof(TextAsset), false) as TextAsset;
        if (GUILayout.Button("Set AnimationClip", GUILayout.Height(20), GUILayout.ExpandWidth(true)))
		{
			if (fbx && animationClipList)
			{
				AnimationClipProcessor.SetModelImportSettings(importer, animationClipList);
			}
		}
		if (GUILayout.Button("Set FighterSkill", GUILayout.Height(20), GUILayout.ExpandWidth(true)))
		{
			AnimationClipSetter.SetAnimationClipToFighterSkill(path);
		}
    }
}
#endif