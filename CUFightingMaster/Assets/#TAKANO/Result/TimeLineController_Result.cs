using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineController_Result : MonoBehaviour
{
	[SerializeField] //AnimationSetter animationSetter;
	[SerializeField] CameraMover cameraMover;


	//void PlayTimeline(GameObject _fighter)
	//{
	//	cameraMover.ClicoWin();
	//	animationSetter.ClicoWonAnimationSet(_fighter);
	//}

	//void PlayClicoLoseTimeline(GameObject _fighter)
	//{
	//	cameraMover.ClicoLose();
	//	animationSetter.ClicoLosingAnimationSet(_fighter);
	//}

	//void PlayObachanWonTimeline(GameObject _fighter)
	//{
	//	cameraMover.ObachanWin();
	//	animationSetter.ObachanWonAnimationSet(_fighter);
	//}

	//void PlayObachanLoseTimeline(GameObject _fighter)
	//{
	//	cameraMover.ObachanLose();
	//	animationSetter.ObachanLosingAnimationSet(_fighter);
	//}

	void JudgeTimeline( FighterStatus _fighterStatus)
	{
		int cnt = 0;
		foreach (FighterStatus fighterStatus in GameDataStrage.Instance.fighterStatuses)
		{
			switch (GameDataStrage.Instance.fighterStatuses[cnt].PlayerID)
			{
				case (int)FighterType.CLICO:
					break;
				case (int)FighterType.OBACHAN:
					break;
			}
		}
	}

	void a( FighterStatus _fighterStatus )
	{
		//if(_fighterStatus.)
	}
}
