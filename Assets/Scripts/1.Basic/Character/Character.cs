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

    public Boards boards;
    public Sprite image;
    public string skillName;
    public string skillDetail;

    public virtual void Awake() { }

    public virtual string GetName() { return name; }
    public virtual int GetLevel() { return characterLevel; }
    public virtual int GetExp() { return characterExp; }
    public virtual int GetMaxExp() { return characterMaxExp; }
    public virtual int GetAtk() { return characterAtk; }
    public virtual void CheckBeforeClearLine(int totalLineClear) { }
    public virtual void CheckAfterClearLine(int totalLineClear) { }

    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetLevel(int level)
    {
        this.characterLevel = level;
    }
    public void SetExp(int exp)
    {
        this.characterExp = exp;
    }
    public void SetMaxExp(int maxExp)
    {
        this.characterMaxExp = maxExp;
    }
    public void SetAtk(int atk)
    {
        this.characterAtk = atk;
    }
    
}
