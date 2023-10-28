using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyCore : MonoBehaviour
{
    public SkillBar skillBar;
    public Boards boards;

    public Sprite EnemyImage;
    
    public string EnemyName;
    public string EnemyDetail;

    public string EnemyDifficulty;
    public int EnemyHealth;
    public int maxSkillWait;
    public int skillWait;

    public void CheckStatus(){
        if (skillWait > (maxSkillWait / 3 * 2)) 
            skillBar.TurnRed();
        else
            skillBar.TurnWhite(); 
    }

    public abstract void Awake();
    // Tạo skill
    public abstract string getName();
    // Lấy giá trị máu
    public abstract int getHealth();

    public abstract string getDifficulty();

    public abstract string getDetail();

    // Kiểm tra sử dụng chiêu
    // Dùng chiêu lúc bất đầu game
    public abstract void CheckSkillStart();
    // Dùng chiêu lúc Spawn Khối
    public abstract void CheckSkillSpawn();
    // Dùng chiêu lúc Clear Dòng xong
    public abstract void CheckSkillClearLine();

    public void SetEnemyHealth(int health)
    {
        this.EnemyHealth = health;
    }

    // Phương thức set cho EnemyName
    public void SetEnemyName(string name)
    {
        this.EnemyName = name;
    }

    public void SetEnemyDetail(string detail)
    {
        this.EnemyDetail = detail;
    }

    public void SetEnemyDifficulty(string difficulty)
    {
        this.EnemyDifficulty = difficulty;
    }
}

