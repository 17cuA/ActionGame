using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugu_Move : MonoBehaviour
{
	public GameObject eff;
	public GameObject targetObject;

	private Vector3 vector3;
	// Start is called before the first frame update
	void Start()
    {
		//endPosition = new Vector3(transform.position.x, transform.position.y, -130.0f);
		//startPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		vector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z / 10.0f);
		if (Input.GetKeyDown(KeyCode.F10))
		{
			Instantiate(eff,vector3, Quaternion.identity);
			Destroy(targetObject);
		}
		if (Mathf.Approximately(Time.timeScale, 0f)) return;
		if (transform.position.z >= -130.0f)
		transform.position -= new Vector3(0.0f,0.0f,260.0f / 5400.0f);

	}
}
