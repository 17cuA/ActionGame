//---------------------------------------------------------------
// カメラのコントロール
//---------------------------------------------------------------
// 作成者:三沢・金沢
// 作成日:2019.05.17
//-----------------------------------------------------
// 仕様
// プレイヤー間の距離に応じてズームイン・ズームアウトする
// プレイヤーがジャンプした時カメラのY座標をプレイヤー間の平均(Y座標)に合わせる
//-----------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CUEngine.Pattern;

public class CameraController : SingletonMono<CameraController>
{
	#region 変数宣言
	[SerializeField]
	private GameObject Fighter1;	// プレイヤー1
	[SerializeField]
	private GameObject Fighter2;	// プレイヤー2

	[SerializeField]
	private float offsetY;			// カメラのY座標の基準値
	[SerializeField]
	private Vector3 cameraPos_Max;  // カメラの最大座標
	[SerializeField]
	private Vector3 cameraPos_Min;  // カメラの最小座標
	[SerializeField]
	private float speed_ZoomIn;				// カメラのズーム時の速度
	[SerializeField]
	private float speed_ZoomOut;			// カメラのズームアウト時の速度
	[SerializeField]
	private float distance_StartZoomIn;		// ズームインを開始するプレイヤー間の距離
	[SerializeField]
 	private float distance_StartZoomOut;	// ズームアウトを開始するプレイヤー間の距離

	private float distanceOfCamToPlayerPreviousFrame;   // 1フレーム前のカメラからキャラまでの距離
	private float distanceOfCamToPlayer;				// カメラからキャラまでの距離
	private float distanceOfPlayers_Start;				// ゲーム開始時のプレイヤー同士の距離
	private float distanceOfPLayers_Current;			// 現在のプレイヤー同士の距離
	private Vector3 PlayersCentorPos;					// プレイヤー同士のセンターを取得

	private float moveSpeed;		// カメラの移動速度
	private float stageWidth;		// ステージの横幅
	private Vector3 lBottom, rTop;	// 画面左下、右上の座標

	public GameObject Collider1;		// 画面端のオブジェクト
	public GameObject Collider2;
	public BoxCollider boxCollider1;    // 画面端のオブジェクトの当たり判定
	public BoxCollider boxCollider2;

    public CinemaController cinemaController;
	public static CameraController instance;

	public bool EvaFlag;

	#endregion

	#region 初期化
	private void Awake()
    {
        instance = GetComponent<CameraController>();
        boxCollider1 = Collider1.GetComponent<BoxCollider>();
        boxCollider2 = Collider2.GetComponent<BoxCollider>();
		EvaFlag = false;
    }


    void Start()
	{
		Fighter1 = GameManager.Instance.Player_one.gameObject;
		Fighter2 = GameManager.Instance.Player_two.gameObject;
		offsetY = transform.position.y;

		speed_ZoomIn = 5.0f;
		speed_ZoomOut = 20.0f;
		distance_StartZoomIn = 0.55f;
		distance_StartZoomOut = 0.6f;

		moveSpeed = 5.0f;
		stageWidth = 10.0f;
		cameraPos_Max = new Vector3(50.0f,0, -9.5f);	// ズームアウトの最大値
		cameraPos_Min = new Vector3(-50.0f,0,-13.0f);   // ズームインの最小値

		//Evangerionの時のカメラの設定
		if (SceneManager.GetActiveScene().name == "EVABattle") EvaFlag = true;
		if(EvaFlag)
		{
			cameraPos_Max = new Vector3(50.0f, 0, -18.0f);   // ズームアウトの最大値
			cameraPos_Min = new Vector3(-50.0f, 0, -22.5f); // ズームインの最小値
		}
		distanceOfPlayers_Start = 0.3f; // ゲーム開始時のプレイヤー同士の距離
    }
	#endregion

	#region Update
	void Update()
	{
		#region デバック用
		if (Input.GetKeyDown(KeyCode.N))
		{
			Shake(0.25f, 0.1f);
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			Shake(0.25f, 5f);
		}
		#endregion
	}

	void LateUpdate()
	{
        if (cinemaController != null)
        {
            if (cinemaController.isPlay) return;
        }
        CameraUpdate();
	}

	void CameraUpdate()
	{
		CameraPos();
		TargetPos();
		// カメラの移動・ズーム
		transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), 
										  new Vector3(PlayersCentorPos.x, transform.position.y, transform.position.z + Zoom()), 
										  Time.time * moveSpeed / distanceOfCamToPlayerPreviousFrame);
		// カメラの座標を制限(現在Y軸移動はここで行っている)
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraPos_Min.x, cameraPos_Max.x), PlayersCentorPos.y + offsetY, transform.position.z);
	}
	#endregion

	#region カメラの座標
	/// <summary>
	/// ターゲットの中心を求める
	/// </summary>
	void TargetPos()
	{
		PlayersCentorPos = (Fighter1.transform.position + Fighter2.transform.position) / 2;
		distanceOfPLayers_Current = Vector3.Distance(Camera.main.WorldToViewportPoint(Fighter1.transform.position), Camera.main.WorldToViewportPoint(Fighter2.transform.position));
		distanceOfCamToPlayerPreviousFrame = Vector3.Distance(transform.position, PlayersCentorPos);
	}

	/// <summary>
	/// カメラに関する座標を求める
	/// </summary>
	void CameraPos()
	{
		// カメラからプレイヤーまでの距離の計算
		distanceOfCamToPlayer = Vector3.Distance(transform.position, PlayersCentorPos);
		#region 座標を求める
		// カメラの左下座標を求める
		lBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceOfCamToPlayer));
		Collider1.transform.position = new Vector3(lBottom.x, transform.position.y, 0);
		// カメラの右上座標を求める
		rTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distanceOfCamToPlayer));
		Collider2.transform.position = new Vector3(rTop.x, transform.position.y, 0);
		#endregion
	}

	/// <summary>
	/// ステージ横幅からカメラのX座標の最大値・最小値を求める
	/// Update関数内で一度だけ呼び出す
	/// </summary>
	void GetCameraPos_X()
	{
		cameraPos_Max.x = stageWidth - (rTop.x - transform.position.x);
		cameraPos_Min.x = -stageWidth + (transform.position.x - lBottom.x);
	}
	#endregion

	#region ズーム処理
	/// <summary>
	/// カメラのズームイン、ズームアウトの処理
	/// </summary>
	/// <returns></returns>
	float Zoom()
	{
		// ズームの比率
		float zoomRatio = 0.0f;

		// ズームの比率を計算
		// カメラのZ座標が最大値より小さいかつプレイヤー間の距離が0.55未満の時
		if (transform.position.z < cameraPos_Max.z && distanceOfPLayers_Current < distance_StartZoomIn)
		{
			// プレイヤー間の距離によって速度を変更
			if(distanceOfPLayers_Current == 0||speed_ZoomIn==0)
			{
				return zoomRatio;
			}
			zoomRatio += distanceOfPlayers_Start / distanceOfPLayers_Current / speed_ZoomIn;
		}
		// カメラのZ座標が最小値より大きいかつプレイヤー間の距離が0.6より大きい時
		if (cameraPos_Min.z < transform.position.z && distanceOfPLayers_Current > distance_StartZoomOut)
		{
			if (distanceOfPLayers_Current == 0 || speed_ZoomIn == 0)
			{
				return zoomRatio;
			}
			// プレイヤー間の距離によって速度を変更
			zoomRatio -= distanceOfPLayers_Current / distanceOfPlayers_Start / speed_ZoomOut;
		}
		return zoomRatio;
	}
	#endregion

	#region カメラを揺らす処理
	/// <summary>
	/// カメラを揺らす値を受け取りコルーチンに投げる
	/// </summary>
	/// <param name="duration">揺れる期間</param>
	/// <param name="magnitude">揺れの大きさ</param>
	void Shake(float duration, float magnitude)
	{
		StartCoroutine(DoShake(duration, magnitude));
	}

	/// <summary>
	/// カメラを揺らす値を受け取り揺らす
	/// </summary>
	/// <param name="duration">揺れる期間</param>
	/// <param name="magnitude">揺れの大きさ</param>
	/// <returns></returns>
	IEnumerator DoShake(float duration, float magnitude)
	{
		// 経過時間
		var elapsed = 0f;

		while(elapsed < duration)
		{
			var x = transform.position.x + Random.Range(-0.1f, 0.1f) * magnitude;
			var y = transform.position.y + Random.Range(-0.1f, 0.1f) * magnitude;

			transform.position = new Vector3(x, y, transform.position.z);

			elapsed += Time.deltaTime;

			yield return null;
		}
	}
	#endregion
}
