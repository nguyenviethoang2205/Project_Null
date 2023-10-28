using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;

[JsonObject(MemberSerialization.OptIn)]
public class Character : MonoBehaviour
{
    public new Collider2D collider;
    public GameObject board;
    [JsonProperty]
    public new string name;

    [JsonProperty]
    public int characterAtk;

    [JsonProperty]
    public int monsterTrophy = 0;

    public Boards boards;
    public Sprite skillImage;
    public string skillName;
    public string skillDetail;
    public string charDifficulty;
    public string charStyle;
    public int skillEnergy;
    public int skillEnergyMax;


    public virtual void Awake() { }
    public virtual string GetSkillName() { return skillName; }
    public virtual string GetSkillDetail() { return skillDetail; }
    public virtual string GetName() { return name; }
    public virtual int GetAtk() { return characterAtk; }
    public virtual string GetStyle() { return charStyle; }
    public virtual string GetDifficulty() { return charDifficulty; }

    public virtual int GetMonsterTrophy() { return monsterTrophy; }
    public virtual void CheckBeforeClearLine(int totalLineClear) { }
    public virtual void CheckAfterClearLine(int totalLineClear) { }

    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetAtk(int atk)
    {
        this.characterAtk = atk;
    }
    public void AddTrophy()
    {
        this.monsterTrophy++;
    }
    public void LostTrophy()
    {
        this.monsterTrophy--;
    }
    public void SetSkillName(string skillName)
    {
        this.skillName = skillName;
    }
    public void SetSkillDetail(string skillDetail) 
    {
        this.skillDetail = skillDetail;
    }

    public void SetCharDifficulty(string charDifficulty) 
    {
        this.charDifficulty = charDifficulty;
    }

    public void SetCharStyle(string charStyle)
    {
        this.charStyle = charStyle;
    }
}
