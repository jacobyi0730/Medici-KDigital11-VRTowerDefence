using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int id;
    public int maxHP = 10;
    public int hp {
        get;
        private set;
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        TowerManager.instance.InitTowerUI(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            //hp = Mathf.Max(hp - 1, 0);
            hp--;
            if (hp < 0)
                hp = 0;

            TowerManager.instance.OnMyTowerDamage(this);
        }
    }
}
