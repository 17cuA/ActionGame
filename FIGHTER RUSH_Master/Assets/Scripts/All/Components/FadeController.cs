////---------------------------------------------------------------
//// フェードの処理を
////---------------------------------------------------------------
//// 作成者:三沢
//// 作成日:2019.07.12
////----------------------------------------------------
//// 更新履歴
//// 2019.07.12 フェードの処理を分解し作成
////-----------------------------------------------------
//// 仕様
//// 関数を呼び出して使うかも
////-----------------------------------------------------
//// MEMO
//// 現在は未使用。いつか使う時までさようなら
////----------------------------------------------------
//// 現在判明しているバグ
////----------------------------------------------------

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class FadeController : MonoBehaviour
//{
//	private float fadeSpeed;						// フェードイン・アウトの速度
//	private float red, green, blue, alpha;	// パネルの色、不透明度を管理
//	private bool isFadeIn = false;				// フェードインの処理の開始、完了の管理するフラグ
//	private bool isFadeOut = false;			//フェードアウトの処理の開始、完了の管理するフラグ
//	Image fadeImage;								// 透明度を変更するパネルのイメージ

//	// Start is called before the first frame update
//	void Start()
//	{
//		fadeImage = GetComponent<Image>();		// Imageを設定
//		fadeSpeed = 2;											// フェードの速度を設定
//		alpha = 1;													// アルファ値を設定
//	}

//	// Update is called once per frame
//	void Update()
//	{

//	}

//	// フェードイン
//	void FadeIn()
//	{
//		fadeImage.enabled = true;							// パネルを有効化
//		alpha -= 1 / fadeSpeed * Time.deltaTime;	// 不透明度を徐々に下げる
//		SetAlpha();												// 変更した不透明度をパネルに設定する
//		if (alpha <= 0)
//		{
//			isFadeIn = false;									// フェードイン完了
//			fadeImage.enabled = false;						// パネルの表示をオフ
//		}
//	}

//	// フェードアウト
//	void FadeOut()
//	{
//		fadeImage.enabled = true;							// パネルを有効化
//		alpha += 1 / fadeSpeed * Time.deltaTime;	// 不透明度を徐々に下げる
//		SetAlpha();												// 変更した不透明度をパネルに設定する
//		if (alpha >= 1)
//		{
//			isFadeOut = false;									// フェードイン完了
//			fadeImage.enabled = false;						// パネルの表示をオフ
//		}
//	}

//	// アルファ値の更新
//	void SetAlpha()
//	{
//		fadeImage.color = new Color(red, green, blue, alpha);
//	}
//}
