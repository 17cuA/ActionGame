//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System
//	;

//public class TestMan : MonoBehaviour
//{
//	public enum EStatus
//	{
//		stop,
//		walking,
//	}
//	public GameObject[] movePoint;
//	public AnimationClip stopAnimation;
//	public AnimationClip walkingAnimation;

//	public Dictionary<EStatus, TestBase> animationStatus = new Dictionary<EStatus, TestBase>();

//	public TestBase nowStatus;

//	public void Awake()
//	{
//		animationStatus.Add(EStatus.stop, new Stop(stopAnimation));
//		animationStatus.Add(EStatus.walking, new Walking(walkingAnimation));
//	}

//	public void Update()
//	{
//		switch (Random())
//		{
//			case (int)EStatus.stop:
//				ChangeStatus(EStatus.stop);
//				break;

//			case (int)EStatus.walking:
//				ChangeStatus(EStatus.walking);
//				break;
//		}

//	}
//	public int Random()
//	{
//		//ここに何か書く
//	}
//	public void ChangeStatus(EStatus _eStatus)
//	{
//		nowStatus = animationStatus[_eStatus];
//	}
//}

//public class Stop : TestBase
//{
//	public Stop(AnimationClip _animationClip) : base(_animationClip)
//	{
//	}
//	public override void TestMethod()
//	{
//		// 止まってる時の処理
//	}
//}

//public class Walking : TestBase
//{
//	public Walking(AnimationClip _animationClip) : base(_animationClip) { }

//	public override void TestMethod()
//	{

//	}
//}

//public abstract class TestBase
//{
//	public AnimationClip animationClip;

//	public TestBase(AnimationClip _animationClip)
//	{
//		animationClip = _animationClip;
//	}

//	public abstract void TestMethod();
//}