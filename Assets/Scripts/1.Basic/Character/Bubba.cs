using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubba : Character
{
    public override void Awake()
    {
        skillEnergyMax = 2351;
        skillEnergy = 0;
        skillImage = Resources.Load<Sprite>("PlayerSkill/Bubba_Skill");
        GetName();
        SetAtk(5);
        SetSkillName("");
        SetSkillDetail("Your next Piece will be I Block. Cooldown 15s.");
        this.skillReady = true;
        this.skillTiming = Time.time;
    }
    private void Start()
    {


    }
    private bool skillReady;
    public float skillTiming;
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
            boards.levelAnimationUIManager.SetMaxEnergy(skillEnergyMax);
            boards.levelAnimationUIManager.SetEnergy(skillEnergy);
            if (skillEnergy == skillEnergyMax){
                boards.levelAnimationUIManager.EnergyFull();
                boards.levelAnimationUIManager.SkillCanUse();
            } else {
                boards.levelAnimationUIManager.EnergyNotFull();
                boards.levelAnimationUIManager.SkillCannotUse();
            }
        }
        catch (Exception e){
            Debug.Log(e);
        }
        //--------------------------//

        if (this.skillTiming + this.coolDown <= Time.time)
        {
            this.skillReady = true;
            skillEnergy = 2351;
        } else {
            skillEnergy++;
        }
        if (this.skillReady == true && Input.GetKeyDown(KeyCode.E) && skillEnergy == skillEnergyMax){
            CharacterSkill();
            skillReady = false;
            skillTiming = Time.time;
            skillEnergy = 0;
            try{
                boards.levelAudioPlayer.PlayPlayerAttackSound();
                boards.animationCharacter.PlayerDoAttackAction();
                boards.characterAnimation.EnemyDoDefenseAction();
            }
            catch (Exception e){
                Debug.Log(e);
            }
        }
    }

    public override string GetName()
    {
        SetName("Bubba");
        return name;
    }
}
