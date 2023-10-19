using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    //매개변수 피해량, 맞은 위치, 맞은 각도
    void OnDamage(float Damage, Vector3 hitposition, Vector3 hitNomal);
    
}