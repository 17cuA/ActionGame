//---------------------------------------
// 使用可能なディスプレイをアクティベート
//---------------------------------------
// 作成者:三沢
// 作成日:2019.07.25
//--------------------------------------
// 更新履歴
// 2019.07.25 作成
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
// はせぴーのデータを参考
//----------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSetUp
{
	public class DualCameraSetUp
	{
		public DualCameraSetUp()
		{
			// 使用可能なディスプレイを全てアクティベートする
			for (int i = 1; i < Display.displays.Length; ++i)
			{
				Display.displays[i].Activate();
			}
		}
	}
}