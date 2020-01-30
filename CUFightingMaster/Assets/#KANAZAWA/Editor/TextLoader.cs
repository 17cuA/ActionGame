using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLoader : MonoBehaviour
{
    private string[,] textWords = null;
    private string[] textMessage = null;
    private int rowLength = 0;
    private int columnLength = 0;

	/// <summary>
	/// テキストファイルの読み込み
	/// </summary>
	/// <param name="_textAsset">テキストファイル</param>
	public void LoadText(TextAsset _textAsset)
    {
        if (_textAsset == null)
        {
            return;
        }
        // 取得したファイルを改行で分割
        textMessage = _textAsset.text.Split('\n');
        // 改行した数から列数を取得
        rowLength = textMessage.Length;
        // 一行目のテキストを[,]で分割し行数を取得
        columnLength = textMessage[0].Split(',').Length;
        textWords = new string[rowLength, columnLength];
        for(int i = 0; i < rowLength; i++)
        {
            string[] textLine = textMessage[i].Split(',');
            for (int j = 0; j < columnLength; j++)
            {
                textWords[i, j] = textLine[j];
            }
        }
    }

	/// <summary>
	/// テキストファイルを読み込み、配列にして返す
	/// </summary>
	/// <param name="_textAsset">テキストファイル</param>
	/// <returns>テキストの配列</returns>
	public string[,] GetText(TextAsset _textAsset)
    {
        LoadText(_textAsset);
        return textWords;
    }
}
