using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class AnimationProcessor : EditorWindow
{
    public Object fbx = null;
    public TextAsset animationClipList = null;

    [MenuItem("Editor/AnimationProcessor")]
    public static void Open()
    {
        GetWindow<AnimationProcessor>();
    }

    private void OnGUI()
    {
        // FBX
        fbx = EditorGUILayout.ObjectField("FBX", fbx, typeof(Object), false);
        // FBX以外入らないようにする(もっといい方法ありますよね)
        if(Path.GetExtension(AssetDatabase.GetAssetPath(fbx).ToLower()) != ".fbx") fbx = null;
        // テキストファイル
        animationClipList = EditorGUILayout.ObjectField("AnimationClipList", animationClipList, typeof(TextAsset), false) as TextAsset;
        EditorGUILayout.Space();
        GUILayout.Button("Split AnimationClip", GUILayout.Height(20), GUILayout.ExpandWidth(true));
    }
}
