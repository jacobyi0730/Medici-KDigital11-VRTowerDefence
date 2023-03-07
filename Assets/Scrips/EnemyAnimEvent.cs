using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    public Enemy enemy;
    // enemy가 player에게 hit하는 순간
    public void OnMyAttackHit()
    {
        // enemy야 플레이어 공격해!!
        enemy.OnMyAttackHit();
    }
}
