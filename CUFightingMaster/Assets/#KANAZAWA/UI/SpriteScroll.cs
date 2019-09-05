using UnityEngine;

public class SpriteScroll : MonoBehaviour
{
	private float pos;
	private float scrollSpeed;

	void Start()
	{
		pos = 32.0f;
		scrollSpeed = 1.0f;
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(-scrollSpeed * Time.deltaTime, 0.0f, 0.0f);

		// 画面の下に移動したら
		if (transform.position.x < -pos)
		{
			// 背景を画面の上に移動させる
			// transform.position = オブジェクトの位置
			transform.position = new Vector3(pos, -2.25f, 0.0f);

		}
	}
}
