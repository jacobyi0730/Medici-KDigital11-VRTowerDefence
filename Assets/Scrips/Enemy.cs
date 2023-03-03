using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���¸� ����� �����ϰ�ʹ�.
// �˻�, �̵�, ����
public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    public enum State
    {
        Find,
        Walk,
        Attack
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
        case State.Find: UpdateFine(); break;
        case State.Walk: UpdateWalk(); break;
        case State.Attack: UpdateAttack(); break;
        }
    }

    private void UpdateAttack()
    {
    }

    private void UpdateWalk()
    {
        agent.SetDestination(target.transform.position);

    }

    GameObject target;
    private void UpdateFine()
    {
        // ���� ����� Ÿ���� ã��ʹ�.
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        // 1. ������ �Ÿ��� ���� ����
        float dist = float.MaxValue;
        // 2. ���� ����� �ε���
        int chooseIndex = -1;
        for (int i = 0; i < towers.Length; i++)
        {
            // �� ���������� �Ÿ�(temp)�� ���ʹ�.
            float temp = Vector3.Distance(transform.position, towers[i].transform.position);
            if (dist > temp) // dist > temp ���
            {
                dist = temp;
                chooseIndex = i;
            }
        }
        // �װ��� �������� �ϰ�ʹ�.
        target = towers[chooseIndex];
        // �̵� ���·� �����ϰ�ʹ�.
        state = State.Walk;
    }
}