using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
	enum InputDirection{
		up,
		down,
		right,
		left,
		none
	}

	InputDirection moveDirection;
	// Start is called before the first frame update
	void Start()
    {
		moveDirection = InputDirection.none;
	}

	// Update is called once per frame
	void Update()
    {
		MoveDirection();
    }
	void MoveDirection()
	{
		Direction();
		print(moveDirection);
		switch(moveDirection)
		{
			case InputDirection.up:
				transform.position += transform.forward * 1f * Time.deltaTime;
				break;
			case InputDirection.down:
				transform.position -= transform.forward * 1f * Time.deltaTime;
				break;
			case InputDirection.right:
				transform.position += transform.right * 1f * Time.deltaTime;
				break;
			case InputDirection.left:
				transform.position -= transform.right * 1f * Time.deltaTime;
				break;
			case InputDirection.none:
				break;
			default:
				Destroy(this.gameObject);
				break;
		}
		moveDirection = InputDirection.none;
	}
	void Direction()
	{
		if (Input.GetKey("up")) moveDirection = InputDirection.up;
		if (Input.GetKey("down")) moveDirection = InputDirection.down;
		if (Input.GetKey("right")) moveDirection = InputDirection.right;
		if (Input.GetKey("left")) moveDirection = InputDirection.left;
	}
}
