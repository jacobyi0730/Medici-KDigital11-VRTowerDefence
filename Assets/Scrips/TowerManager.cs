using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SliderUI들을 관리하고싶다.
// 타워가 데미지를 입고 체력이 변화되면 Slider의 값도 변화시키고싶다.
// 타워가 데미지를 입었을 때 모든타워의 체력을 검사해서 모두 0이라면 게임오버 UI를 출력하고싶다. 
public class TowerManager : MonoBehaviour
{
    public static TowerManager instance = null;
    private void Awake()
    {
        TowerManager.instance = this;
    }

    public Slider[] sliderTower;
    public Tower[] towers;


    // 타워의 체력이 변화되었다.
    public void OnMyTowerDamage(Tower tower)
    {
        // 변화된 체력에 맞게 UI를 갱신
        Slider ui = GetSlider(tower.id);
        ui.value = tower.hp;
        // 모든타워의 체력을 검사
        bool isGameOver = true;
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i].hp != 0)
            {
                isGameOver = false;
                break;
            }
        }
        if (true == isGameOver)
        {
            // 게임오버...
            Time.timeScale = 0;
        }
    }

    public void InitTowerUI(Tower tower)
    {
        Slider ui = GetSlider(tower.id);
        ui.maxValue = tower.maxHP;
        ui.value = tower.hp;
    }

    Slider GetSlider(int id)
    {
        return sliderTower[id - 1];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
