using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClicoCam : MonoBehaviour
{
	public CinemachineVirtualCamera Vcam0;
	public CinemachineVirtualCamera Vcam1;

	public int pMax = 50;
	public int pMin = 10;

	public ScriptableObject ClicoObject;
	public ScriptableObject ObachanObject;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.O))
		{
			StartCoroutine("Clico");
		}
    }


	IEnumerator Clico()
	{
		Vcam0.Priority = pMax;
		Vcam1.Priority = pMin;
		yield return new WaitForSeconds(0.3f);
		Vcam0.Priority = pMin;
		Vcam1.Priority = pMax;
	}
}
