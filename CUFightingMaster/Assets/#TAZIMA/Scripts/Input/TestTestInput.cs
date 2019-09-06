using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text.RegularExpressions;

public class TestTestInput : MonoBehaviour
{   // Axisタイプ
	private enum AxisType
	{
		KeyOrMouseButton = 0,
		MouseMovement = 1,
		JoystickAxis = 2
	};

	// Axis情報
	private class InputAxis
	{
		public string name;
		public string descriptiveName;
		public string descriptiveNegativeName;
		public string negativeButton;
		public string positiveButton;
		public string altNegativeButton;
		public string altPositiveButton;
		public float gravity;
		public float dead;
		public float sensitivity;
		public bool snap = false;
		public bool invert = false;
		public AxisType type;
		public int axis;
		public int joyNum;
	};
	public int playerIndex; //プレイヤー番号
	public string player;   //Inputでプレイヤー毎の入力を識別するための文字列
	public string controllerName = ""; //使用するコントローラーの名前
	public int configIndex;
	public TempInputManagerInfo.InputAxis[] config;

	void Start()
    {

    }


    void Update()
    {
        
    }
	private void SetConfig()
	{

	}
}
