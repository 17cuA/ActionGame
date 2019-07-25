using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
	//プレイヤー番号
	public int playerNum;

	private const int maxChara = 6;
	public GameObject[] character = new GameObject[maxChara];               //キャラクターゲームオブジェクト
	public GameObject[] characterPanels = new GameObject[maxChara];    //キャラクターパネル
	public GameObject currentSellectCharacter = null;
	private GameObject currentSellectPanel;
	public int currentSelectNumber = 0;	//0が左
	public float selectDir = 0;						//カーソル入力方向
	public GameObject selectCursor;		//カーソル
	public float limitCursorFrame;				//カーソル移動
	public float moveCursorFrames = 0;	//カーソル移動後フレーム

	void Start()
	{
		CreateCharacter();
	}

	void Update()
	{
		//カーソル移動
		selectDir = Input.GetAxisRaw(string.Format("Player{0}_Horizontal", playerNum));
		moveCursorFrames += Time.deltaTime;
		if (selectDir != 0)
		{
			if (moveCursorFrames >= limitCursorFrame)
			{
				//左右移動（-1が左、1が右）
				SelectChara(selectDir);
				moveCursorFrames = 0;
			}
		}
		//決定(シーン移動)
		if (Input.GetButtonDown(string.Format("Player{0}_Attack1", playerNum)))
		{
			SceneManager.LoadScene("Battle");
		}
	}
	public void SelectChara(float _dir)
	{
		//カーソルの移動を判定
		if (_dir == -1)
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
