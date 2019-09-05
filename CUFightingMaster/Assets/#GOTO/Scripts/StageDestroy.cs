using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDestroy : MonoBehaviour
{
	public GameObject Effects;
	Vector3 EfePos;
    // Start is called before the first frame update
    void Start()
    {
		EfePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F11) && this.gameObject.active)
		{
			Instantiate(Effects, EfePos, Quaternion.identity);
			Destroy(this.gameObject);
		}
    }
}
