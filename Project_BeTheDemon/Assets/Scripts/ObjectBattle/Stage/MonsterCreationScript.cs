using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class MonsterDummy
{
    public int monNum;
    public GameObject monster;
    public Animator anim;
    public AnimatorOverrideController animCont;
    public ControlCharacterMonster controlMon;
    public BoxCollider2D boxCollider;
}

public class MonsterFixedInfo
{
    public int monNum;//0 Monster 1 Lizard 2 Jinn 3 Medusa 4 Dragon
    public string monName;
    public float attackDelayTime;
    public Vector3 localYpos;
    public Vector3 localScale;
    public Vector2 boxOffset;
    public Vector2 boxSize;
    public int[] aniHash;
    public int hitPoint;
}

public class MonsterCreationScript
{
    AnimatorOverrideController animCont;
    Material colorShaderMat;

    int monsterMax;
    MonsterFixedInfo[] monster;
    string[] monName = { "Monster", "Lizard", "Jinn", "Medusa", "Dragon", };
    float[] attackDelayTime = { 0.0f, 1.0f, 1.0f, 1.0f, 2.0f, };
    Vector3[] localYpos =
    {
        Vector3.zero,
        new Vector3(3f, -2f, 0f),
        new Vector3(3f, -1.8f, 0f),
        new Vector3(3f, -1.8f, 0f),
        new Vector3(4f, -1.3f, 0f),
    };
    Vector3[] localScale =
    {
        Vector3.zero,
        new Vector3(4f, 4f, 1f),
        new Vector3(3f, 3f, 1f),
        new Vector3(4f, 4f, 1f),
        new Vector3(4f, 4f, 1f),
    };
    Vector2[] boxOffset =
    {
        Vector2.zero,
        new Vector2(0.15f, -0.04f),
        new Vector2(0.15f, -0.04f),
        new Vector2(0.0f, -0.04f),
        new Vector2(-0.35f, -0.25f),
    };
    Vector2[] boxSize =
    {
        Vector2.zero,
        new Vector2(0.5f, 0.5f),
        new Vector2(0.57f, 0.7f),
        new Vector2(0.5f, 0.5f),
        new Vector2(1.0f, 0.8f),
    };
    int[] hitPoint = { 0, 3, 5, 6, 10 };

    AnimationClipLoader clipLoader;

    public void StageMonsterInitialize()
    {
        clipLoader = new AnimationClipLoader();
        clipLoader.Load();
        
        var handleAnim = Addressables.LoadAssetAsync<AnimatorOverrideController>("Assets/Animations/Monster/AnimatorOverrideMonster.overrideController");
        animCont = handleAnim.WaitForCompletion();

        var handleMat = Addressables.LoadAssetAsync<Material>("Assets/Material/ColorMat.mat");
        colorShaderMat = handleMat.WaitForCompletion();

        monster = new MonsterFixedInfo[monsterMax = 5];

        for (int i = 0; i < monster.Length; i++)
        {
            monster[i] = new MonsterFixedInfo();
            monster[i].monNum = i;
            monster[i].hitPoint = hitPoint[i];
            monster[i].monName = monName[i];
            monster[i].attackDelayTime = attackDelayTime[i];
            monster[i].localYpos = localYpos[i];
            monster[i].localScale = localScale[i];
            monster[i].boxOffset = boxOffset[i];
            monster[i].boxSize = boxSize[i];
        }
    }

    public void MonsterDummyCreation(int index, MonsterDummy dummy, Transform parent)
    {
        var e = monster[0];

        dummy.monNum = e.monNum;
        dummy.monster = new GameObject(e.monName);
        dummy.monster.transform.SetParent(parent);

        dummy.monster.layer = 7;
        dummy.monster.tag = e.monName;
        
        dummy.anim = dummy.monster.AddComponent<Animator>();
        dummy.animCont = new AnimatorOverrideController(animCont);
        dummy.anim.runtimeAnimatorController = dummy.animCont;

        dummy.controlMon = dummy.monster.AddComponent<ControlCharacterMonster>();
        dummy.controlMon.InsertDummyNumber(index);
        dummy.controlMon.colorShaderMat = colorShaderMat;
        dummy.controlMon.InsertAttackDelayTime(e.attackDelayTime);

        SpriteRenderer sr = dummy.monster.AddComponent<SpriteRenderer>();
        sr.flipX = true;
        sr.sortingLayerName = e.monName;
        sr.sortingOrder = 0;

        dummy.boxCollider = dummy.monster.AddComponent<BoxCollider2D>();
        dummy.boxCollider.isTrigger = true;

        dummy.monster.SetActive(false);
    }

    void InsertAnimationClips(MonsterDummy dummy)
    {
        var e = monster[dummy.monNum];
        Dictionary<string, AnimationClip> clips = clipLoader.GetAnimationClip(e.monName);
        
        foreach (var clip in clips)
            dummy.animCont[$"Monster_{clip.Key}"] = clip.Value;
    }

    public void MonsterSpawn(int monNum, MonsterDummy dummy, bool firstMonster = false)
    {
        var e = monster[monNum];

        dummy.monNum = monNum;
        dummy.monster.name = e.monName;
        
        InsertAnimationClips(dummy);

        dummy.controlMon.MonsterInitialize(e.hitPoint);
        dummy.controlMon.InsertAttackDelayTime(e.attackDelayTime);

        dummy.monster.transform.localScale = e.localScale;
        
        dummy.boxCollider.offset = e.boxOffset;
        dummy.boxCollider.size = e.boxSize;

        if (firstMonster == false)
        {
            var randomXpos = Random.Range(0.0f, 2.0f);
            e.localYpos = new Vector3(lastMonXpos += 1.7f + randomXpos, e.localYpos.y, e.localYpos.z);
        }
        dummy.monster.transform.localPosition = e.localYpos;

        dummy.monster.SetActive(true);
    }

    float lastMonXpos;
    public void InsertLastMonXpos(float x) => lastMonXpos = x; 

    public void MonsterRandomGeneration(MonsterDummy[] dummys)
    {
        foreach (var m in dummys)
        {
            if (m.controlMon.MonsterDeath())
            {
                var randomMonNum = Random.Range(1, monsterMax - 1);
                MonsterSpawn(randomMonNum, m);
                InsertLastMonXpos(m.monster.transform.localPosition.x);
            }
        }
    }
}
