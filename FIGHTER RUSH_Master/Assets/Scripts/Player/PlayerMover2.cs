//---------------------------------------------------------------
//  プレイヤーを動かす
//---------------------------------------------------------------
//  作成者:高野
//  作成日:2019.05.04
//----------------------------------------------------
//  更新履歴
//  2019.05.04  :  作成
//-----------------------------------------------------
//  仕様
//  プレイヤーを動かす
//-----------------------------------------------------
//  MEMO
//  
//----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover2 : MonoBehaviour
{
	[SerializeField]
    private float speed = 8f;		
    float moveX = 0f;				
	float jumpPower = 0f;			
	Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		transform.position = new Vector3(2.5f, 0, 0);
	}

	void Update()
	{
		// 登場シーンが終わるまで動かせないようにする
		if (UIManager_Game.instance.call_Once)
		{
			#region 移動
			if (Input.GetKey(KeyCode.G))
			{
				moveX = -speed;
			}

			else if (Input.GetKey(KeyCode.H))
			{
				moveX = speed;
			}

			else
			{
				moveX = 0;
			}
			#endregion
			#region ジャンプ処理
			if (Input.GetKeyDown(KeyCode.Space))
			{
				jumpPower = 3f;
			}
			else
			{
				jumpPower = 0f;
			}
			#endregion
		}
		transform.Translate(moveX / 30, jumpPower, 0);
	}

	void LateUpdate()
	{
		// 登場シーンが終わるまで動かせないようにする
		if (UIManager_Game.instance.call_Once)
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, CameraControll.instance.lBottom.x + 1.0f, CameraControll.instance.rTop.x - 1.0f), transform.position.y, 0);
		}
	}
}
