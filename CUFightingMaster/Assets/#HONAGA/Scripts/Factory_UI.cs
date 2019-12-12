using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory_UI : MonoBehaviour
{
    public Dictionary<string, CharacterSelectObjectImage> UIDic = new Dictionary<string, CharacterSelectObjectImage>();
    public ScriptableObject_UI SU;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SU.Ui.Count; i++)
        {
            Debug.Log(i);
            UIDic.Add(SU.Ui[i].name, new CharacterSelectObjectImage(ref SU.Ui[i].gameObj, SU.Ui[i].image));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
