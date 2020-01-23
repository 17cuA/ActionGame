using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
	public Sprite maskSprite;
	public CursolObject cursol;

	[SerializeField]
	private int currentStageNumber;
    // Start is called before the first frame update
    void Start()
    {
		currentStageNumber = 1;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
