using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoMove : UIManager_Title
{
	private float MoveTime;
	private float rate;
	private Vector3 secondPos;
	private Vector3 lastPos;
	private bool checkPoint_1;
	private bool checkPoint_2;

	public float CheckTime_1;
	public float CheckTime_2;

	// Start is called before the first frame update
	void Start()
    {
		secondPos = new Vector3(transform.position.x, 15.0f, 0.0f);
		lastPos = new Vector3(transform.position.x, 0.5f, 0.0f);
		checkPoint_1 = false;
		checkPoint_2 = false;
		MoveTime = 0.0f;
	}

	// Update is called once per frame
	void Update()
    {
		MoveTime += Time.deltaTime;
		if (checkPoint_1 == false)
		{
			rate = MoveTime / CheckTime_1 / 100.0f;
			//１回目UP
			transform.position = Vector3.Lerp(transform.position, secondPos, rate);		
			if (MoveTime >= CheckTime_1) checkPoint_1 = true;
		}
		else if (checkPoint_1 == true)
		{
			rate = MoveTime / CheckTime_2 / 100.0f;
			//１回目Down
			transform.position = Vector3.Lerp(transform.position, lastPos, rate);
			if (MoveTime >= CheckTime_2) checkPoint_2 = true;
		}
		if (checkPoint_2 == true)
		{
			//ScaleUp(gameObject, 3.0f);
		}
	}
}
