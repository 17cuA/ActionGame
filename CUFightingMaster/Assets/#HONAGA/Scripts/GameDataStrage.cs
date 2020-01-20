using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;
using System;

public class GameDataStrage : MonoBehaviour
{
	#region Singleton
	public bool dontDs;

	private static GameDataStrage instance;
	public static GameDataStrage Instance
	{
		get
		{
			if (instance == null)
			{
				Type t = typeof(GameDataStrage);

				instance = (GameDataStrage)FindObjectOfType(t);
				if (instance == null)
				{
					var _ins = new GameObject();
					_ins.name = "GameDataStrage";
					instance = _ins.AddComponent<GameDataStrage>();
				}
			}

			return instance;
		}
	}

	private void Awake()
	{
		// 他のゲームオブジェクトにアタッチされているか調べる
		// アタッチされている場合は破棄する
		CheckInstance();


	}

	protected bool CheckInstance()
	{
		if (instance == null)
		{
			instance = this as GameDataStrage;
			if (dontDs == true)
			{
				DontDestroyOnLoad(this.gameObject);
			}
			return true;
		}
		else if (Instance == this)
		{
			return true;
		}
		Destroy(this.gameObject);
		return false;
	}
	#endregion

	public AnimationClip[,] animationClips = new AnimationClip[3,3];
	public FighterStatus[] fighterStatuses = new FighterStatus[3];

	public int plusAttackDamage_One = 0;
	public int plusAttackDamage_Two = 0;

	public bool winFlag_PlayerOne;
	public bool winFlag_PlayerTwo;

	public MatchResult matchResult = MatchResult.NONE;

	public GameObject fighterModel_P1;
	public GameObject fighterModel_P2;

	//デバッグ用のダメージ
	public void SetPlusDamage(PlayerNumber _num,int _dam)
	{
		if(_num ==PlayerNumber.Player1)
		{
			plusAttackDamage_One += _dam;	}
		else if(_num==PlayerNumber.Player2)
		{
			plusAttackDamage_Two += _dam;
		}
	}
	public int GetPlusDamage(PlayerNumber _num)
	{
		if (_num == PlayerNumber.Player1)
		{
			return plusAttackDamage_One;
		}
		else if (_num == PlayerNumber.Player2)
		{
			return plusAttackDamage_Two;
		}
		return 0;
	}
}
