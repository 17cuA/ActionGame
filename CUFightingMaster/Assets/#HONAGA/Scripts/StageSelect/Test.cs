using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Edata
{
	aoki,
	kanazawa,
	yokoyama,
}
public class Test : asdf<Edata>
{

	Edata edata;
	Dictionary<Edata, string> keyValuePairs = new Dictionary<Edata, string>();

    // Start is called before the first frame update
    void Start()
    {
		keyValuePairs.Add(edata, "aoki");
		keyValuePairs.Add(edata+1, "kanazawa");
		keyValuePairs.Add(edata+2, "yokoyama");
	}

	// Update is called once per frame
	void Update()
    {
		aewsdf(Edata.yokoyama);
	}
}
public class asdf<T>:MonoBehaviour where T:System.Enum
{
	public T currentNumber;
	public void aewsdf(T sef)
	{
		// Enumの要素をArray型に変換する
		var tempEnumValueArray = System.Enum.GetValues(typeof(T));
		// T
		int currentNumberInt = Convert.ToInt32(sef);
		int valueNumberPosition = 0;
		// 今選択しているキャラの番号を一致させる
		foreach (int tempInt in tempEnumValueArray)
		{
			if (tempInt == currentNumberInt) break;
			valueNumberPosition++;
		}
		// tempEnumValueArrayの一致している要素名をストリングに変換し、Parseメソッドを使用してEnumに変換する
		currentNumber = (T)System.Enum.Parse(typeof(T), tempEnumValueArray.GetValue(valueNumberPosition).ToString());
		Debug.Log(currentNumber);
		//(moveobject as GameObject).transform.position = _movePosition.transform.position;
	}
}