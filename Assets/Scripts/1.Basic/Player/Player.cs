using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterDB characterDB;
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

    private void UpdateCharacter(int selectOption){
        Character character = characterDB.GetCharacter(selectOption);
        player = character.player;
    }

    private void Load(){
        selectOption = PlayerPrefs.GetInt("selectOption");
    }
}
