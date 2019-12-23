//---------------------------------------------------------------
// TimeLineのカメラトラックの操作
//---------------------------------------------------------------
// 作成者:高野
// 作成日:2019.12.02
//----------------------------------------------------
// 更新履歴
//-----------------------------------------------------
// 仕様
// インスペクタでplayableDirectorを参照してください
// PlayOnAwake = true
//-----------------------------------------------------
// MEMO
//
//------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using System.Linq;

public class CameraBindController : MonoBehaviour
{
	[SerializeField] PlayableDirector playableDirector;
	public string trackName = "Cinemachine Track";

	[SerializeField] public CinemachineBrain cinemachineBrain { set; private get; }

	public PlayableBinding playableBinding;
	
	/// <summary>
	/// trackNameからトラックを取得
	/// </summary>
	public void BindAnimation()
	{
		playableBinding = playableDirector.playableAsset.outputs.First(c => c.streamName == trackName);

		playableDirector.SetGenericBinding(playableBinding.sourceObject, cinemachineBrain);
	}

	/// <summary>
	/// 
	/// </summary>
	public void PlayAnimaiton()
	{
		playableDirector.Play();
	}
}
