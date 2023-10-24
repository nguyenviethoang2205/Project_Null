using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class AnimationCharacter : MonoBehaviour
{
    // Animation của người chơi
    public SkeletonAnimation playerAnimation;
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
