using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelection : MonoBehaviour
{
    
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private new Text name;
    private int currentChar;

    #region Data
        private CharacterCore character;
        private IDataService DataService = new JsonDataService();
        private bool EncryptionEnable;
        private long saveTime;
    #endregion

    private void Awake()
    {
        SelectChar(0);
        UpdateChar();

    }
   
    public void SelectChar(int _index)
    {
        backButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }

    }

    public void ChangeChar(int _change)
    {
        currentChar += _change;
        SelectChar(currentChar);
        UpdateChar();
        Debug.Log(currentChar);
    }

    public void UpdateChar()
    {
        character = GetComponentInChildren<CharacterCore>();
        name.text = character.name;
    }

    public void SelectChar()
    {
    
        JsonConvert.SerializeObject(character, Formatting.Indented, 
                new JsonSerializerSettings 
                { 
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                
        if (DataService.SaveData("/characters.json", character, EncryptionEnable))
        {

            Debug.Log(character);
        }
        else
        {
            Debug.LogError("Could not save the file!");
        }


        SceneManager.LoadScene("Level_Map");
    }
}
