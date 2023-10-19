using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GunData",fileName ="Gun_Data")]//찾아보기

public class GunData : ScriptableObject//ScriptableObject데이터 포멧
{
    /*
     * 공격력 => float
     * 연사력 => float => 코루틴
     * 재장전 시간  => float
     * 처음 탄창용량 => int
     * 총소리 => audio clip
     * 재장전 소리  => audio clip
     * 탄창용량 => int
     */

    public float Damage = 25f;
    public float TimebetFire = 0.12f;//연사력
    public float ReloadTime = 1.8f;
    public int MagCapacity = 30; // 탄창 용량
    public int StartAmmoRemaion = 100;//남은 탄 수

    public AudioClip Shot_clip;
    public AudioClip Reload_clip;



}
