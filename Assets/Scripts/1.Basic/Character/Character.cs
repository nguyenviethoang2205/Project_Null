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
    public int characterLevel;

    [JsonProperty]
    public int characterExp;

    [JsonProperty]
    public int characterMaxExp;

    [JsonProperty]
    public int characterAtk;

    [JsonProperty]
    public int monsterTrophy = 0;

    public Boards boards;
    public Sprite image;
    public string skillName;
    public string skillDetail;


    public virtual void Awake() { }
    public virtual string GetSkillName() { return skillName; }
    public virtual string GetSkillDetail() { return skillDetail; }
    public virtual string GetName() { return name; }
    public virtual int GetAtk() { return characterAtk; }
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
    public void SetMonsterTrophy(int trophy)
    {
        this.monsterTrophy = trophy;
    }
    public void SetSkillName(string skillName)
    {
        this.skillName = skillName;
    }
    public void SetSkillDetail(string skillDetail) 
    {
        this.skillDetail = skillDetail;
    }
}
