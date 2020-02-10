#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;


public class AnimationEditor : EditorWindow
{
    private Object fbx = null;

	public Object FBX
	{
		set
		{
			if (Path.GetExtension(AssetDatabase.GetAssetPath(value)) == ".FBX") fbx = value;
		}
		get { return fbx; }
	}
	public TextAsset AnimationClipList { set; get; } = null;
	public string FilePath { set; get; } = null;

	[MenuItem("Editor/AnimationClipSetter")]
    public static void Open()
    {
        var window = GetWindow<AnimationEditor>();
		window.minSize = new Vector2(300, 75);
    }

    private void OnGUI()
    {
        // FBX
        FBX = EditorGUILayout.ObjectField("FBX", FBX, typeof(Object), false);
        // FBXのパス獲得
		FilePath = AssetDatabase.GetAssetPath(FBX);
		// テキストファイル
		AnimationClipList = EditorGUILayout.ObjectField("AnimationClipのリスト", AnimationClipList, typeof(TextAsset), false) as TextAsset;
		// AnimationClipの分割
        if (GUILayout.Button("AnimationClipの分割", GUILayout.Height(20), GUILayout.ExpandWidth(true)))
		{
			if (FBX && AnimationClipList)
			{
				AnimationClipProcessor.SetModelImportSettings(FilePath, AnimationClipList);
			}
		}
		// FighterSkillへのアタッチ
		if (GUILayout.Button("FighterSkillに設定", GUILayout.Height(20), GUILayout.ExpandWidth(true)))
		{
			if (FBX)
			{
				AnimationClipSetter.SetAnimationClipToFighterSkill(FilePath);
			}
		}
	}
}
#endif