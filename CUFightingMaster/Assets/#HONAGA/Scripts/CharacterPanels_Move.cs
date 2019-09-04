using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanels_Move : MonoBehaviour
{
	Vector3 pos;
	public float speed;
	public float time;
	// Start is called before the first frame update
	void Start()
    {
		time = 0;
    }

    // Update is called once per frame
    void Update()
    {
		time = time+1;
		pos.x = speed - time*10f;
		transform.position += pos;
	}
}
