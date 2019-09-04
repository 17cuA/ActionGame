﻿using System.Collections;
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




	public FighterStatus[] fighterStatuses = new FighterStatus[2];
	public int plusAttackDamage = 0;
}
