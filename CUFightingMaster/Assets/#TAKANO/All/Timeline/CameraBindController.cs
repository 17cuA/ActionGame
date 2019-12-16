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
// 
//-------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using System.Linq;

public class CameraBindController : MonoBehaviour
{
	[SerializeField] PlayableDirector playableDirector;
	[SerializeField] string trackName = "Cinemachine Track";

	[SerializeField] public CinemachineBrain cinemachineBrain { set; private get; }

	public PlayableBinding playableBinding;

	private void Start()
	{
		playableBinding = playableDirector.playableAsset.outputs.First(c => c.streamName == trackName);
		if (playableDirector.playOnAwake == true)
			BindAnimation();
	}

	public void BindAnimation()
	{
		playableDirector.SetGenericBinding(playableBinding.sourceObject, cinemachineBrain);
	}

	public void PlayAnimaiton()
	{
		playableDirector.Play();
	}

}
