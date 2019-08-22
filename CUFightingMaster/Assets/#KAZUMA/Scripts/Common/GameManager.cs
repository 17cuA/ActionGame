using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;
using CUEngine;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField, Header("ゲームの設定")]
    private GameSettings settings;
	public GameSettings Settings
	{
        get { return settings; }
    }


    public FighterCore Player_one;
    public FighterCore Player_two;
	public TestInput input_one;
	public TestInput input_two;

    public bool isStartGame = false;

    //ヒットストップ
    private int hitStop_one = 0;
	private int hitStop_two = 0;
	private bool isHitStop_one = false;
	private bool isHitStop_two = false;
	void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		//Inputの初期化
        input_one.InitCommandManagers(Player_one);
        input_two.InitCommandManagers(Player_two);
        isStartGame = false;
    }
	//Updateの順番の管理
	private void Update()
	{
		//ポーズ中にプレイヤーの動きを止める処理
		if (Mathf.Approximately(Time.timeScale, 0f))		return;

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
