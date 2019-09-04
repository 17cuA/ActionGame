using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUIManager : MonoBehaviour
{
	[SerializeField] private PlayerNumber num = PlayerNumber.Player1;
	Text text;
	FighterCore core;
	public Sprite combo;
	public Sprite[] comboNum;
	public GameObject[] comboNumSprite;
	public GameObject comboSprite;

    void Start()
    {
		text = GetComponent<Text>();
		core = GameManager.Instance.GetPlayFighterCore(num);
	}

    void Update()
    {
		if (core.ComboCount > 1)
		{
			for (int i = 0; i  < core.ComboCount.ToString().Length; i++)
			{
				
			}
			text.text = core.ComboCount.ToString() + "Combo!!!!!";
		}
		else
		{
			text.text = "";
		}
	}
}
