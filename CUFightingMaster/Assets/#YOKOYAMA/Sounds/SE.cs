/*---------------------------------------------------
 * 制作日：2018/11/03
 * 制作者：横山凌
 * 機能：SEの管理
 * 生成されてから何秒後にSEを消す（Unity側で消滅までの時間を設定する必要がある）
 * 更新日：2018/11/03
 ----------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
	float time;                         // 時間を計る
	public float timeMax;               // スプライト切り替えに要する時間

	void Update()
	{
		time += Time.deltaTime;
		if (time >= timeMax) { Destroy(gameObject); }
	}
}
