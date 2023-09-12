using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    MonsterCreationScript monsterCreation;
    MonsterDummy[] dummys;

    void Awake()
    {
        monsterCreation = new MonsterCreationScript();
        monsterCreation.StageMonsterInitialize();
    }

    void Start()
    {
        dummys = new MonsterDummy[10];

        for (int i = 0; i < dummys.Length; i++)
        {
            dummys[i] = new MonsterDummy();
            monsterCreation.MonsterDummyCreation(i, dummys[i], transform);
        }

        var m = dummys[0];
        monsterCreation.MonsterSpawn(1, m, true);
        monsterCreation.InsertLastMonXpos(m.monster.transform.localPosition.x);
    }

    void Update()
    {
        monsterCreation.MonsterRandomGeneration(dummys);
    }
}
