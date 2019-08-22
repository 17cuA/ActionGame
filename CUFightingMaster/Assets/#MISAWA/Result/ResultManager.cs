//---------------------------------------
// Result画面
//---------------------------------------
// 作成者:三沢
// 作成日:2019.08.14
//--------------------------------------
// 更新履歴
// 2019.08.14 作成
//--------------------------------------
// 仕様 
// InGameManagerとCharacterSelectManagerから
// 勝敗判定と選ばれていたキャラクターを取得
//----------------------------------------
// MEMO 
//----------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
	public Canvas canvas_1;	// ディスプレイ1
	[SerializeField] private GameObject Player1;	// 選ばれていたプレイヤー1

	public Canvas canvas_2;	// ディスプレイ2
	[SerializeField] private GameObject Player2;	// 選ばれていたプレイヤー2

	// 参照先
	[SerializeField] private InGameManager inGameManager;
	[SerializeField] private CharacterSelectManager characterSelectManager;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
		{
			SceneManager.LoadScene("JECLogo");
		}
		Debug.Log(ShareSceneVariable.P1_info.isWin);
    }
}
