using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CommandManager :MonoBehaviour
{
    TestInput testInput;                        //入力を管理するスクリプト

    //プロパティ========================================================
	public string inputData = "0";			    //入力した方向を保存する変数
    public string inputCommandName = "";        //入力したコマンドを保存する変数
    public bool isCommandInterval = false;      //コマンド発動中かどうか
    public bool isHitStop = false;              //ヒットストップ中かどうか

	//攻撃技のパラメータ
	public List<AttackParameter> attackParameters;

	//メソッド========================================================
	//変数の初期化をする
	public void Init()
	{
        testInput = gameObject.GetComponent<TestInput>();
        //コマンド確認用変数の初期化
        for (int i = 0; i < attackParameters.Count; i++) attackParameters[i].checkCommadStr = "";
	}

	//入力からコマンドデータを取得
	public void GetCommandData(string _data)
	{
        //入力
        CheckInputData(_data);
        //コマンドが成立しているかどうか
        InputCommandApplyCheck();
    }

	private void CheckInputData(string _data)
	{
		//前回の入力とデータが同じ、もしくはニュートラル状態（５）のときはスキップ
		if ((_data != inputData) && (_data != "5"))
		{
            //入力された値をそれぞれのコマンド確認用変数に移す
            for (int i = 0;i < attackParameters.Count;i++)
            {
                var check = false;
                while (!check)
                {
                    //入力された値がコマンドの次の入力と同じであれば格納
                    if (_data == attackParameters[i].command[attackParameters[i].checkCommadStr.Length].ToString())
                    {
                        //コマンド確認用変数への最初の入力だった場合、ここに入力受付カウント処理を追加
                        if (attackParameters[i].checkCommadStr.Length == 0) StartCoroutine(CheckInputCommand(attackParameters[i]));
                        attackParameters[i].checkCommadStr += _data;
                        check = true;
                    }
                    //そうでない場合リセットし、要素数0番目の入力処理でなければもう一度入力を行う
                    else
                    {
                        if (attackParameters[i].checkCommadStr.Length != 0) attackParameters[i].checkCommadStr = "";
                        else check = true;
                    }
                }
            }
			inputData = _data;
		}
	}

	//入力がコマンドに当てはまるかチェック
	private void InputCommandApplyCheck()
	{
		//コマンド発動後のインターバル中ではない時のみこの処理を行う
		if (!isCommandInterval)
		{
			for (int i = 0; attackParameters.Count > i; i++)
			{
				//文字数を正規表現で当てはまるかチェック
				//パターンが文字列の最後尾で終了する場合のみ抽出
				if (Regex.IsMatch(attackParameters[i].checkCommadStr, attackParameters[i].command + "$"))
				{
                    //コマンドのフラグを立てる
                    attackParameters[i].isShot = true;
					Debug.Log(attackParameters[i].attackName + "の入力が完了");
					//コマンドを保存しておく時間
					StartCoroutine(CheckSaveCommand(attackParameters[i]));
                    //これまでに入力されていたデータをリセット（空にする）
                    attackParameters[i].checkCommadStr = "";
				}
                //コマンドを発動する処理
                var isShotCommand = false;
                if (attackParameters[i].isShot == true && Regex.IsMatch(attackParameters[i].shotTrigger, testInput.atkButton) && isShotCommand == false)
                {
                    //コマンドを保存
                    inputCommandName = string.Format("{0}_Atk{1}", attackParameters[i].attackName,testInput.atkButton);
                    attackParameters[i].isShot = false;
                    //コマンドを受け付けない時間
                    StartCoroutine(CheckInterval(attackParameters[i]));
                }
            }
		}
	}
    //コマンド入力を開始してからの時間(フレーム)を管理
    public IEnumerator CheckInputCommand(AttackParameter _attackParameter)
    {
        var time = _attackParameter.validInputFrame / 60f;
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //入力中のコマンドをリセットする
        _attackParameter.checkCommadStr = "";
    }
    //コマンドを保存しておくフレームを管理
    public IEnumerator CheckSaveCommand(AttackParameter _attackParameter)
    {
        Debug.Log("コマンド消去待機");
        var str = _attackParameter.attackName;
        var time = _attackParameter.validShotFrame / 60f;
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //ヒットストップ中でない時
        yield return (isHitStop == false);
        //フラグをオフ
        Debug.Log("コマンド消去");
    }
    //コマンド発動後のインターバル(フレーム)を管理
    public IEnumerator CheckInterval(AttackParameter _attackParameter)
	{
        isCommandInterval = true;
        Debug.Log("インターバル開始");
        var time = _attackParameter.intervalFrame / 60f;
        yield return new WaitForSeconds(time);
        isCommandInterval = false;
        Debug.Log("インターバル終了");
    }
}
