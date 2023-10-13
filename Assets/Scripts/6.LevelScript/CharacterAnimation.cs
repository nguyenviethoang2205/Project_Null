﻿using UnityEngine;
using Spine.Unity;
using Spine;

public class CharacterAnimation : MonoBehaviour
{
    // Animation của người chơi
    public SkeletonAnimation playerAnimation;
    
    // Animation của quái vật
    public SkeletonAnimation enemyAnimation;

    // Các hoạt động của Player
    // Người chơi thực hiện hành động tấn công
    public void PlayerDoAttackAction(){
        playerAnimation.AnimationState.Complete +=  (trackEntry) => WaitAnimationComplete(trackEntry, "attack/melee/mouth-bite", "action/idle/normal", playerAnimation);
        DoAnimation("attack/melee/mouth-bite", playerAnimation);
    }
    // người chơi bị đấm
    public void PlayerDoDefenseAction(){
        playerAnimation.AnimationState.Complete += (trackEntry) => WaitAnimationComplete(trackEntry, "defense/hit-by-normal", "action/idle/normal", playerAnimation);
        DoAnimation("defense/hit-by-normal", playerAnimation);
    }
    // Người chơi thắng
    public void PlayerDoVictoryAction(){
        LoopAnimation("activity/victory-pose-back-flip", playerAnimation);
    }
    // Người chơi thất bại
    public void PlayerDoLoseAction(){
        LoopAnimation("activity/prepare", playerAnimation);
    }
    
    // Các hoạt động của Enemy
    // enemy tấn công
    public void EnemyDoAttackAction(){
        enemyAnimation.AnimationState.Complete +=  (trackEntry) => WaitAnimationComplete(trackEntry, "attack/melee/normal-attack", "action/idle/normal", enemyAnimation);
        DoAnimation("attack/melee/normal-attack", enemyAnimation);
    }
    // Enemy thực hiện hành động bị tấn công
    public void EnemyDoDefenseAction(){
        enemyAnimation.AnimationState.Complete += (trackEntry) => WaitAnimationComplete(trackEntry, "defense/hit-by-normal", "action/idle/normal", enemyAnimation);
        DoAnimation("defense/hit-by-normal", enemyAnimation);
    }

    // Kẻ địch chiến thắng
    public void EnemyDoVictoryAction(){
        LoopAnimation("battle/get-buff", enemyAnimation);
    }

    // Kẻ địch thất bại
    public void EnemyDoLoseAction(){
        DoAnimation("defense/hit-die", enemyAnimation);
    }
    
    // Thực hiện Animation
    private void DoAnimation(string animation, SkeletonAnimation characterAnimation){
        characterAnimation.AnimationState.SetAnimation(0, animation, loop:false);
    }

    // Thực hiện Animation được lập lại liên tục
    private void LoopAnimation(string animation, SkeletonAnimation characterAnimation){
        characterAnimation.AnimationState.SetAnimation(0, animation, loop:true);
    }

    // Bất kỳ nhân vật nào thực hiện hành động nhàn rỗi
    public void DoIdieAction(SkeletonAnimation characterAnimation){   
        LoopAnimation("action/idle/normal", characterAnimation);
    }

    // Đợi hành động được hoàn thành
    private void WaitAnimationComplete(TrackEntry trackEntry, string completeAnimation, string returnAnimation, SkeletonAnimation characterAnimation)
    {
        if (trackEntry.Animation.Name == completeAnimation)
        {
            DoIdieAction(characterAnimation);
        }
    }

}
