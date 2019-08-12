/*	CharacterStatus_Game.cs
 *	キャラクターのステータス(Game用)
 *	製作者：宮島幸大
 *	制作日：2019/06/07
 *	 ----------更新----------
 *	 2019/07/14 クラス名の変更 、UIの情報を別クラスへ　by takano
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
	public int hpMax;	//キャラクターの最大HP 
	public int hp;	//キャラクターのHP
	public int staminaMax; //キャラクターの最大スタミナ
	public int stamina;	//キャラクターのスタミナ
	public int Deathblow;

	//	デバッグ用
	public bool Hit;
	public float hitTime;

	//	初期化
	void Start()
    {
		hp = hpMax;
		stamina = staminaMax;
		//
		Hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Hit)
		{
			if(hitTime>=0)
			{
				hitTime -= Time.deltaTime;
			}
			else
			{
				Hit = false;
			}
		}
    }

	//	簡易的なダメージ処理
	public void Damage(int dm)
	{
		hp -= dm;
		Hit = true;
		hitTime = .5f;
	}

	//	リセット
	public void Reset()
	{
		hp = hpMax;
		stamina = staminaMax;
	}
}
//	Writer by Miyajima Kodai