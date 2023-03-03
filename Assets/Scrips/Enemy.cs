using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 상태를 만들고 제어하고싶다.
// 검색, 이동, 공격
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
        // 가장 가까운 타워를 찾고싶다.
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        // 1. 측정한 거리를 담을 변수
        float dist = float.MaxValue;
        // 2. 가장 가까운 인덱스
        int chooseIndex = -1;
        for (int i = 0; i < towers.Length; i++)
        {
            // 각 목적지마다 거리(temp)를 재고싶다.
            float temp = Vector3.Distance(transform.position, towers[i].transform.position);
            if (dist > temp) // dist > temp 라면
            {
                dist = temp;
                chooseIndex = i;
            }
        }
        // 그곳을 목적지로 하고싶다.
        target = towers[chooseIndex];
        // 이동 상태로 전이하고싶다.
        state = State.Walk;
    }
}