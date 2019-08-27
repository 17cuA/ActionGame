using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KARIUISample : MonoBehaviour
{
    [SerializeField] private PlayerNumber num = PlayerNumber.Player1;
    Text text;
    FighterCore core;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        core = GameManager.Instance.GetPlayFighterCore(num);
    }

    // Update is called once per frame
    void Update()
    {
        if(core.ComboCount > 1)
        {
            text.text = core.ComboCount.ToString()+"Combo!!!!!";
        }
        else
        {
            text.text = "";
        }
    }
}
