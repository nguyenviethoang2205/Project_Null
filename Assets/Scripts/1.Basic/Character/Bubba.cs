using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubba : CharacterCore
{
    public override void Awake()
    {
        GetName();
        SetLevel(1);
        SetExp(0);
        SetMaxExp(100);
        SetAtk(5);
        this.skillReady = true;
        this.skillTiming = Time.time;
    }
    private void Start()
    {
        
    }
    private bool skillReady;
    private float skillTiming;
    public void CharacterSkill()
    {
        this.boards.nextBox.ClearPiece();
        this.boards.nextBox.SpawmPiece(0);
        this.boards.ChangeNextPiece(0);
    }
    public void Update()
    {
        if (this.skillTiming + 15f <= Time.time)
        {
            this.skillReady = true;
        }
        if (this.skillReady == true && Input.GetKeyDown(KeyCode.E))
        {
            CharacterSkill();
            skillReady = false;
            skillTiming = Time.time;
        }
    }
    public override string GetName()
    {
        SetName("Bubba");
        return characterName;
    }
}
