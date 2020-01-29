//------
//UIを親子関係でグループに分けて左から右に動かす
//updateで呼び出す想定
//-----
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
	[SerializeField]GameObject group1Parent;
	[SerializeField]GameObject group2Parent;

	Vector3 group1CurrentPos;
	Vector3 group2CurrentPos;

	Vector3 group1InitPos;
	Vector3 group2InitPos;

	private void InitUIPositon()
	{
		//最初の位置を保存
		group1InitPos = group1Parent.transform.position;
		group2InitPos = group2Parent.transform.position;
		group1CurrentPos = group1InitPos;
		group1CurrentPos = group2InitPos;

		//画面外へUIを配置
		group1CurrentPos.x -= 1000;
		group2CurrentPos.x -= 1000;
		group1Parent.transform.position = group1CurrentPos;
		group2Parent.transform.position = group2CurrentPos;
	}

	private Vector3 LeftMover(Vector3 _currentPostion, float _speed)
	{
		_currentPostion.x += _speed;
		return _currentPostion;
	}

	public bool Group1Move()
	{
		if(group1Parent.transform.position.x <= group1InitPos.x)
		{
			group1Parent.transform.position = LeftMover(group1Parent.transform.position, 50.0f);
			return false;
		}
		return true;
	}

	public bool Group2Move()
	{
		if (group2Parent.transform.position.x <= group2InitPos.x)
		{
			group2Parent.transform.position = LeftMover(group2Parent.transform.position, 50.0f);
			return false;
		}
		return true;
	}

	public void Start()
	{
		InitUIPositon();
	}
}