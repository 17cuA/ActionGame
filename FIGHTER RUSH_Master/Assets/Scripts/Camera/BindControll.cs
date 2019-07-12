//---------------------------------------------------------------
// 登場シーンをプレイヤーごとに変更
//---------------------------------------------------------------
// 作成者:三沢
// 作成日:2019.07.12
//----------------------------------------------------
// 更新履歴
// 2019.07.12 登場シーンをプレイヤーごとに変更できるかテスト
//-----------------------------------------------------
// 仕様
// トラックを取得し、クリップを上書き
//-----------------------------------------------------
// MEMO
// 再生中のTimeLineは動的に更新できない
// よって再生前に上書きする必要がある
// 
//----------------------------------------------------
// 現在判明しているバグ
//----------------------------------------------------
// 参考サイト
// http://tsubakit1.hateblo.jp/entry/2017/09/22/224526
//----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Linq;
using UnityEngine.Timeline;

public class BindControll : MonoBehaviour
{
	//[SerializeField] PlayableDirector directer;
	//Coroutine coroutine;	// コルーチン
	public GameObject ob;	// 上書きするオブジェクト

	private void Start()
	{
		Test();
	}

	public void Test()
	{
		var directer = GetComponent<PlayableDirector>();
		// トラックを取得
		#region お小言
		// GetOutputTracks…タイムラインの全ての出力トラックリストを取得
		// .First…トラックリストの最初の要素を取得する
		// (c => c.name == "Player1")...trackの名前が"Player1"の時に取得
		#endregion
		var track = ((TimelineAsset)directer.playableAsset).GetOutputTracks().First(c => c.name == "Player1");
		// トラックから上書きしたいクリップを取得
		#region お小言
		// GetClips…トラックが所有するクリップのリスト
		// displayName…文字列を設定することで表示名を任意のものに変更できるようになる(?)
		#endregion
		var clip = (ControlPlayableAsset)track.GetClips().First(c => c.displayName == "Player1_1").asset;
		// Clip内に参照するためのキーを取得
		var exposeName = clip.sourceGameObject.exposedName;
		// 値を登録
		directer.SetReferenceValue(exposeName, ob);
	}

}
