using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
	[SerializeField] UI_Judge ui_Judge;
	public void SetJudge(int p1,int p2)
	{
		ui_Judge.Judge(p1, p2);
	}

	 void Awake()
	{
		ui_Judge = gameObject.transform.Find("VictoryORdefeat").GetComponent<UI_Judge>();
	}
}
