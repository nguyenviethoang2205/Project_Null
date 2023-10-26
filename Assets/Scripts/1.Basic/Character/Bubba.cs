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
        board = GameObject.FindGameObjectWithTag("Board");
        boards = board.GetComponent<Boards>();
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
