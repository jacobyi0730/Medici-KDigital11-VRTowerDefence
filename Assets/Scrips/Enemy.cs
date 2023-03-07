using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

using Random = UnityEngine.Random;

// 상태를 만들고 제어하고싶다.
// 검색, 이동, 공격
public class Enemy : MonoBehaviour
{
    // Low Data
    public int UID = 1;
    string MonName = "Normal";
    int MaxHP = 2;
    int Atk = 1;

    public Slider sliderHP;

   

    public TextMeshProUGUI textHP;
    int hp;
    int HP
    {
        get { return hp; }
        set {
            hp = value;
            sliderHP.value = hp;
            textHP.text = hp + " / " + MaxHP;
        }
    }

    NavMeshAgent agent;
    public enum State
    {
        Find,
        Walk,
        Attack
    }
    State state;
    Animator anim;

    GameObject bulletFactory;
    // Start is called before the first frame update
    void Start()
    {
        bulletFactory = Resources.Load("EnemyBullet") as GameObject;

        sliderHP.maxValue = MaxHP;
        HP = MaxHP;
        anim = GetComponentInChildren<Animator>();
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

    float attackDistance = 12;
    private void UpdateWalk()
    {
        agent.SetDestination(target.transform.position);
        // 거리를 구하고
        float dist = Vector3.Distance(transform.position, target.transform.position);
        // 만약 거리 < 공격 가능 거리에 도착했다면
        if (dist < attackDistance)
        {
            state = State.Attack; // 공격 상태로 전이하고싶다.
            anim.SetTrigger("Attack");
        }
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

    public Transform firePosition;
    internal void OnMyAttackHit()
    {
        // 총알공장에서 총알을 만들어서 총구위치에 배치하고싶다.
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = firePosition.position;

        Vector3 randTarget = firePosition.position + firePosition.forward + Random.insideUnitSphere * 0.1f;

        bullet.transform.forward = randTarget - firePosition.position;


    }
}