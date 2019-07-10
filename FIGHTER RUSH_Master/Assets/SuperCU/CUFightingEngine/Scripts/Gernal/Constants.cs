using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperCU.FightingEngine
{
#if UNITY_EDITOR
    public static class ConstantsEditor
    {
        //パス定数
        public const string CHARACTER_VIEW_SCENE_PATH = "Assets/SuperCU/CUFightingEngine/Scenes/CharacterView.unity";		//キャラクタビューのシーンパス
        public const string CHARACTER_PARANT_RESOURCES_PATH = "Character/CharacterParant";													//キャラクタの親オブジェクトのパス(Resources)
        public const string GAME_PROPERTY_RESOURCES_PATH = "Property/GameProperty";

		// Sceneパス
		public const string GAME_SCENE_TITLE_PATH = "Assets/SuperCU/CUFightingEngine/Scenes/UI/Title.unity";                                           // Titekシーンのパス

		public const string GAME_SCENE_BATTLE_PATH = "Assets/SuperCU/CUFightingEngine/Scenes/UI/Battle.unity";                                  // Butteleシーンのパス
		public const string GAME_SCENE_BATTLE_RESULT_PATH = "Assets/SuperCU/CUFightingEngine/Scenes/UI/BattleResult.unity";                                                   // Finシーンのパス

		public const string GAME_SCENE_SELECT_MODE_PATH = "Assets/SuperCU/CUFightingEngine/Scenes/UI/SelectMode.unity";                                                   // Finシーンのパス
		public const string GAME_SCENE_SELECT_STAGE_PATH = "Assets/SuperCU/CUFightingEngine/Scenes/UI/SelectStage.unity";                                       // Selectシーンのパス
		public const string GAME_SCENE_SELECT_CHARA_PATH = "Assets/CharacterSelect/Scenes/CharacterSelect.unity";                                                   // Finシーンのパス

		public const string GAME_SCENE_TRAINING_PATH = "Assets/SuperCU/CUFightingEngine/Scenes/UI/Training.unity";                                                   // Finシーンのパス

		// Sceneに設置するUI情報を持ったScriptbleObjectのパス
		public const string UI_PROPERTY_TITLE_PAHT = "UIProperty/Title";

		public const string UI_PROPERTY_BATTLE_PAHT = "UIProperty/Battle";
		public const string UI_PROPERTY_BATTLE_RESULT_PAHT = "UIProperty/BattleResult";

		public const string UI_PROPERTY_SELECT_MODE_PAHT = "UIProperty/SelectMode";
		public const string UI_PROPERTY_SELECT_STAGE_PAHT = "UIProperty/SelectStage";
		public const string UI_PROPERTY_SELECT_CHARA_PAHT = "UIProperty/CharacterSelect";

		public const string UI_PROPERTY_TRAINING_PAHT = "UIProperty/Training";

		//座標定数
		public const float CHARACTER_VIEW_PLACE = -300; //キャラクタビューの場所（Ｘ軸）
    }
#endif
}
