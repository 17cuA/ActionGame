using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundGetDisplay : MonoBehaviour
{
	[SerializeField] DisplayObject[] displayObjects = new DisplayObject[3]; 
	[SerializeField] GameObject CHECK;

	public void Display()
	{
		for(int roundNum = 0; roundNum < 3; roundNum++)
		{
			if(GameDataStrage.Instance.roundResult[roundNum] == RoundResult.P1WIN)
			{
				var obj = Instantiate(CHECK, displayObjects[roundNum].P1win.transform.position,Quaternion.identity);
				obj.transform.parent = displayObjects[roundNum].P1win.transform;
			}
			else if(GameDataStrage.Instance.roundResult[roundNum] == RoundResult.P2WIN)
			{
				var obj = Instantiate(CHECK, displayObjects[roundNum].P2win.transform.position, Quaternion.identity);
				obj.transform.parent = displayObjects[roundNum].P2win.transform;
			}
		}
	}
}

[System.SerializableAttribute]
class DisplayObject
{
	public GameObject P1win;
	public GameObject P2win;
}
