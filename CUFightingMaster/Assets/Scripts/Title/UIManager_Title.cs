/*	UIManager_Title.cs
 *	TitleシーンのUI管理スクリプト
 *	製作者：宮島幸大
 *	制作日：2019/06/13
 *	----------更新----------
 *	2019/07/04：コメントがない、変数名がわかりづらいところほぼ変更(三沢)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Title : MonoBehaviour
{
	public Image buttonImage;       //	点滅させるImage
									//	点滅用
	[SerializeField]
	private float speed;        // 点滅させる速度
	private float time;     // 点滅させる時間

	void Start()
	{
		speed = 0.5f;
	}
	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		// 点滅させるImageのアルファ値を取得
		buttonImage.color = GetAlphaColor(buttonImage.color);
	}
	//	--------------------
	//	文字を点滅させる関数
	//	作成者：宮島 幸大
	//	--------------------
	//	Alpha値を更新してColorを返す
	Color GetAlphaColor(Color color)
	{
		time += Time.deltaTime * 5.0f * speed;  // 点滅させる速度
		color.a = Mathf.Sin(time) * 0.5f + 0.5f;        // 不透明度変更

		return color;
	}
}
//	write by Miyajima Kodai