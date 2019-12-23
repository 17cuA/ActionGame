//---------------------------------------------------------------
// TimeLineのトラック名からミュートを解除する
//---------------------------------------------------------------
// 作成者:高野
// 作成日:2019.12.13
//----------------------------------------------------
// 更新履歴
//-----------------------------------------------------
// 仕様

//-----------------------------------------------------
// MEMO
//
// 
//-------------------------------------------------
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using System.Linq;

public class TrackUnmute : MonoBehaviour
{
	[SerializeField] PlayableDirector playableDirector;
	[SerializeField] private IEnumerable<TrackAsset> trackAssets;
	[SerializeField] TrackAsset trackAsset;

	/// <summary>
	/// trackNameから、トラックをミュートする
	/// </summary>
	public void MuteTrack(string _trackName)
	{
		trackAsset = trackAssets.FirstOrDefault(x => x.name == _trackName);
		trackAsset.muted = true;
	}

	/// <summary>
	/// tracnNameから、トラックをミュートを解除する
	/// </summary>
	public void UnMuteTrack(string _trackName)
	{
		trackAsset = trackAssets.FirstOrDefault(x => x.name == _trackName);
		trackAsset.muted = false;
	}

	private void Start()
	{
		//タイムラインの全トラックを取得する
		trackAssets = (playableDirector.playableAsset as TimelineAsset).GetOutputTracks();
	}

	public void Update()
	{
		Debug.Log(trackAsset);
	}
}
