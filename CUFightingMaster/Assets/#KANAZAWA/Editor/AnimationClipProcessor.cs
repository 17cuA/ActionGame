#if UNITY_EDITOR
using System.Collections;
using UnityEngine;
using UnityEditor;

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
	/// モデルのImportSettingを設定
	/// </summary>
	/// <param name="_fbx">設定するFBX</param>
	/// <param name="_clipList">Clipの切り取りが記載されているテキストファイル</param>
    public static void SetModelImportSettings(ModelImporter _importer, TextAsset _clipList)
    {
        var list = new ArrayList();
        var text = new TextLoader();

		// 読み込むテキストファイルを渡し、情報を受け取る
		string[,] animationClipList = text.GetText(_clipList);
		if (animationClipList == null)
		{
			EditorUtility.DisplayDialog("Error", "TextFile not found.", "OK");
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
		_importer.clipAnimations = (ModelImporterClipAnimation[])list.ToArray(typeof(ModelImporterClipAnimation));
		// Humanoidに設定
		_importer.animationType = ModelImporterAnimationType.Human;

		// エラーが出なかった場合のみ読み込む
		if (success) AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(_importer));
	}
}
#endif