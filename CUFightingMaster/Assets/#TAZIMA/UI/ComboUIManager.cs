using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUIManager : MonoBehaviour
{
	[SerializeField] private PlayerNumber num = PlayerNumber.Player1;
	Text text;
	FighterCore core;
	public int fadeOutFrame;
	private int currentRemainFrame;
	public GameObject comboObj;
	public Sprite comboSprite;
	public GameObject[] comboNumObj;
	public Sprite[] comboNumSprite;
	private Color initColor;

    void Start()
    {
		Init();
	}

    void Update()
    {
		ComboCounter();
	}
	private void Init()
	{
		text = GetComponent<Text>();
		core = GameManager.Instance.GetPlayFighterCore(num);
		initColor = new Color(255,255,255,255);
		ResetCombo();
	}
	void ResetCombo()
	{
		for (int i = 0; i < comboNumObj.Length; i++)
		{
			comboNumObj[i].SetActive(false);
			comboNumObj[i].GetComponent<Image>().color = initColor;
		}
		comboObj.SetActive(false);
		comboObj.GetComponent<Image>().color = initColor;
	}
	public void ComboCounter()
	{
		var comboCount = core.ComboCount;
		if (core.ComboCount > 1)
		{
			if (currentRemainFrame != fadeOutFrame) currentRemainFrame = fadeOutFrame;
			var length = comboCount.ToString("D2").Length;
			var isCombo = new bool[length];
			//スプライトを格納
			for (int i = 0; i < length; i++)
			{
				comboNumObj[i].GetComponent<Image>().sprite = comboNumSprite[comboCount / (int)Mathf.Pow(10, (i == 1) ? 0:1)];
				isCombo[i] = true;
			}
			//表示するかどうか判定
			for (int i = 0; i < length; i++)
			{
				if (comboNumObj[i].GetComponent<Image>().sprite == comboNumSprite[0]) isCombo[i] = false;
				else break;
			}
			//表示する
			for (int i = 0; i < length; i++)
			{
				Debug.Log(isCombo[i]);
				comboNumObj[i].SetActive(isCombo[i]);
			}
			comboObj.SetActive(true);
		}
		else
		{
			if (currentRemainFrame != 0)
			{
				currentRemainFrame--;
				float alpha = currentRemainFrame / fadeOutFrame;
				for (int i = 0; i < comboNumObj.Length; i++)
				{
					var numColor = comboNumObj[i].GetComponent<Image>().color;
					numColor.a = alpha;
					comboNumObj[i].GetComponent<Image>().color = numColor;
				}
				var color = comboObj.GetComponent<Image>().color;
				color.a = alpha;
				comboObj.GetComponent<Image>().color = color;
			}
			else ResetCombo();
		}
	}
}
