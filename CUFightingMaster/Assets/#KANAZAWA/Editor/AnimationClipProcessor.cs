using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AnimationClipProcessor : AssetPostprocessor
{
    // AnimationClipの項目
    private enum EAnimationClipInfo
    {
        name,
        firstFrame,
        lastFrame,
        isLoop,
    }
	private static bool success = true;

    /// <summary>
    /// モデルのImportSettingを設定する
    /// </summary>
    /// <param name="_importer">選択したモデルのModelImporter</param>
    static void SetModelImportSettings(ModelImporter _importer)
    {
        var list = new ArrayList();
        var text = new TextLoader();
        // 読み込むテキストファイルのパスを渡す
        string[,] animationClipList = text.GetText("Animation/AnimationClipList");
		if (animationClipList == null)
		{
			EditorUtility.DisplayDialog("Error", "\"AnimationClipList\" not found.", "OK");
			success = false;
			return;
		}
        // リストに項目を追加
        for (int i = 0; i < animationClipList.GetLength(0); i++)
        {
            var clip = new ModelImporterClipAnimation();
            for (int j = 0; j < animationClipList.GetLength(1); j++)
            {
                switch(j)
                {
                    case (int)EAnimationClipInfo.name:
                        clip.name = animationClipList[i, j];
                        break;
                    case (int)EAnimationClipInfo.firstFrame:
                        clip.firstFrame = int.Parse(animationClipList[i, j]);
                        break;
                    case (int)EAnimationClipInfo.lastFrame:
                        clip.lastFrame = int.Parse(animationClipList[i, j]);
                        break;
                    case (int)EAnimationClipInfo.isLoop:
                        if (int.Parse(animationClipList[i, j]) == 0) clip.loopTime = false;
                        else clip.loopTime = true;
                        break;
                }
                clip.lockRootHeightY = true;
                clip.lockRootPositionXZ = true;
                clip.lockRootRotation = true;
                clip.keepOriginalOrientation = true;
                clip.keepOriginalPositionXZ = true;
                clip.keepOriginalPositionY = true;
            }
            list.Add(clip);
        }
        // 引数のオブジェクトのClipAnimationを変更
        _importer.clipAnimations = (ModelImporterClipAnimation[])
        // リストを配列(ClipAnimation)に変更
        list.ToArray(typeof(ModelImporterClipAnimation));

        // Humanoidに設定
        _importer.animationType = ModelImporterAnimationType.Human;
    }

	static void SetAnimationClipToSkill(Object obj)
	{
		var clipList = new List<AnimationClip>();
		clipList.Clear();
		var clip = obj as AnimationClip;
		if (clip != null)
		{
			//clipList.Add(clip);
		}
		FighterSkill fighterSkill = Resources.Load<FighterSkill>("Skills/Idle");
		//fighterSkill.animationClip = clip;
	}

	/// <summary>
	///  FBXを選択した時のみ実行可能にする
	/// </summary>
	/// <returns>選択したオブジェクトの形式が.fbxか</returns>
	[MenuItem("Assets/Set Animation Options", validate = true)]
    private static bool ShowMenu()
    {
        return Path.GetExtension(AssetDatabase.GetAssetPath(Selection.activeObject)).ToLower() == ".fbx";
    }

    /// <summary>
    /// 選択したオブジェクトの情報を取得し、ImportSettingsを設定する
    /// </summary>
    [MenuItem("Assets/Set Animation Options")]
    static void SetAnimationOptions()
    {
		var obj = Selection.activeObject;
        // 選択したオブジェクトのファイルパスを取得
        string path = AssetDatabase.GetAssetPath(obj);
        // 取得したパスからオブジェクトのAssetImporterを取得
        AssetImporter importer = AssetImporter.GetAtPath(path);
        // 取得したAssetImporterのModelImporterを渡して関数呼び出し
        SetModelImportSettings(importer as ModelImporter);
		// エラーが出なかった場合のみ読み込む
		if (success) AssetDatabase.ImportAsset(path);

		//SetAnimationClipToSkill(obj);
	}
}
