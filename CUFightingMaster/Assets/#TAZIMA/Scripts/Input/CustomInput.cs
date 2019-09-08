using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
namespace CustomInputClass
{
    public class CustomInput
    {
        public int playerIndex; //プレイヤー番号
        public int controllerIndex;
	    public int configIndex;
        public TempInputManagerInfo.InputAxis[] config;
        public void SetConfig(int _playerIndex,int _cotrollerIndex,int _joyNum)
        {
            var axes = new TempInputManagerInfo.InputAxis[TempInputManagerInfo.Config.Length];
            for (int i = 0;i < TempInputManagerInfo.Config.Length;i++)
            {
                var axis = new TempInputManagerInfo.InputAxis();
                axis.name = TempInputManagerInfo.Config[i].name.Replace("{0}",_playerIndex.ToString());
			    axis.descriptiveName = TempInputManagerInfo.Config[i].descriptiveName;
			    axis.descriptiveNegativeName = TempInputManagerInfo.Config[i].negativeButton;
			    axis.negativeButton = TempInputManagerInfo.Config[i].positiveButton;
			    axis.positiveButton = TempInputManagerInfo.Config[i].positiveButton.Replace("{0}",_cotrollerIndex.ToString());
			    axis.altNegativeButton = TempInputManagerInfo.Config[i].altNegativeButton;
			    axis.altPositiveButton = TempInputManagerInfo.Config[i].altPositiveButton;
			    axis.gravity = TempInputManagerInfo.Config[i].gravity;
			    axis.dead = TempInputManagerInfo.Config[i].dead;
			    axis.sensitivity = TempInputManagerInfo.Config[i].sensitivity;
			    axis.snap = TempInputManagerInfo.Config[i].snap;
			    axis.invert = TempInputManagerInfo.Config[i].invert;
			    axis.type = TempInputManagerInfo.Config[i].type;
			    axis.axis = TempInputManagerInfo.Config[i].axis;
                if (TempInputManagerInfo.Config[i].joyNum != 0)
			        axis.joyNum = _joyNum;    
                else
                    axis.joyNum = TempInputManagerInfo.Config[i].joyNum;

                axes[i] = axis;
            }
            config = axes;
        }

        #region GetAxisRaw
        /*public float GeyAxisRaw()
        {
            if()
            return 0f;
        }*/
        #endregion

        #region GeyKey,GetKeyDown
        private KeyCode GetKeyCode()
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //処理を書く
                    return code;
                }
            }
            return KeyCode.None;
        } 
        private string GetName(string _positiveButton)
        {
            for (int i = 0;i < config.Length;i++)
            {
                if (config[i].positiveButton == _positiveButton || config[i].altPositiveButton == _positiveButton)
                {
                    return config[i].name;
                }
            }
            return "";
        }
        public bool GetButtonDown(string _name)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log(GetKeyCode().ToString()+ ":" +GetName(GetKeyCode().ToString()) + ":" + _name);
                return GetName(GetKeyCode().ToString()) == _name;
            }
            return false;
        }
        public bool GetButton(string _name)
        {
            if (Input.anyKey)
            {
                return GetName(GetKeyCode().ToString()) == _name;
            }
            return false;
        }
        #endregion
    }
}