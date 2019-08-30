using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cursor_CharacterSelect : MonoBehaviour
{
	public Characterselect_Timer timer;

	public Sprite[] images = new Sprite[2];

	public int playerNum = 0;

	public string controllerName = "";

	private const int maxChara = 4;
	public GameObject[] character = new GameObject[maxChara];         //キャラクターゲームオブジェクト
	public GameObject[] characterPanels = new GameObject[maxChara];   //キャラクターパネル
	public Sprite[] characterName = new Sprite[maxChara];            //キャラクターの名前画像

	public GameObject currentSellectCharacter = null;  //現在選択しているキャラクター
	public Image currentSelectCharacter;           //現在選択しているキャラクターの名前

	public float limitCursorFrame;      //カーソル移動
	public float moveCursorFrames = 0;  //カーソル移動後フレーム

	public Vector2 inputDir = Vector2.zero;
	public int selectDir = 0;                           //現在のカーソル位置

	[SerializeField]
	private bool determining_decision;
	public bool Determining_decision
	{
		get { return determining_decision; }
	}
	void Start()
	{
		determining_decision = false;
		var controllerNames = Input.GetJoystickNames();
		if (playerNum < controllerNames.Length)
		{
			if (controllerNames[playerNum] != "")
			{
				controllerName = string.Format("{0}_", controllerNames[playerNum]);
			}
		}
	}
	// Update is called once per frame
	void Update()
	{
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;
		//カーソル移動
		inputDir.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNum));
		inputDir.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNum));
		moveCursorFrames += Time.deltaTime;
		if (inputDir != Vector2.zero && this.determining_decision == false)
		{
			if (moveCursorFrames >= limitCursorFrame)
			{
				//左右移動（-1が左、1が右）
				SelectChara(inputDir);
				moveCursorFrames = 0;
			}
		}
		//決定(シーン移動)
		if (Input.GetButtonDown(string.Format("{0}Player{1}_Attack1", controllerName, playerNum)))
		{
			if (determining_decision == false)
				determining_decision = true;
			else
				determining_decision = false;
			//飯塚追加------------------------------------------ -
			Sound.LoadSe("Menu_Cancel", "Se_menu_cancel");
			Sound.PlaySe("Menu_Cancel", 1, 0.8f);
			//---------------------------------------------------
		}
		transform.position = new Vector3(characterPanels[selectDir].transform.position.x, transform.position.y, transform.position.z);
		if (selectDir > 1/*currentSellectCharacter.name == "ObaChan" || currentSellectCharacter.name == "ObaChan(1)"*/)
		{
			gameObject.GetComponent<Image>().sprite = images[1];
		}
		else
		{
			gameObject.GetComponent<Image>().sprite = images[0];
		}
		if(timer.IsPlayCountDown == false)
		{
			determining_decision = true;
		}

	}
	public void SelectChara(Vector2 _dir)
	{
		int x = selectDir;
		//カーソルの移動を判定
		//x軸
		if (_dir.x == -1)
		{
			// 飯塚追加-------------------------------------------
			Sound.LoadSe("Menu_MoveCursor", "Se_menu_moveCursor");
			Sound.PlaySe("Menu_MoveCursor", 1, 0.2f);
			// ---------------------------------------------------
			if (x > 0)
				x--;
			else
				x = 3;
		}
		else if (_dir.x == 1)
		{
			// 飯塚追加-------------------------------------------
			Sound.LoadSe("Menu_MoveCursor", "Se_menu_moveCursor");
			Sound.PlaySe("Menu_MoveCursor", 1, 0.2f);
			// ---------------------------------------------------
			if (x < 3)
				x++;
			else
				x = 0;
		}
		selectDir = x;
	}

}
