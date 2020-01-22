using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

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

    /// <summary>
    /// モデルのImportSettingを設定する
    /// </summary>
    /// <param name="_importer">選択したモデルのModelImporter</param>
    static void SetModelImportSettings(ModelImporter _importer)
    {
        var list = new ArrayList();
        var text = new TextLoader();
        // 読み込むテキストファイルのパスを渡す
        string[,] AnimationClipList = text.GetText("Animation/AnimationClipList");
        // リストに項目を追加
        for (int i = 0; i < AnimationClipList.GetLength(0); i++)
        {
            var clip = new ModelImporterClipAnimation();
            for (int j = 0; j < AnimationClipList.GetLength(1); j++)
            {
                switch(j)
                {
                    case (int)EAnimationClipInfo.name:
                        clip.name = AnimationClipList[i, j];
                        break;
                    case (int)EAnimationClipInfo.firstFrame:
                        clip.firstFrame = int.Parse(AnimationClipList[i, j]);
                        break;
                    case (int)EAnimationClipInfo.lastFrame:
                        clip.lastFrame = int.Parse(AnimationClipList[i, j]);
                        break;
                    case (int)EAnimationClipInfo.isLoop:
                        if (int.Parse(AnimationClipList[i, j]) == 0) clip.loopTime = false;
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
        // 選択したオブジェクトのファイルパスを取得
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        // 取得したパスからオブジェクトのAssetImporterを取得
        AssetImporter importer = AssetImporter.GetAtPath(path);
        // 取得したAssetImporterのModelImporterを渡して関数呼び出し
        SetModelImportSettings(importer as ModelImporter);
        // オブジェクトの情報を更新？
        AssetDatabase.ImportAsset(path);
    }
}
