using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
	public int currentSelectNumber = 0;     //0が左

	private const int maxChara = 6;
	public GameObject[] character = new GameObject[maxChara];               //キャラクターゲームオブジェクト
	public GameObject[] characterPanels = new GameObject[maxChara];    //キャラクターパネル
	public GameObject currentSellectCharacter = null;
	private GameObject currentSellectPanel;
	public GameObject selectCursor;     //カーソル

	void Start()
	{
		CreateCharacter();
	}

	void Update()
	{
		//カーソル左移動
		if (Input.GetKeyDown("a"))
		{
			SelectChara(0);
		}
		//カーソル右移動
		else if (Input.GetKeyDown("s"))
		{
			SelectChara(1);
		}
		//決定(シーン移動)
		else if (Input.GetKeyDown("b"))
		{
			SceneManager.LoadScene("Battle");
		}
		CreateCharacter();
	}
	public void SelectChara(int _dir)
	{
		//カーソルの移動を判定
		if (_dir == 0)
		{
			if (currentSelectNumber > 0) currentSelectNumber--;
			else currentSelectNumber = 5;
		}
		else
		{
			if (currentSelectNumber < 5) currentSelectNumber++;
			else currentSelectNumber = 0;
		}
		selectCursor.transform.position = characterPanels[currentSelectNumber].transform.position;
		CreateCharacter();
	}

	//表示するキャラクターを生成
	public void CreateCharacter()
	{
		if (currentSellectCharacter != null) Destroy(currentSellectCharacter);
		currentSellectCharacter = Instantiate(character[currentSelectNumber]);
	}
}
