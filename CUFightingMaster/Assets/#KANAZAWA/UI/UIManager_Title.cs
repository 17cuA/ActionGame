//---------------------------------------
// タイトル画面のテキストの表示
//---------------------------------------
// 作成者:金沢
// 作成日:2019.08.15
//--------------------------------------
// 更新履歴
// 2019.08.15 宮島が作成したものを元に画像を拡大・透過させる処理を作成
//--------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Title : MonoBehaviour
{     
	public Image visibleImage;  //	点滅させるImage

	[SerializeField] private float scaleTime;     // 現在時間
	private float scaleTime_Start;	// 処理を開始する時間
	private float scaleTime_Max;    // 処理を終了する時間
	private float scaleValue;	// 拡大の割合

	void Start()
	{
		scaleTime_Start = 0.5f;
		scaleTime_Max = 1.0f;
		scaleValue = 0.75f;
		// 画像を透明にしておく
		visibleImage.color = new Color(1, 1, 1, 0);
	}
	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		// 時間計測
		scaleTime += Time.deltaTime;

		// 開始時間になった時
		if (scaleTime >= scaleTime_Start)
		{
			// 画像を拡大する
			// Sin式を利用して徐々にスケールを足していく
			visibleImage.transform.localScale = new Vector3(1.0f + Mathf.Sin(scaleTime - scaleTime_Start) * scaleValue, 1.0f + Mathf.Sin(scaleTime - scaleTime_Start) * scaleValue, 1.0f);
			// 画像を透明にしていく
			visibleImage.color = new Color(1, 1, 1, 1 - scaleTime);
		}
		// 終了時間になった時
		if (scaleTime >= scaleTime_Max)
		{
			scaleTime = 0.0f;
		}
	}
}
