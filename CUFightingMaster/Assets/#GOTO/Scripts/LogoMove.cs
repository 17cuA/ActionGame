using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoMove : MonoBehaviour
{
	public float MoveTime;
	public float CheckTime;
	public float CheckTime1;
	public float CheckTime2;

	public float DontMoveTime;

	public int up1;
	public int up2;
	public int down1;
	public float MoveSpeed;

	public float rotationSpeed = 0;
	public float StartSecond;
	public int StartSecondMax;

	Rigidbody rigidbody;

	// Start is called before the first frame update
	void Start()
    {
		rigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
		Transform myTransform = this.transform;
		if (MoveTime < DontMoveTime)
		{
			MoveTime += Time.deltaTime;
			if (MoveTime <= CheckTime)
			{
				//１回目UP
				rigidbody.AddForce(0, MoveSpeed * up1, 0);
			}
			else if (MoveTime <= CheckTime1)
			{
				//何もなし
			}
			else if (MoveTime < CheckTime2 && MoveTime > CheckTime1)
			{
				//１回目Down
				rigidbody.AddForce(0, -MoveSpeed * down1, 0);
			}
			else
			{
				//二回目Do
				if (transform.position.y >= 0.7f) rigidbody.AddForce(0, MoveSpeed * up2, 0);
				else rigidbody.velocity = Vector3.zero;
			}
		}
		else 
		{
			StartSecond += Time.deltaTime;
			rigidbody.velocity = Vector3.zero;
			if(StartSecondMax < StartSecond) myTransform.Rotate(0f, rotationSpeed, 0f);
		}
	}
}
