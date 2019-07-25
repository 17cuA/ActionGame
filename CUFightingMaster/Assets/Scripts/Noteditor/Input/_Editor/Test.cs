#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;
using UnityEditor;

public class Test : EditorWindow
{
	//変数
	public static InputManagerSetter inputManagerSetter = new InputManagerSetter();
	public string[] controller = new string[2];
    public TestInput[] testInput;
	[MenuItem("Window/InputSetter")]
	public static void Open()
	{
        var controllerNames = Input.GetJoystickNames();
		inputManagerSetter.SetInputManager();
    }
}
#endif