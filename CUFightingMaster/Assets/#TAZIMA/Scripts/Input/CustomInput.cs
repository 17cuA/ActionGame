using UnityEngine;
using System;

namespace CustomInputClass
{
    public class CustomInput
    {
        /// <summary>
        /// CustomInputの変数
        /// </summary>
        public int playerIndex; //プレイヤー番号
        public int controllerIndex;
	    public int configIndex;
        public TempInputManagerInfo.InputAxis[] config;

        #region SerConfig
        /// <summary>
        /// コンフィグの設定を行います
        /// </summary>
        /// <param name="_playerIndex"></param>
        /// <param name="_cotrollerIndex"></param>
        /// <param name="_joyNum"></param>
        public void SetConfig(int _playerIndex,int _cotrollerIndex)
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
			        axis.joyNum = _cotrollerIndex;    
                else
                    axis.joyNum = TempInputManagerInfo.Config[i].joyNum;

                axes[i] = axis;
            }
            config = axes;
        }
        #endregion

        #region GetAxisRaw
        /// <summary>
        /// 軸の入力を受け取りfloat値を返します
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public float GetAxisRaw(string _name)
        {
			return Input.GetAxisRaw(_name);
        }
        #endregion

        #region GeyKey,GetKeyDown
        /// <summary>
        /// 入力されたKeyCodeをstring型で返します
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// コンフィグに保存されているボタンと引数で受け取ったボタンを比較し、
        /// trueならコンフィグに保存されている仮想ボタンを返す
        /// </summary>
        /// <param name="_positiveButton"></param>
        /// <returns></returns>
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

        /// <summary>
        /// ボタンが押された時の処理
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public bool GetButtonDown(string _name)
        {
            if (Input.anyKeyDown)
            {
                return GetName(GetKeyCode().ToString()) == _name;
            }
            return false;
        }

        /// <summary>
        /// ボタンが押されている時の処理
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
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