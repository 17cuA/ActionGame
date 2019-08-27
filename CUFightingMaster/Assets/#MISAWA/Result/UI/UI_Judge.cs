using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Judge : MonoBehaviour
{
	public GameObject Win;
	public GameObject Lose;

	void Judge()
	{
		if (ShareSceneVariable.P1_info.isWin)
		{
			Win.SetActive(true);
			Lose.SetActive(false);
		}
		if (ShareSceneVariable.P2_info.isWin)
		{
			Win.SetActive(false);
			Lose.SetActive(true);
		}
	}
}
