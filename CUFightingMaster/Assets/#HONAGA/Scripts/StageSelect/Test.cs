using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private enum Edata
	{
		aoki,
		kanazawa,
		yokoyama,
	}
	Edata edata;
	Dictionary<Edata, string> keyValuePairs = new Dictionary<Edata, string>();

    // Start is called before the first frame update
    void Start()
    {
		keyValuePairs.Add(edata, "aoki");
		keyValuePairs.Add(edata+1, "kanazawa");
		keyValuePairs.Add(edata+2, "yokoyama");
	}

	// Update is called once per frame
	void Update()
    {
     for(int i = 0;i< System.Enum.GetNames(typeof(Edata)).Length;i++)
		{
			Debug.Log(keyValuePairs[edata+i]);
		}
    }
}
