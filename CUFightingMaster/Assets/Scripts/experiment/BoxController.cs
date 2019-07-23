using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
	public EnemyData enemyData;
    void Start()
    {
		ShowScriptableObjData();
    }

    void Update()
    {
        
    }
	void ShowScriptableObjData()
	{
		Debug.Log("私の名前は" + enemyData.enemyName +
							", 最大HPは" + enemyData.maxHp +
							", 攻撃力は" + enemyData.atk +
							", 防御力は" + enemyData.def +
							", 経験値は" + enemyData.exp +
							", ゴールドは" + enemyData.gold + "です。");
	}
}
