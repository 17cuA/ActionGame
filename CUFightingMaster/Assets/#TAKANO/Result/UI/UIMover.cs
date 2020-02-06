//------
//UIを親子関係でグループに分けて左から右に動かす
//updateで呼び出す想定
//-----
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
	[SerializeField] GameObject[] gameObjects = new GameObject[4];

	[SerializeField]Vector3[] initPos = new Vector3[4];
	[SerializeField] Vector3[] parentPos = new Vector3[4];
	[SerializeField] Vector3[] currentPos = new Vector3[4];

	private void InitUIPositon()
	{
		for(int i = 0; i < 4; i++)
		{
			//最初の位置を保存
			initPos[i] = gameObjects[i].transform.position;
			currentPos[i] = initPos[i];

			//画面外へUIを配置
			currentPos[i].x -= 1000;
			gameObjects[i].transform.position = currentPos[i];

		}
	}

	private Vector3 LeftMover(Vector3 _currentPostion, float _speed)
	{
		_currentPostion.x += _speed;
		return _currentPostion;
	}

	public void Move( int i)
	{
		RectTransform rect = RectTransformEx.ToRectTransform(gameObjects[i].transform);

		if (gameObjects[i].transform.position.x <= initPos[i].x)
		{
			gameObjects[i].transform.position = LeftMover(gameObjects[i].transform.position, 50.0f);
			//return false;
		}
		//return true;
	}

	public void Start()
	{
		InitUIPositon();
	}
}