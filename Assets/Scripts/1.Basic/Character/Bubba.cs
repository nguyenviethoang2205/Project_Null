using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubba : CharacterCore
{
    public override void Awake()
    {
        this.skillActive = true;
        this.skillTiming = Time.time;
    }
    private bool skillActive;
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
            this.skillActive = true;
        }
        if (this.skillActive == true && Input.GetKeyDown(KeyCode.E))
        {
            CharacterSkill();
            skillActive = false;
            skillTiming = Time.time;
        }
    }
    public override string GetName()
    {
        SetName("Bubba");
        return characterName;
    }
}
