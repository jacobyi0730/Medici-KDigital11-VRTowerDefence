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
    // enemy�� player���� hit�ϴ� ����
    public void OnMyAttackHit()
    {
        // enemy�� �÷��̾� ������!!
        enemy.OnMyAttackHit();
    }
}
