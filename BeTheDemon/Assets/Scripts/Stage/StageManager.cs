using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController animCont;
    [SerializeField] Material colorShaderMat;
    MonsterDummy[] dummys;

    void Awake()
    {
        AnimationClipLoader.Load();
        dummys = new MonsterDummy[10];

        for (int i = 0; i < dummys.Length; i++)
        {
            dummys[i] = new MonsterDummy();
            MonsterCreationScript.MonsterDummyCreation(dummys[i], transform);
            dummys[i].animCont = animCont;
            dummys[i].controlMon.colorShaderMat = colorShaderMat;
        }
    }

    void Start()
    {
        //GameObject obj = Instantiate(hero, this.transform);
        MonsterCreationScript.MonsterSpawn("Lizard", dummys[0]);
        dummys[0].monster.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            MonsterCreationScript.MonsterSpawn("Medusa", dummys[0]);
        if (Input.GetKeyDown(KeyCode.S))
            MonsterCreationScript.MonsterSpawn("Jinn", dummys[0]);
        if (Input.GetKeyDown(KeyCode.Z))
            MonsterCreationScript.MonsterSpawn("Dragon", dummys[0]);
        if (Input.GetKeyDown(KeyCode.X))
            MonsterCreationScript.MonsterSpawn("Lizard", dummys[0]);

    }

}
