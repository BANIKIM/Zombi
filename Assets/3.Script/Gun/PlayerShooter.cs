using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    /*
     * 총쏘기 재장전 -> Gun 
     * Player input
     * Gun 오브젝트 손에 맞추기 -> animator
     * 
     */

    public Gun gun;

    //총기 위치 맞추기 위한 Transform들
    public Transform gunpivot;
    public Transform LeftHand_Mount;
    public Transform RightHand_Mount;

    [SerializeField] private Animator animator;
    [SerializeField] private Player_Input input;

    private void Start()
    {
        input = GetComponent<Player_Input>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //input관련된 이벤트 호출
        if(input.isFire)
        {
            gun.Fire();
        }
        else if(input.isReload)
        {
            if(gun.Reload())
            {
                animator.SetTrigger("Reload");
            }
        }    
    }


    private void OnAnimatorIK(int layerIndex)
    {
        //총의 기준점을 오른쪽 팔꿈치로 이동 
        gunpivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);


        //ik를 사용하여 왼손의 위치와 회전을 총 왼쪽 손잡이에 맞춤
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);//SetIKPositionWeight 가중치를 부여 할 수 있다.
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHand_Mount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHand_Mount.rotation);

        //ik를 사용하여 오른손의 위치와 회전을 총 손잡이에 맞춤
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, RightHand_Mount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, RightHand_Mount.rotation);
    }

}
