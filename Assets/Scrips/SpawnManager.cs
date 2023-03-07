using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ð��� �帣�ٰ� �����ð��� �Ǹ� �� ���忡�� ���� ���� ������ ��ġ�� ��ġ�ϰ�ʹ�.
// �Ϲ� �� 5���� ����� Ư�� �� 1���� ����� �ݺ��ϰ�ʹ�. 
public class SpawnManager : MonoBehaviour
{
    float makeTime = 1;
    Transform[] spawnList = null;
    public GameObject enemyFactory;
    public GameObject enemyExFactory;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // �¾ �� �ڽ� ���ӿ�����Ʈ�� �� ��������ʹ�.
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

            // spawnList�߿� �������� �ϳ� ���ؼ� �� ��ġ�� ��ġ�ϰ�ʹ�.
            int chooseIndex = Random.Range(0, spawnList.Length);

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
