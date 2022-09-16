using UnityEngine;
using System.Collections.Generic;


public class MonsterDummy
{
    public GameObject monster;
    public Animator anim;
    public AnimatorOverrideController animCont;
    public ControlCharacterMonster controlMon;
    public BoxCollider2D boxCollider;
}

public class MonsterCreationScript
{
    public static void MonsterDummyCreation(MonsterDummy dummy, Transform parent)
    {
        dummy.monster = new GameObject("Monster");
        dummy.monster.transform.SetParent(parent);
        
        dummy.monster.layer = 6;
        dummy.monster.tag = "Monster";
        
        dummy.anim = dummy.monster.AddComponent<Animator>();
        dummy.anim.runtimeAnimatorController = dummy.animCont;
        
        dummy.controlMon = dummy.monster.AddComponent<ControlCharacterMonster>();
        
        SpriteRenderer sr = dummy.monster.AddComponent<SpriteRenderer>();
        sr.flipX = true;
        sr.sortingLayerName = "Monster";
        sr.sortingOrder = 0;
        
        dummy.boxCollider = dummy.monster.AddComponent<BoxCollider2D>();
        dummy.boxCollider.isTrigger = true;
        
        dummy.monster.SetActive(false);
    }

    public static void MonsterSpawn(string monName, MonsterDummy dummy)
    {
        dummy.monster.name = monName;
        dummy.monster.transform.localPosition = new Vector3(3.0f, -0.9f, 0.0f);
        dummy.monster.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        
        InsertAnimationClips(monName, dummy);

        dummy.controlMon.MonsterInitialize();

        dummy.boxCollider.offset = new Vector2(0.15f, -0.04f);
        dummy.boxCollider.size = new Vector2(0.5f, 0.5f);
    }

    public static void InsertAnimationClips(string monName, MonsterDummy dummy)
    {
        Dictionary<string, AnimationClip> clips = AnimationClipLoader.GetAnimationClip(monName);
        foreach (var clip in clips)
            dummy.animCont[$"Monster_{clip.Key}"] = clip.Value;

        dummy.anim.runtimeAnimatorController = dummy.animCont;
    }
}
