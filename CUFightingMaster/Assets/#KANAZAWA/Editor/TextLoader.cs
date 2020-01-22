using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLoader : MonoBehaviour
{
    public string[,] textWords;

    private string[] textMessage;
    private int rowLength;
    private int columnLength;

    public void LoadText(string fileName)
    {
        var textAsset = new TextAsset();
        textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset == null)
        {
            Debug.LogError("テキストファイルが見つかりませんでした。");
            return;
        }

        // 取得したファイルを改行で分割
        textMessage = textAsset.text.Split('\n');
        // 改行した数から列数を取得
        rowLength = textMessage.Length;
        // 一行目のテキストをタブで分割し行数を取得
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
}
