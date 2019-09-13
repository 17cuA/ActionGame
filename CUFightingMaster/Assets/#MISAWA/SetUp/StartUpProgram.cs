//---------------------------------------
// exe起動時のみに処理される
//---------------------------------------
// 作成者:三沢
// 作成日:2019.07.25
//--------------------------------------
// 更新履歴
// 2019.07.25 作成
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
// はせぴーのデータを参考
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpProgram : MonoBehaviour
{
	private static StartUpProgram instance = null;
	public GameObject obj;

	void Awake()
	{
		if (instance) { return; }
		Cursor.visible = false;
		Screen.SetResolution(1920, 1080, true);
		Application.targetFrameRate = 60;
		CameraSetUp.DualCameraSetUp cameraSetup = new CameraSetUp.DualCameraSetUp();
		instance = FindObjectOfType<StartUpProgram>();
		obj.GetComponent<FuncKeyManager>().Init();
	}
}
