using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Judge : MonoBehaviour
{
	public GameObject Win;
	public GameObject Lose;

	public void Judge(int winP1cnt, int winP2cnt)
	{
		if (winP1cnt > winP2cnt)
		{
			Debug.Log("aaa");
			Win.SetActive(true);
			Lose.SetActive(false);
		}
		if (winP2cnt > winP1cnt)
		{
			Debug.Log("bbbb");
			Win.SetActive(false);
			Lose.SetActive(true);
		}
	}
}
