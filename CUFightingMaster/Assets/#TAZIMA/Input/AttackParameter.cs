using UnityEngine;
[System.Serializable]
public class AttackParameter
{
    //この攻撃の名前
    public string commandName;
    //この攻撃の発動コマンド
    public string command;
    //このコマンドを発動できる攻撃ボタン(弱:1 中:2 強:3)
    public string shotTrigger;
	//この攻撃のコマンドの入力受付フレーム数
	public int validInputFrame;
	//この攻撃のコマンドの発動受付フレーム数
	public int validShotFrame;
	//再度コマンドを入力できるようになるまでのフレーム数
	public int intervalFrame;
    //発動できる状態かどうか
    public bool isShot;
    //コマンドが入力されているか確認するための変数
    public string checkCommadStr;

    public Coroutine nowCoroutine = null;
}
