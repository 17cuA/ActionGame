//--------------------------------------------------------
//ファイル名：AnimationUISetting.cs
//作成者　　：田嶋颯
//作成日　　：20190830
//
//AnimationUIManagerをつけたオブジェクトをセットし初期化処理を行う
//--------------------------------------------------------
using UnityEngine;

public class AnimationUISetting : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Awake()
    {
        for (int i = 0;i < gameObjects.Length;i++)
        {
			var animUIManagers = gameObjects[i].GetComponents<AnimationUIManager>();
			for (int j = 0;j < animUIManagers.Length; j++)
			{
				animUIManagers[j].Init();
			}
		}
    }
}
