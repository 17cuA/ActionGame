using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;
using CUEngine;
using System;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField, Header("ゲームの設定")]
    private GameSettings settings = null;
	public GameSettings Settings
	{
        get { return settings; }
    }

    public int fighterAmount = 2;
	public GameObject parantFighter = null;
	public FighterStateBase oneState;
	public FighterStateBase twoState;

    public FighterCore Player_one;
    public FighterCore Player_two;
	public TestInput input_one;
	public TestInput input_two;

    public bool isStartGame = false;
    public bool isEndRound = false;

    //ヒットストップ
    private int hitStop_one = 0;
	private int hitStop_two = 0;
	private bool isHitStop_one = false;
	private bool isHitStop_two = false;

    public List<IEventable> UpdateBulletList = new List<IEventable>();
    public List<IEventable> LateUpdateBulletList = new List<IEventable>();
    public List<IEventable> DeleteBulletList = new List<IEventable>();
	override protected void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		base.Awake();
		//プレイヤー1生成
		var obj = Instantiate(GameDataStrage.Instance.fighterStatuses[0].fighter,InGameManager.Instance.targetPoint[0].transform.position,Quaternion.identity);
		if (parantFighter != null)
		{
			obj.transform.parent = parantFighter.transform;
		}
		FighterCore _co = obj.GetComponent<FighterCore>();
		_co.SetPlayerNumber(PlayerNumber.Player1);
		oneState.core = _co;
		Player_one = _co;
        CommonConstants.SetLayerRecursively(_co.gameObject, LayerMask.NameToLayer(CommonConstants.Layers.Player_One));
		//プレイヤー2生成
		obj = Instantiate(GameDataStrage.Instance.fighterStatuses[1].fighter,InGameManager.Instance.targetPoint[1].transform.position,Quaternion.identity);
		if (parantFighter != null)
		{
			obj.transform.parent = parantFighter.transform;
		}
		_co = obj.GetComponent<FighterCore>();
		_co.SetPlayerNumber(PlayerNumber.Player2);
		twoState.core = _co;
		Player_two = _co;
        CommonConstants.SetLayerRecursively(_co.gameObject, LayerMask.NameToLayer(CommonConstants.Layers.Player_Two));
    }

    void Start()
	{
        DeleteBulletList = new List<IEventable>();//削除用
                                                  //Inputの初期化
        input_one.InitCommandManagers(Player_one);
        input_two.InitCommandManagers(Player_two);
        isStartGame = false;
    }
	//Updateの順番の管理
	private void Update()
	{
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f))		return;
        //遠距離のUpdate,UpdateManagerで処理するため、UpdateManagerより前
        foreach(var lis in UpdateBulletList)
		{
            lis.UpdateGame();
        }

		input_one.UpdateGame(Player_one);
		input_two.UpdateGame(Player_two);
		UpdateManager.Instance.UpdateGame();
		//ヒットストップがない時
		if ((hitStop_one <= 0) || (!isHitStop_one))
		{
			isHitStop_one = false;
			Player_one.UpdateGame();
            //コマンドの削除
            input_one.DeleteCommand();
        }
		//ヒットストップ中
		else
		{
			Player_one.HitStopUpdate();
			hitStop_one--;
		}
		if ((hitStop_two <= 0) || (!isHitStop_two))
		{
			isHitStop_two = false;
			Player_two.UpdateGame();
            //コマンドの削除
            input_two.DeleteCommand();
        }
		else
		{
			Player_two.HitStopUpdate();
			hitStop_two--;
		}


		if ((hitStop_one <= 0) || (!isHitStop_one))
		{
			if(hitStop_one<=0)
			{
				Player_one.KnockBackUpdate();
			}
        }
		if ((hitStop_two <= 0) || (!isHitStop_two))
		{
			if (hitStop_one <= 0)
			{
				Player_two.KnockBackUpdate();
			}
        }


		if(hitStop_one>0)
		{
			isHitStop_one = true;
			
		}
		if(hitStop_two>0)
		{
			isHitStop_two = true;
		}
		//UpdateManagerで使用するため、Gameobjectの削除はUpdateManagerより後
		foreach(var lis in LateUpdateBulletList)
		{
            lis.LateUpdateGame();
        }
        int i = 0;
        foreach(var del in DeleteBulletList)
		{
            i++;
            UpdateBulletList.Remove(del);
            LateUpdateBulletList.Remove(del);
        }
		if(i>0)
		{
            DeleteBulletList = new List<IEventable>();//削除用
        }

		if(isEndRound)
		{
            Player_one.SetComboCount(0);
            Player_two.SetComboCount(0);
        }
		Player_one.Mover.SetAirXMove(0);
		Player_two.Mover.SetAirXMove(0);

	}
	public FighterCore GetPlayFighterCore(PlayerNumber _mode)
	{
		switch (_mode)
		{
			case PlayerNumber.Player1:
                return Player_one;
			case PlayerNumber.Player2:
                return Player_two;
        }
        return null;
    }
	public int GetHitStop(PlayerNumber _mode)
	{
		switch (_mode)
		{
			case PlayerNumber.Player1:
				return hitStop_one;
			case PlayerNumber.Player2:
				return hitStop_two;
		}
		return 0;
	}
	public void SetHitStop(PlayerNumber _mode,int _stop)
	{
		switch (_mode)
		{
			case PlayerNumber.Player1:
				hitStop_one = _stop;
				isHitStop_one = false;
				break;
			case PlayerNumber.Player2:
				hitStop_two = _stop;
				isHitStop_two = false;
				break;
		}
	}
	public FighterCore GetPlayFighterCore(int _layer)
	{
		if(_layer == LayerMask.NameToLayer(CommonConstants.Layers.Player_One))
		{
            return Player_one;
        }
		else if(_layer == LayerMask.NameToLayer(CommonConstants.Layers.Player_Two))
		{
            return Player_two;
        }
        return null;
    }
}
