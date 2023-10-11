using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDB characterDB;
    public Text nameText;
    public GameObject player;
    private int selectOption = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("selectOption")){
            selectOption = 0;
        }
        else{
            Load();
        }
        UpdateCharacter(selectOption);
    }

    public void NextOption(){
        selectOption++; 
        if(selectOption >= characterDB.CharacterCount){
            selectOption = 0;
        }

        UpdateCharacter(selectOption);
        Save();
    }

    public void BackOption(){
        selectOption--;
        if(selectOption < 0){
            selectOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectOption);
        Save();
    }

    private void UpdateCharacter(int selectOption){
        Character character = characterDB.GetCharacter(selectOption);
        player = character.player;
        nameText.text = character.characterName;
    }
    
    private void Save(){    
        PlayerPrefs.SetInt("selectOption", selectOption);
    }

    private void Load(){
        selectOption = PlayerPrefs.GetInt("selectOption");
    }
}
