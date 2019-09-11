//---------------------------------------
// HPバー
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.14
//--------------------------------------
// 更新履歴
// 2019.07.17 作成
// 2019.07.30 更新
//--------------------------------------
// 仕様
// 子オブジェクトに必要なもの
// ・Mask
// ・5:ダメージを受けたときにHpバーの色が変わるやつ
// ・4:Hpバーの緑のところ
// ・3:ブロックした時に色が変わるところ
// ・2:ダメージの赤
// ・1:一番後ろのbackground(0822現在、使用していない)
//----------------------------------------
// MEMO
// 参照は手動です
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
	public PlayerType playerType;

	public GameObject[] hpObjects = new GameObject[5];

	public GameObject effectsObject;

	public Image redImage;
	public RectTransform hpGuagePosition;
	public RectTransform greenRect, redRect, grayRect;
	public RectTransform hitRedGuage;

	public Vector3 initHpGuagePositon;
	public Vector3 limitHpGuagePosition;
    public Vector3 initPosition;

    private bool isUpdate = false;               //HPゲージを更新しているか
	private bool isTransparentRed = false;       //赤いところを透明にするか
	private bool isDeleteRed = false;            //赤いところを消すか (透明にしている時に攻撃されたら、消す)

	private float hpBarWidth;                    //hpバーの画像の長さ
	private float redAlphaValue;                 //赤いところのアルファ値
	public float transparentSpeed = 0.015f;      //赤いところを透明にする速度

	private int beforeHp;
	public int maxHp = 100;
	private float totalDamage = 0;
	private float beforeDamage = 0;

	private int redStartCnt = 0;
	private int redStartCntMax = 15;

	private float vibrationValue = 5;   //振動値

	/// <summary>
	/// Hpゲージを更新させる
	/// </summary>
	/// <param name="damage">現在のHp</param>
	public void Call_UpdateHpGuage(int currentHp)
	{
		if (beforeHp != currentHp)
		{
			float dmg = beforeHp - currentHp;
			UpdateHpGuage(dmg);
			isUpdate = true;
		}
		beforeHp = currentHp;
	}

	/// <summary>
	/// Hpゲージの更新
	/// </summary>
	/// <param name="dmg">ダメージ量</param>
	private void UpdateHpGuage(float dmg)
	{
		StartCoroutine(ReceiveDamageAction());
		redStartCnt = 0;
		if (isUpdate == true && isTransparentRed == true)
		{
			isDeleteRed = true;
		}
		beforeDamage = totalDamage;
		totalDamage += dmg;
		LowerHP();
	}

	/// <summary>
	/// 非ガード時のHP減少
	/// </summary>
	private void LowerHP()
	{
		greenRect.localPosition = new Vector3(CalcMove(maxHp, totalDamage), 0, 0);
		grayRect.localPosition = new Vector3(CalcMove(maxHp, totalDamage), 0, 0);
	}

	/// <summary>
	/// HPに更新があった時にする演出
	/// </summary>
	/// <returns></returns>
	private IEnumerator ReceiveDamageAction()
	{
		//Instantiate(effectsObject, initHpGuagePositon, Quaternion.identity);

		//ダメージを受けたときにHpバーの色が変わるやつ
		hpObjects[4].SetActive(true);
		hitRedGuage.localPosition = new Vector3(CalcMove(maxHp, totalDamage), 0, 0);
		//HPゲージを震わす
		Shake(0.5f, 100f);
		yield return new WaitForSeconds(0.1f);
		//HPゲージを元の位置に
		hpGuagePosition.position = initHpGuagePositon;
		//非表示にする
		hpObjects[4].SetActive(false);
	}

#region 揺らす処理
	/// <summary>
	/// ふるえるHP
	/// </summary>
	/// <param name="duration">揺れる期間</param>
	/// <param name="magnitude">揺れの大きさ</param>
	void Shake(float duration, float magnitude)
	{
		StartCoroutine(DoShake(duration, magnitude));
	}

	/// <summary>
	/// 揺らす値を受け取り揺らす
	/// </summary>
	/// <param name="duration">揺れる期間</param>
	/// <param name="magnitude">揺れの大きさ</param>
	/// <returns></returns>
	IEnumerator DoShake(float duration, float magnitude)
	{
		// 経過時間
		var elapsed = 0f;

		while (elapsed < duration)
		{
			var x = transform.position.x + Random.Range(-0.1f, 0.1f) * magnitude;
			var y = transform.position.y + Random.Range(-0.1f, 0.1f) * magnitude;

			transform.position = new Vector3(x, y, transform.position.z);

			elapsed += Time.deltaTime;

			yield return null;
			transform.position = initHpGuagePositon;
		}
	}
	#endregion

	/// <summary>
	/// 赤いところの操作
	/// </summary>
	private void OperateRed()
	{
		//赤いところが透明な時にHPの更新があった時の処理
		if (isDeleteRed == true)
		{
			//赤を透明にする
			redAlphaValue = 0.0f;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlphaValue);

			//位置の更新
			redRect.localPosition = new Vector3(CalcMove(maxHp, beforeDamage), 0, 0);

			//パラメータの初期化
			isDeleteRed = false;
			isTransparentRed = false;
			redStartCnt = 0;

			//HPバーが更新されている
			isUpdate = true;

			//赤を表示する
			redAlphaValue = 1.0f;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlphaValue);
		}
		//赤いところが完全に透明になったときの処理
		else if (redAlphaValue <= 0.0f)
		{
			//赤を合計のダメージ量のところへ移動させる
			redRect.localPosition = new Vector3(CalcMove(maxHp, totalDamage), 0, 0);

			//赤を赤くする
			isTransparentRed = false;
			redAlphaValue = 1.0f;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlphaValue);

			//パラメータの初期化
			redStartCnt = 0;
			isUpdate = false;
		}
		//赤いところを透明にする処理
		else if (isTransparentRed == true)
		{
			redAlphaValue -= transparentSpeed;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlphaValue);
		}
	}

	/// <summary>
	/// ダメージを受けたときに減らすHPバーの量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float hp, float damage)
	{
		float temp = hp + damage;

		if (playerType == PlayerType.P1)
		{
			return hpBarWidth * (hp - temp) / hp;
		}
		return hpBarWidth * (hp - temp) / hp * -1;
	}

	/// <summary>
	/// `キャラのHpを取得
	/// </summary>
	public void SetHpMax(int fighterHp)
	{
		maxHp = fighterHp;
		beforeHp = fighterHp;
	}

	private void Start()
	{
        //画像の横のサイズを取得
        hpBarWidth = hpObjects[0].GetComponent<RectTransform>().sizeDelta.x;


		//描画順番を指定
		for (int i = 0; i < 5; i++)
		{
			hpObjects[i].transform.SetSiblingIndex(i);
		}

		//初期ポジションを保存
		initHpGuagePositon = hpGuagePosition.position;
	}

	private void Update()
	{
		//HPの更新があったときに
		if (isUpdate == true)
		{
			//赤いところが透明になるまでのカウンタ
			if (redStartCnt <= redStartCntMax)
			{
				isTransparentRed = false;
				redStartCnt++;
			}
			else
			{
				isTransparentRed = true;
			}
		}
		OperateRed();
	}
}
