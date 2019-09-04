using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	Vector3 Vector3;
    // Start is called before the first frame update
    void Start()
    {
		Vector3 = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		Shake(5.0f, 0.1f);
		transform.position = Vector3;
	}
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

		while (elapsed < duration)
		{
			var x = transform.position.x + Random.Range(-0.1f, 0.1f) * magnitude;
			var y = transform.position.y + Random.Range(-0.1f, 0.1f) * magnitude;

			transform.position = new Vector3(x, y, transform.position.z);

			elapsed += Time.deltaTime;
			yield return null;
		}
	}
}
