using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Judge : MonoBehaviour
{
	[SerializeField] GameObject Win;
	[SerializeField] GameObject Lose;

	void Awake()
	{
		Win = transform.Find("WIN").gameObject;
		Lose = transform.Find("LOSE").gameObject;
	}

	public void Judge(int winP1cnt, int winP2cnt)
	{
		if (winP1cnt > winP2cnt)
		{
			Win.SetActive(true);
			Lose.SetActive(false);
		}
		if (winP2cnt > winP1cnt)
		{
			Win.SetActive(false);
			Lose.SetActive(true);
		}
	}
}
