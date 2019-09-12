using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
	GameObject[] tagObjects;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		CheckTag();
    }
	void  CheckTag()
	{
		//tagObjects.Lengthはオブジェクトの数であれば（tagObjects 。長さ== 0 ）{ デバッグ。Log （tagname + "タグがついたオブジェクトはありません" ）; } } }
		tagObjects = GameObject.FindGameObjectsWithTag("Effects") ;
		Debug.Log(tagObjects.Length);
	}
}
