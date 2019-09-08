using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fighting/Setting")]
public class GameSettings : ScriptableObject
{
    [SerializeField,Header("押し合いの値")]
    private float pushAmount = 0.5f;
    [SerializeField, Header("ノックバックフレーム")]
    private int knock_Back_Count = 6;
    [SerializeField,Header("コマンドの入力猶予フレーム")]
    private int validInputFrame = 15;
	[SerializeField, Header("ダウン時間")]
	private int downFrame = 30;
	[SerializeField, Header("地上起き上がりまでの時間")]
	private int wakeUpFrame = 15;
	[SerializeField, Header("KOヒットストップフレーム")]
	private int koHitStopFrame = 60;
    [SerializeField, Header("コンボ重力加算")]
    private float comboGravity = 0.1f;
    [SerializeField, Header("何回バウンドできるか")]
    private int boundCount = 1;


    public float PushAmount
    {
        get { return pushAmount; }
    }
    public int Knock_Back_Count
    {
        get { return knock_Back_Count; }
    }
    public int ValidInputFrame
    {
        get { return validInputFrame; }
    }
	public int DownFrame
	{
		get { return downFrame; }
	}
	public int WakeUpFrame
	{
		get { return wakeUpFrame; }
	}

	public int KOHitStopFrame
	{
		get { return koHitStopFrame; }
	}
	public float ComboGravity
	{
        get { return comboGravity; }
    }
	public int BoundCount
	{
        get { return boundCount; }
    }
}
