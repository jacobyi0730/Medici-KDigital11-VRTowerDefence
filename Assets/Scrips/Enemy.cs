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
        set
        {
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
        Attack,
        Damage,
        Die,    // 바로 터짐
        Boom    // 공중 도약 후 터짐
    }
    State state;
    Animator anim;

    GameObject bulletFactory;
    public float attackDistance = 12;
    // Start is called before the first frame update
    void Start()
    {
        bulletFactory = Resources.Load("EnemyBullet") as GameObject;

        sliderHP.maxValue = MaxHP;
        HP = MaxHP;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);

        agent.stoppingDistance = attackDistance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Find: UpdateFind(); break;
            case State.Walk: UpdateWalk(); break;
            case State.Attack: UpdateAttack(); break;
            case State.Damage: UpdateDamage(); break;
            case State.Die: UpdateDie(); break;
            case State.Boom: UpdateBoom(); break;
        }
    }

    private void UpdateBoom()
    {
        Destroy(gameObject);
    }

    private void UpdateDie()
    {
        Destroy(gameObject);
    }
    

    float currentTime;
    private void UpdateDamage()
    {
        // 시간이 흐르다가
        currentTime += Time.deltaTime;
        // 1초가 지나면
        if (currentTime > 1)
        {
            // 이동상태로 전이하고싶다.
            state = State.Walk;
            // 다시 agent에게 움직이라고 하고싶다.
            agent.isStopped = false;
            anim.speed = 1;
        }
    }

    private void UpdateAttack()
    {
        // 목적지의 체력이 0이면
        if (target != null && target.hp == 0)
        {
            // 다른 목적지를 찾고싶다.
            state = State.Find;
            anim.SetTrigger("Walk");
        }
    }

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

    Tower target;
    private void UpdateFind()
    {
        // 가장 가까운 타워를 찾고싶다.
        Tower[] towers = GameObject.FindObjectsOfType<Tower>();
        // 1. 측정한 거리를 담을 변수
        float dist = float.MaxValue;
        // 2. 가장 가까운 인덱스
        int chooseIndex = -1;
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i].hp == 0)
            {
                continue;
            }
            // 각 목적지마다 거리(temp)를 재고싶다.
            float temp = Vector3.Distance(transform.position, towers[i].transform.position);
            if (dist > temp) // dist > temp 라면
            {
                dist = temp;
                chooseIndex = i;
            }
        }
        // 목적지를 골랐다면
        if (chooseIndex != -1)
        {
            // 그곳을 목적지로 하고싶다.
            target = towers[chooseIndex];
            // 이동 상태로 전이하고싶다.
            state = State.Walk;
        }
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

    public void OnMyDamageProcess(int damage, bool isBoom = false)
    {
        // 체력을 감점 시키고싶다.
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
        }
        // agent를 멈추고싶다.
        agent.isStopped = true;

        // 만약 체력이 남았다면 
        if (HP > 0)
        {
            //  Damage 상태로 전이하고싶다.
            state = State.Damage;
            anim.speed = 0;
        }
        // 그렇지않고 isBoom이 참이라면
        else if (isBoom) // HP == 0 && isBoom
        {
            //  Boom상태로 전이하고싶다.
            state = State.Boom;
            agent.enabled = false;
        }
        // 이도저도 아니라면
        else  // HP == 0 && false == isBoom
        {
            //  Die상태로 전이하고싶다.
            state = State.Die;
            // 폭발 VFX를 표현하고싶다.
            GameObject explosionFactory = Resources.Load("Explosion") as GameObject;

            GameObject exp = Instantiate(explosionFactory);
            exp.transform.position = transform.position;
            Destroy(exp, 2);
        }
    }
}