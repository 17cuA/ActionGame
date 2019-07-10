using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
	PlayerInputManager pInputManager = new PlayerInputManager();					//プレイヤーの入力を見て保存するスクリプト
	TestCommandSystem commandSystem;			//正規表現でコマンドを判別するスクリプト


	[SerializeField]
	private float speed = 8f;
	float moveX = 0f;
	float jumpPower = 0f;
	Rigidbody rb;
	bool rightDir;                  //プレイヤーが右向きか（true=>右,false=>左）（コマンド発動時や移動、反転で使うと思う）
	bool crouching = false;     //しゃがんでいるか

	public int playerIndex; //プレイヤー番号（0 => Player01,1 => Player02）

    public string debugCommandStr;      //コマンドの文字列を確認するための変数

	//初期化
	void Start()
    {
		pInputManager.playerIndex = playerIndex;
		pInputManager.player = string.Format("Player{0}", playerIndex);
		rb = gameObject.GetComponent<Rigidbody>();
		//transform.position = new Vector3(2.5f, 0, 0);

        //正規表現でコマンドを判別するスクリプトの変数初期化
        commandSystem = gameObject.GetComponent<TestCommandSystem>();
		commandSystem.Init();
	}

    //更新
    void Update()
    {
		// 登場シーンが終わるまで動かせない
		if (UIManager_Game.instance.call_Once)
		{
            //移動、攻撃などの入力の管理
            pInputManager.DownKeyCheck();
            //コマンドの入力
            //コマンド含む攻撃処理
            commandSystem.GetCommandData(pInputManager.playerDirection.ToString());
            debugCommandStr = commandSystem.inputCommandData;
            //プレイヤーの挙動（true=>右向き,false=>左向き）
            if (pInputManager.atkBotton != "")
			{
                //コマンド処理
                if (commandSystem.commandName != null)
                    //コマンド発動条件処理をここに書く（弱中強とか）
                    Debug.Log("aaa" + pInputManager.player + "_" + commandSystem.commandName + pInputManager.atkBotton);
                //攻撃処理
                else
                    Debug.Log("ooo" + pInputManager.player + pInputManager.atkBotton);
			}
			//スティックの入力に応じた挙動
			else
			{
				//Y移動
				switch ((pInputManager.playerDirection - 1) / 3)
				{
					case 0:
						//しゃがみ処理
						Debug.Log(pInputManager.player);
						if (!crouching) crouching = true;
						jumpPower = 0f;
						break;
					case 1:
						if (crouching) crouching = false;
						jumpPower = 0f;
						break;
					case 2:
						if (crouching) crouching = false;
							jumpPower = 1f;
						break;
					default:
						break;
				}
				//X移動
				switch (pInputManager.playerDirection % 3)
				{
					case 0:
						moveX = speed;
						break;
					case 1:
						moveX = -speed;
						break;
					case 2:
						moveX = 0;
						break;
					default:
						break;
				}
				//ジャンプ用
				transform.Translate(moveX / 30, jumpPower, 0);
			}
		}
	}
	void LateUpdate()
	{
		// 登場シーンが終わるまで動かせない
		if (UIManager_Game.instance.call_Once)
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, CameraControll.instance.lBottom.x + 1.0f, CameraControll.instance.rTop.x - 1.0f), transform.position.y, 0);
		}
    }
}
