using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Fighting/ファイター")]
public class FighterStatus : ScriptableObject
{
	[System.Serializable]
	public class HitBox_
	{
		public Vector3 size;
		public Vector3 localPosition;
	}
	[System.Serializable]
	public class SkillAnimationCustom
	{
        public string name;
        public string command;
        public string trigger;
		public int skillCost = 0;//発動のためのコスト
        public int countValid = 1;//空中の場合何回発動できるか
        public int validShotFrame = 6;
        public FighterSkill skill;
	}
	[System.Serializable]
	public class MoveAnimationCustom
	{
        public string name;
        public string command;
        public FighterSkill skill;
    }
	[System.Serializable]
	public class FighterMaterials
	{
		public Material[] nomalMaterial = { };
		public Material[] inversionMaterial = { };
	}

	public int HP = 100;
	public int StanGuage = 100;
	public int SpecialGuage = 100;
	public float airBraking = 0.5f;//空中制動

	public GameObject PlayerModel;
	public FighterMaterials[] playerMaterials = { new FighterMaterials(), new FighterMaterials() };
    public Vector3 gravity = new Vector3();
    public Vector3 gravityMax = new Vector3();

    //当たり判定
    public HitBox_ headHitBox = new HitBox_();
	public HitBox_ bodyHitBox = new HitBox_();
	public HitBox_ footHitBox = new HitBox_();
    public HitBox_ grabHitBox = new HitBox_();
	public HitBox_ pushingHitBox = new HitBox_();
    //スキル
    public FighterSkill[] constantsSkills = { };
	public FighterCore fighter = null;//ファイター本体

    public List<SkillAnimationCustom> groundAttackSkills = new List<SkillAnimationCustom>();
    public List<SkillAnimationCustom> airAttackSkills = new List<SkillAnimationCustom>();

    // public List<SkillAnimationCustom> uniqueSkills = new List<SkillAnimationCustom>();
    // public List<SkillAnimationCustom> specialSkills = new List<SkillAnimationCustom>();
    // public List<SkillAnimationCustom> CASkills = new List<SkillAnimationCustom>();

    //カスタム移動
    public List<MoveAnimationCustom> groundMoveSkills = new List<MoveAnimationCustom>();
    public List<MoveAnimationCustom> airMoveSkills = new List<MoveAnimationCustom>();


    #region EDITOR_
#if UNITY_EDITOR
    [CustomEditor(typeof(FighterStatus))]
	public class FigterStatusInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("ファイター設定画面を開く"))
			{
				FigterEditor.Open((FighterStatus)target);
			}
		}
	}
#endif
	#endregion
}
