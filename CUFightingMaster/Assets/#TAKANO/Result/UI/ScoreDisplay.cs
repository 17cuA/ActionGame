using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
	[SerializeField] public int num {  get; set; }
	[SerializeField] Sprite[] numbersSprite = new Sprite[10];

	[SerializeField] GameObject createPos; 
	[SerializeField] GameObject number;

	private string score;
	private Vector3 x;

	private void DisplayNumber(int _score)
	{
		score = _score.ToString();
		for (int i = 0; score.Length > i; i++)
		{
			var obj = Instantiate(number, createPos.transform);
			
			obj.transform.transform.position += x;
			x.x += 55;
			int index = 0;
			index = CharExt.ToInt(score[i]);

			obj.GetComponent<ScoreNumber>().SetImage(numbersSprite[index]);
		}
	}

	public void Start()
	{
		DisplayNumber(num); 
	}

	//public void Update()
	//{
	//	DisplayNumber(score);
	//}
}

