using System.Collections;
using System.Collections.Generic;
using UnityEngine;	
public class ResultAnimationChanger : MonoBehaviour
{
	//		    | 勝った(0) |    負けた(1)   | 勝ったループ(2) | 負けたループ(3) |
	// glico(0) | anime    |     anime      |    anime    |    anime       |
	// oba  (1) | anime    |     anime      |    anime    |    anime       |

	[SerializeField] private ResultAnimationPlayer resultAnimationPlayer_1;
	[SerializeField] private ResultAnimationPlayer resultAnimationPlayer_2;

	[SerializeField] private List<ResultAnimationList> animeList = new List<ResultAnimationList>();


	/// <summary>
	/// リザルト用アニメーションをセット_1
	/// </summary>
	/// <param name="_fighterNun">PlayerID</param>
	/// <param name="_victoryNum">1=win,2=lose</param>
	/// <returns></returns>
	public void SetAnimation_1( int _fighterNun , int _victoryNum)
	{
		resultAnimationPlayer_1.SetAnimetion(animeList[_fighterNun].animeList[_victoryNum]);
	}

	/// <summary>
	/// リザルト用アニメーションをセット_2
	/// </summary>
	/// <param name="_fighterNun">PlayerID</param>
	/// <param name="_victoryNum">1=win,2=lose</param>
	/// <returns></returns>
	public void SetAnimation_2(int _fighterNun, int _victoryNum)
	{
		resultAnimationPlayer_2.SetAnimetion(animeList[_fighterNun].animeList[_victoryNum]);
	}
}
