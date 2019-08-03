using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
	//プレイヤー番号
	public int playerNum = 0;
	public string controllerName = ""; //使用するコントローラーの名前

	private const int maxChara = 6;
	public GameObject[] character = new GameObject[maxChara];               //キャラクターゲームオブジェクト
	public GameObject[] characterPanels = new GameObject[maxChara];    //キャラクターパネル
	public GameObject currentSellectCharacter = null;
	private GameObject currentSellectPanel;
	public Vector2 inputDir = Vector2.zero;//カーソル入力方向
	public int selectDir = 0;			//現在のカーソル位置
	public GameObject selectCursor;		//カーソル
	public float limitCursorFrame;		//カーソル移動
	public float moveCursorFrames = 0;	//カーソル移動後フレーム

	void Start()
	{
        //プレイヤー番号に対応した現在接続されているコントローラーを設定
        var controllerNames = Input.GetJoystickNames();
        if (playerNum < controllerNames.Length)
        {
            if (controllerNames[playerNum] != "")
            {
                controllerName = string.Format("{0}_", controllerNames[playerNum]);
            }
        }
	}

	void Update()
	{
        //カーソル移動
        inputDir.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNum));
		inputDir.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNum));
		moveCursorFrames += Time.deltaTime;
		if (inputDir != Vector2.zero)
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
			SceneManager.LoadScene("Battle");
		}
	}
	public void SelectChara(Vector2 _dir)
	{
		int x = selectDir % 3,y = selectDir / 3;
		//カーソルの移動を判定
		//x軸
		if (_dir.x == -1)
		{
			if (x > 0) x--;
			else x = 2;
		}
		else if(_dir.x == 1)
		{
			if (x < 2) x++;
			else x = 0;
		}
		//y軸
		if (_dir.y == -1)
		{
			Debug.Log("w");
			if (y > 0) y--;
			else y = 1;
		}
		else if (_dir.y == 1)
		{
			Debug.Log("s");
			if (y < 1) y ++;
			else y = 0;
		}
		Debug.Log(y);
		selectDir = x + (3 * y);
		selectCursor.transform.position = characterPanels[selectDir].transform.position;
	}
}
