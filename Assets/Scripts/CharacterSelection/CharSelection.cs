﻿using System;
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
    [SerializeField] private new Text difficulty;
    [SerializeField] private new Text style;
    [SerializeField] private new Text detail;
    [SerializeField] private new Text atk;
    [SerializeField] private new Text skill;
    [SerializeField] private new SpriteRenderer image;
    private int currentChar;

    #region Data
    private Character character = new Character();
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    public NewData newData;
    #endregion

    private void Awake()
    {
        newData = GetComponent<NewData>();
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
        character = GetComponentInChildren<Character>();
        name.text = character.name;
        difficulty.text = character.charDifficulty;
        style.text = character.charStyle;
        detail.text = character.skillDetail;
        atk.text = character.characterAtk.ToString(); 
        image.sprite = character.skillImage;
        skill.text = character.skillName;

        if (character.charDifficulty == "Easy"){
            TurnGreen(difficulty);
        } else if (character.charDifficulty == "Normal"){
            TurnYellow(difficulty);
        } else {
            TurnRed(difficulty);
        }

        if (character.charStyle == "Attack"){
            TurnRed(style);
        } else if (character.charStyle == "Control"){
            TurnBlue(style);
        } else {
            TurnGray(style);
        }
    }

    public void SaveChar()
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
            Debug.LogError("Could not save character!");
        }
    }

    public void SelectChar()
    {
        SaveChar();
        newData.NewMapData();
        SceneManager.LoadScene("Level_Map");
    }

    private void TurnGreen(Text text){
        Color color = new Color(0f, 0.8f, 0.2196f);
        text.color = color;
    }

    private void TurnYellow(Text text){
        Color color = new Color(0.8f, 0.7529f, 0.1647f, 1f);
        text.color = color;
    }

    private void TurnRed(Text text){
        Color color = new Color(0.8f, 0.1137f, 0.1098f, 1f);
        text.color = color;
    }
    
    public void TurnBlue(Text text){
        Color color = new Color(0.15f, 0.76f, 0.8f);
        text.color = color;
    }
    
    public void TurnGray(Text text){
        Color color = new Color(0.4f, 0.4f, 0.4f);
        text.color = color;
    }
}
