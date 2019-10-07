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
// 一応完成。キャラセレが完成次第、確認し修正 
//----------------------------------------------------
// 現在判明しているバグ
//----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using System.Linq;

public class BindController : MonoBehaviour
{
	[SerializeField] PlayableDirector director;
	[SerializeField] int PlayerNum;
    public int PlayerNum_
    {
        set { PlayerNum = value; }
    }

    public CinemachineBrain cinemachineBrain;	// カメラごとにcinemachineBrainセット
	public string trackName;					// トラック名の指名

	private void Start()
	{
		// シーン上のCinemachineBrainをマネージャーから取得
		cinemachineBrain = ProductionCameraManager.Instance.cinemachine[PlayerNum];
		// 自身のPlaybleDirectorをセット
		director = gameObject.GetComponent<PlayableDirector>();
		// トラック名の指定がなかったら
		if (trackName == "")
		{
			trackName = "Cinemachine Track";        //デフォルトを取得
		}
		// 自身のPlaybleDirectorのトラックを取得
		var binding = director.playableAsset.outputs.First(c => c.streamName == trackName);
		// 自身のTrackに指定したカメラをセット
		director.SetGenericBinding(binding.sourceObject, cinemachineBrain);
	}
}


