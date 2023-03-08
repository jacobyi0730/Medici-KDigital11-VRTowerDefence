using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 시간이 흐르다가 생성시간이 되면 적 공장에서 적을 만들어서 랜덤한 위치에 배치하고싶다.
// 일반 적 5마리 만들고 특별 적 1마리 만들고를 반복하고싶다. 
public class SpawnManager : MonoBehaviour
{
    float makeTime = 1;
    Transform[] spawnList = null;
    public GameObject enemyFactory;
    public GameObject enemyExFactory;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 태어날 때 자식 게임오브젝트를 다 가져오고싶다.
        MeshFilter[] s = GetComponentsInChildren<MeshFilter>();
        spawnList = new Transform[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            spawnList[i] = s[i].transform;
        }

        while(true)
        {
            yield return new WaitForSeconds(makeTime);

            GameObject enemy = Instantiate(enemyFactory);

            // spawnList중에 랜덤으로 하나 정해서 그 위치에 배치하고싶다.
            int chooseIndex = 0; //Random.Range(0, spawnList.Length);

            Transform choose = spawnList[chooseIndex];

            enemy.transform.position = choose.position;
            enemy.transform.rotation = choose.rotation;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
