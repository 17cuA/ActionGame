using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create Test")]
public class Test_Scriptable_List : ScriptableObject
{
	public List<Test_Scriptable> TestStatusList = new List<Test_Scriptable>();
}

// データを保持するため、設定
[System.Serializable]
public class Test_Scriptable
{
	public string Name = "名前を変更";
	public int HP = 100, SP = 50, Atk = 5, Def = 15, Spd = 99, Exp = 58;
	public bool IsBoss = false;
}