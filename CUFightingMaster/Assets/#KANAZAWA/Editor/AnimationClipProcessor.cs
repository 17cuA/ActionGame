using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class AnimationClipProcessor : AssetPostprocessor
{
    private enum eAnimationClipInfo
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
        ArrayList list = new ArrayList();
        TextLoader text = new TextLoader();
        text.LoadText("AnimationClipList");
        for (int i = 0; i < text.textWords.GetLength(0); i++)
        {
            ModelImporterClipAnimation clip = new ModelImporterClipAnimation();

            for (int j = 0; j < text.textWords.GetLength(1); j++)
            {
                switch(j)
                {
                    case (int)eAnimationClipInfo.name:
                        clip.name = text.textWords[i, j];
                        break;
                    case (int)eAnimationClipInfo.firstFrame:
                        clip.firstFrame = int.Parse(text.textWords[i, j]);
                        break;
                    case (int)eAnimationClipInfo.lastFrame:
                        clip.lastFrame = int.Parse(text.textWords[i, j]);
                        break;
                    case (int)eAnimationClipInfo.isLoop:
                        if (text.textWords[i, j] == null)
                        {
                            clip.loop = false;
                        }
                        else clip.loop = true;
                        break;
                }
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
