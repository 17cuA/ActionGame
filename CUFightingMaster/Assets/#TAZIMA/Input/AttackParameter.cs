using UnityEngine;
[System.Serializable]
public class AttackParameter
{
    //この攻撃の名前
    public string attackName;
    //この攻撃の発動コマンド
    public string command;
	//この攻撃のコマンドの入力受付フレーム数
	public int validInputFrame;
	//この攻撃のコマンドの発動受付フレーム数
	public int validInvFrame;
	//この攻撃にかかる時間(sec)
	public int interval;
}
