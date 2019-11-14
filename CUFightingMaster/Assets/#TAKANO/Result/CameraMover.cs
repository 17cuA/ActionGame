using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
	[SerializeField] GameObject timeline_ClicoWin;	// GurikoじゃなくてClicoだよ
	[SerializeField] GameObject timeline_ClicoLose;
	[SerializeField] GameObject timeline_ObaWin;
	[SerializeField] GameObject timeline_ObaLose;

	[SerializeField] GameObject camera_1PWin;
	[SerializeField] GameObject camera_1PLose;
	[SerializeField] GameObject camera_2PWin;
	[SerializeField] GameObject camera_2PLose;

	[SerializeField] CinemaController cinemaController;

	/// <summary>
	/// 1Pが勝利したときのカメラをセット
	/// </summary>
	public void OnePlayerWonCamera()
	{
		camera_1PWin.SetActive(true);
		camera_2PLose.SetActive(true);
	}
	/// <summary>
	/// 2Pが勝利したときのカメラをセット 
	/// </summary>
	public void TwoPlayerWonCamera()
	{
		camera_2PWin.SetActive(true);
		camera_1PLose.SetActive(true);
	}

	#region カメラワーク
	/// <summary>
	/// Clicoの勝利カメラワーク
	/// </summary>
	public void ClicoWin()
	{
		timeline_ClicoWin.SetActive(true);
	}
	/// <summary>
	/// Clicoの敗北カメラワーク
	/// </summary>
	public void ClicoLose()
	{
		timeline_ClicoLose.SetActive(true);
	}
	/// <summary>
	/// Obachanの勝利カメラワーク
	/// </summary>
	public void ObachanWin()
	{
		timeline_ObaWin.SetActive(true);
	}
	/// <summary>
	/// Obachanの敗北カメラワーク
	/// </summary>
	public void ObachanLose()
	{
		timeline_ClicoLose.SetActive(true);
	}
	#endregion
}
