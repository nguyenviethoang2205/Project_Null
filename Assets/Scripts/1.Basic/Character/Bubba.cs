using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubba : Character
{
    public override void Awake()
    {
        GetName();
        SetAtk(5);
        SetSkillName("");
        SetSkillDetail("Your next Piece will be I Block. Cooldown 15s.");
        //characterSkillBar.setMaxSkillValue(skillEnergyMax);
        //characterSkillBar.setSkillValue(skillEnergy);
        this.skillReady = true;
        this.skillTiming = Time.time;
    }
    private void Start()
    {


    }
    private bool skillReady;
    private float skillTiming;
    private float coolDown = 15f;

    public void CharacterSkill()
    {
        this.boards.nextBox.ClearPiece();
        this.boards.nextBox.SpawmPiece(0);
        this.boards.ChangeNextPiece(0);
    }

    private void Update()
    {
        //--------------------------//
        try{
            board = GameObject.FindGameObjectWithTag("Board");
            boards = board.GetComponent<Boards>();
        }
        catch (Exception e){
            Debug.Log(e);
        }
        //--------------------------//

        if (this.skillTiming + this.coolDown <= Time.time)
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
        return name;
    }
}
