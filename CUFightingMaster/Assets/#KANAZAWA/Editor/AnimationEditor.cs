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
        if(Path.GetExtension(AssetDatabase.GetAssetPath(fbx).ToLower()) != ".fbx") fbx = null;
        // テキストファイル
        animationClipList = EditorGUILayout.ObjectField("AnimationClipList", animationClipList, typeof(TextAsset), false) as TextAsset;
        if (GUILayout.Button("Set AnimationClip", GUILayout.Height(20), GUILayout.ExpandWidth(true)))
		{
			if (fbx && animationClipList)
			{
				AnimationClipProcessor.SetModelImportSettings(fbx, animationClipList);
			}
		}
    }
}
#endif