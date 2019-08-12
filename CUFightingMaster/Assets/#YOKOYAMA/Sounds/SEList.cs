/*---------------------------------------------------
 * 制作日：2018/11/03
 * 制作者：横山凌
 * 機能：SEの生成を管理する
 * Unity側で流したいSEをセットする必要がある
 * 更新日：2018/11/03
 ----------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEList : MonoBehaviour
{
	// エフェクトの列挙方
	public enum SE
	{
		BGM,
		Down,
		guard_strong,
		guard_weak_medium,
		hit_medium,
		hit_strong,
		hit_weak,
		jump,
		punch_strong,
		punch_weak_medium,
		step,
		Num,
	}

	/*-----------------------
	 * 変数宣言
	 -----------------------*/
	public GameObject[] SEs = new GameObject[(int)SE.Num];
	public static SEList instance;

	/*-----------------------
	 * 初期化
	-----------------------*/
	void Awake()
	{
		instance = FindObjectOfType<SEList>();      // 自身をヒエラルキーから探し、呼び出す
	}

	/*-----------------------
	 * SEを生成する。
	-----------------------*/
	public void CreateSE(SE se)
	{
		Instantiate(SEs[(int)se]);
	}
}
