//---------------------------------------
// シーン間で共有されるプレイヤーの情報
//---------------------------------------
// 作成者:高野
// 作成日:2019.08.22
//--------------------------------------
// 更新履歴
// 2019.08.22 作成
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
// 想定される使い方
// ・キャラセレシーンからゲームシーンのキャラクター情報
// ・ゲームシーンからリザルトシーンの勝敗情報
// ・など
//----------------------------------------
using UnityEngine;

public class ShareSceneVariable
{
	public static PlayerInfo P1_info;	//Player1の情報
	public static PlayerInfo P2_info;	//Player2の情報
	
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		P1_info.isWin = false;
		P2_info.isWin = false;
		P1_info.characterModel = null;
		P2_info.characterModel = null;
	}
}
