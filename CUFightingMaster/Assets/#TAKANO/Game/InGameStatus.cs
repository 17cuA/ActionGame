using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStatus : MonoBehaviour
{
	private readonly int playerIndex = 2;
	public int winRound = 3;
	public int roundCount = 1;
	public string[] getRoundCount = {};	//そのラウンドをどう取得したかを保存(0:なし(多分使わない),1:KO,2:DoubleKO,3:TimeOver)し、lengthで取得数確認
}
