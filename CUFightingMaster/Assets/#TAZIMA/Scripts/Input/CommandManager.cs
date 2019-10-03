﻿//--------------------------------------------------------
//ファイル名：CommandManager.cs
//作成者　　：田嶋颯
//作成日　　：20190811
//
//正規表現を使ってコマンドを判別するスクリプト
//--------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CommandManager :MonoBehaviour
{
    /// <summary>
    /// CommandManagerの変数
    /// </summary>
    InputControl inputControl;                      //入力を管理するスクリプト
	public string inputData = "0";			        //入力した方向を保存する変数
    public string inputCommandName = "";            //入力したコマンドを保存する変数
    public bool isCommandInterval = false;          //コマンド発動中かどうか
    public bool isHitStop = false;                  //ヒットストップ中かどうか
	public List<AttackParameter> attackParameters;  //攻撃技のパラメータ


    /// <summary>
    /// 変数の初期化をする
    /// </summary>
    public void Init()
	{
        inputControl = gameObject.GetComponent<InputControl>();
        //コマンド確認用変数の初期化
        for (int i = 0; i < attackParameters.Count; i++) attackParameters[i].checkCommadStr = "";
	}


    /// <summary>
    /// 入力からコマンドデータを取得
    /// </summary>
    /// <param name="_data"></param>
    public void GetCommandData(string _data)
	{
        //入力
        CheckInputData(_data);
        //コマンドが成立しているかどうか
        InputCommandApplyCheck();
    }

    /// <summary>
    /// 入力された値を確認し、必要な処理を行う
    /// </summary>
    /// <param name="_data"></param>
	private void CheckInputData(string _data)
    {
        // if (_data != inputData)
        // {
        //     if (_data != "5")
        //     {
        //入力された値をそれぞれのコマンド確認用変数に移す
        for (int i = 0; i < attackParameters.Count; i++)
        {
            //前回の入力とデータが同じ、もしくはニュートラル状態（５）のときはスキップ,また最初の入力は常時格納
            if (_data != inputData || attackParameters[i].checkCommadStr == "")
            {
                if (_data != "5")
                {
                    var check = false;
                    while (!check)
                    {
                        //入力された値がコマンドの次の入力と同じであれば格納
                        if (_data == attackParameters[i].command[attackParameters[i].checkCommadStr.Length].ToString())
                        {
                            if (attackParameters[i].nowCoroutine != null)
                            {
                                StopCoroutine(attackParameters[i].nowCoroutine);
                            }
                            //コマンド確認用変数への最初の入力だった場合、ここに入力受付カウント処理を追加
                            attackParameters[i].nowCoroutine = StartCoroutine(CheckInputCommand(attackParameters[i]));
                            attackParameters[i].checkCommadStr += _data;
                            check = true;
                        }
                        //そうでない場合ミスを1カウントし、ミスが指定した数を超えたらリセットし要素数0番目の入力処理でなければもう一度入力を行う
                        else
                        {
							if (attackParameters[i].nowMissInput < attackParameters[i].ignoredMissInput)
							{
								attackParameters[i].nowMissInput++; 
							}
							else
							{
								attackParameters[i].nowMissInput = 0;
								if (attackParameters[i].checkCommadStr != _data)
								{
									if (attackParameters[i].checkCommadStr.Length != 0) attackParameters[i].checkCommadStr = "";
									else check = true;
								}
								else
								{
									check = true;
								}
							}
                        }
                    }
                }
                //         }
                // }
            }
        }
        inputData = _data;
    }

    /// <summary>
    /// 入力がコマンドに当てはまるか確認し、コマンド発動処理を行う
    /// </summary>
    private void InputCommandApplyCheck()
    {
        //コマンド発動後のインターバル中ではない時のみこの処理を行う
        // if (!isCommandInterval)
        // {
        var isShotCommand = false;
        for (int i = 0; attackParameters.Count > i; i++)
        {
            //文字数を正規表現で当てはまるかチェック
            //パターンが文字列の最後尾で終了する場合のみ抽出
            if (Regex.IsMatch(attackParameters[i].checkCommadStr, attackParameters[i].command + "$"))
            {
                //コマンドのフラグを立てる
                attackParameters[i].isShot = true;
                //コマンドを保存しておく時間
                StartCoroutine(CheckSaveCommand(attackParameters[i]));
                //これまでに入力されていたデータをリセット（空にする）
                attackParameters[i].checkCommadStr = "";
				//ミスのカウントを0にする
				attackParameters[i].nowMissInput = 0;
			}
            //同時押しのために猶予フレームが0の技は押されていたら有効に
            if (attackParameters[i].validShotFrame == 0 && attackParameters[i].command == inputData)
            {
                //コマンドのフラグを立てる
                attackParameters[i].isShot = true;
                //コマンドを保存しておく時間
                StartCoroutine(CheckSaveCommand(attackParameters[i]));
                //これまでに入力されていたデータをリセット（空にする）
                attackParameters[i].checkCommadStr = "";
				//ミスのカウントを0にする
				attackParameters[i].nowMissInput = 0;
			}
            if (attackParameters[i].isShot == true && Regex.IsMatch(attackParameters[i].shotTrigger, inputControl.atkButton) && isShotCommand == false)
            {
                //コマンド攻撃処理
                if (inputControl.atkButton != "")
                {
                    //コマンドを保存
                    inputCommandName = attackParameters[i].commandName + attackParameters[i].shotTrigger;
                    attackParameters[i].isShot = false;
                    isShotCommand = true;
                    //コマンドを受け付けない時間
                    StartCoroutine(CheckInterval(attackParameters[i]));
                }
                //ステップなどの攻撃がないコマンド処理
                else if (attackParameters[i].shotTrigger == "")
                {
                    //コマンドを保存
                    inputCommandName = attackParameters[i].commandName;

                    attackParameters[i].isShot = false;
                    isShotCommand = true;
                    //コマンドを受け付けない時間
                    StartCoroutine(CheckInterval(attackParameters[i]));
                }
            }
            // }
        }
    }

    /// <summary>
    /// コマンド入力を開始してからの時間(フレーム)を管理
    /// </summary>
    /// <param name="_attackParameter"></param>
    /// <returns></returns>
    public IEnumerator CheckInputCommand(AttackParameter _attackParameter)
    {
        var time = _attackParameter.validInputFrame / 60f;
        yield return new WaitForSeconds(time);
        //入力中のコマンドをリセットする
        _attackParameter.checkCommadStr = "";
    }

    /// <summary>
    /// コマンドを保存しておくフレームを管理
    /// </summary>
    /// <param name="_attackParameter"></param>
    /// <returns></returns>
    public IEnumerator CheckSaveCommand(AttackParameter _attackParameter)
    {
        var str = _attackParameter.commandName;
        var time = _attackParameter.validShotFrame / 60f;
        yield return new WaitForSeconds(time);
        //ヒットストップ中でない時
        while (isHitStop) yield return null;
        //フラグをオフ
        _attackParameter.isShot = false;
    }

    /// <summary>
    /// コマンド発動後のインターバル(フレーム)を管理
    /// </summary>
    /// <param name="_attackParameter"></param>
    /// <returns></returns>
    public IEnumerator CheckInterval(AttackParameter _attackParameter)
	{
        isCommandInterval = true;
        var time = _attackParameter.intervalFrame / 60f;
        yield return new WaitForSeconds(time);
        isCommandInterval = false;
    }
}
