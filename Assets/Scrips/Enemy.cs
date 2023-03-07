using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

using Random = UnityEngine.Random;

// ���¸� ����� �����ϰ�ʹ�.
// �˻�, �̵�, ����
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
        // �Ÿ��� ���ϰ�
        float dist = Vector3.Distance(transform.position, target.transform.position);
        // ���� �Ÿ� < ���� ���� �Ÿ��� �����ߴٸ�
        if (dist < attackDistance)
        {
            state = State.Attack; // ���� ���·� �����ϰ�ʹ�.
            anim.SetTrigger("Attack");
        }
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

    public Transform firePosition;
    internal void OnMyAttackHit()
    {
        // �Ѿ˰��忡�� �Ѿ��� ���� �ѱ���ġ�� ��ġ�ϰ�ʹ�.
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = firePosition.position;

        Vector3 randTarget = firePosition.position + firePosition.forward + Random.insideUnitSphere * 0.1f;

        bullet.transform.forward = randTarget - firePosition.position;


    }
}