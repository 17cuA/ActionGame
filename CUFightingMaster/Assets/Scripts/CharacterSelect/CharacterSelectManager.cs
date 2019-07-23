using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public int currentSelectNumber = 0;     //0が左

    public GameObject[] Character = new GameObject[6];

    public GameObject SelectCursor;
    public GameObject[] CharacterPanels = new GameObject[6];

    private GameObject currentSellectPanel;
    public GameObject currentSellectCharacter;

    public GameObject CharacterCreatePoint;

    public void  SelectLeft()
    {
        if (currentSelectNumber > 0)
        {
            currentSelectNumber--;
        }
        else
            currentSelectNumber = 5;

        SelectCursor.transform.position = CharacterPanels[currentSelectNumber].transform.position;

        CreateCharacter();

    }

    public void SelectRight()
    {
        if (currentSelectNumber < 5)
        {
            currentSelectNumber++;
        }
        else
            currentSelectNumber = 0;

        SelectCursor.transform.position = CharacterPanels[currentSelectNumber].transform.position;

        CreateCharacter();

    }

    public void DecideCharacter()
    {

    }

    public void CreateCharacter()
    {
        //if (currentSellectCharacter != null)
        //    Destroy(currentSellectCharacter);
        //currentSellectCharacter  = Instantiate(Character, CharacterCreatePoint.transform);
        //currentSellectCharacter.transform.localScale =new Vector3(4, 4, 4);

        currentSellectCharacter.SetActive(false);
        currentSellectCharacter =  Character[currentSelectNumber];
        currentSellectCharacter.SetActive(true);

    }

    void Start()
    {
        
    }

    void Update()
    {
    
    }
}
