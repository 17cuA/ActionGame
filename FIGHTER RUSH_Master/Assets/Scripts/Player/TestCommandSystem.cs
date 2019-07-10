using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class TestCommandSystem :MonoBehaviour
{
	//プロパティ========================================================
	public string inputCommandData = "";	    //文字列、コマンドデータ
	public int MaxCommandDataLength = 20;		//文字列の長さ
	public string inputData = "0";			    //入力した方向を保存する変数
    public string commandName = null;           //コマンド名を保存しておく変数
    public bool chechInvCommand = false;        //コマンド発動中かどうか

	//攻撃技のパラメータ
	public List<AttackParameter> attackParameters;

	//メソッド========================================================
	//変数の初期化をする
	public void Init()
	{
		//入力用文字列の長さ設定
		inputCommandData = inputCommandData.PadLeft(MaxCommandDataLength);
	}

	//入力からコマンドデータを取得---------------------
	public void GetCommandData(string data)
	{
        //キーボードからの入力
        CheckInputData(data);
		if (inputCommandData.Length > MaxCommandDataLength)
		{
			inputCommandData = inputCommandData.Remove(0, 1);
		}
        //コマンドが成立しているかどうか
        InputCommandApplyCheck(attackParameters);
    }
	private void CheckInputData(string data)
	{
		//前回の入力とデータが同じ、もしくはニュートラル状態（５）のときはスキップ
		if ((data != inputData) && (data != "5"))
		{
			inputCommandData += data;
			inputData = data;
		}
	}

	//入力がコマンドに当てはまるかチェック--------------------------------------------------------------------------------------
	private void InputCommandApplyCheck(List<AttackParameter> attackParameter)
	{
        //コマンド発動中ではない時のみこの処理を行う
        if (!chechInvCommand)
        {
            for (int i = 0; attackParameters.Count > i; i++)
            {
                //文字数を正規表現で当てはまるかチェック
                //$でパターンが文字列の最後尾で終了する場合のみ抽出
                if (Regex.IsMatch(inputCommandData, attackParameter[i].command + "$"))
                {
                    //技を保存
                    Debug.Log(attackParameter[i].attackName + "を保存しました");
                    commandName = attackParameter[i].attackName;
                    //コマンドを受け付けない時間
                    StartCoroutine(CheckInterval(attackParameter[i]));
                    //コマンドを保存しておく時間
                    StartCoroutine(CheckSaveCommand(attackParameter[i]));
                    //これまでに入力されていたデータをリセット（空にする）
                    inputCommandData = "";
                }
            }
        }
    }
    //コマンド発動後のインターバルを管理
	public IEnumerator CheckInterval(AttackParameter attackParameter)
	{
        chechInvCommand = true;
        Debug.Log("インターバル開始");
        var time = attackParameter.interval / 60;
        yield return new WaitForSeconds(time);
        chechInvCommand = false;
        Debug.Log("インターバル終了");
    }
    //コマンドを保存しておくフレームを管理
    public IEnumerator CheckSaveCommand(AttackParameter attackParameter)
    {
        Debug.Log("コマンド消去待機");
        var str = attackParameter.attackName;
        var time = attackParameter.validInvFrame / 60;
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //現在保存されているコマンドがこの関数開始時と同じ場合のみ中身をリセット
        if (commandName == str)
        {
            commandName = null;
            Debug.Log("コマンド消去");
        }
    }
}
