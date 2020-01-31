using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum test
{
	yokoyoko,
	aoao,
	kanakana,
	ore,
}

public class Test2 : CursolBase<test,GameObject>
{
	test test;
	public GameObject gameObject;
	public GameObject[] movePos;
    // Start is called before the first frame update
    void Start()
    {
		InitCursol(false, true, 0, "", 0.2f, test, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
		CursolUpdate(movePos);
	}
}
