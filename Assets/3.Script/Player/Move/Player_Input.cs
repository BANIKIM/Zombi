using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    [SerializeField] private string MoveAxis_name="Vertical";
    [SerializeField] private string Rotate_name = "Horizontal";
    [SerializeField] private string Fire = "Fire1";
    [SerializeField] private string Reload = "Reload";

    //GetAxis 반환형은 float
    public float Move_Value { get; private set; }
    public float Rotate_Value { get; private set; }

    //GetButton 반환형 bool로
    public bool isFire { get; private set; }
    public bool isReload { get; private set; }

    private void Update()
    {
        //나중에 게임오버를 만든다면 못 움직이게 선언해주세요...

        Move_Value = Input.GetAxis(MoveAxis_name);//움직임
        Rotate_Value = Input.GetAxis(Rotate_name);//몸을 돌리는것

        isFire = Input.GetButton(Fire);//총 발사
        isReload = Input.GetButton(Reload);// 리로드

    }
}
