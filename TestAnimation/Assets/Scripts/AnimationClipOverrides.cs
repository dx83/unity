using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AnimationClipOverrides : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController animCont;
    //[SerializeField] Sprite sprite;
    GameObject monster;
    string monName;

    // 엑셀 데이터 : 이름, 위치값, 레이어, 태그
    void Start()
    {
        AnimationClipLoader.Load();
        monName = "Lizard";//Medusa
        monster = new GameObject(monName);
        monster.transform.position = new Vector3(0.1f, 0.07f, 0.0f);
        monster.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        monster.layer = 6;
        monster.tag = "Monster";

        Animator anim = monster.AddComponent<Animator>();
        Init(anim);

        Monster ms = monster.AddComponent<Monster>();
        ms.MonsterInitialize();
        
        SpriteRenderer sr = monster.AddComponent<SpriteRenderer>();
        sr.flipX = true;
        sr.sortingLayerName = "Monster";
        sr.sortingOrder = 0;

        BoxCollider2D bc = monster.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        bc.offset = new Vector2(0.15f, -0.04f);
        bc.size = new Vector2(0.5f, 0.5f);

        monster.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            monster.SetActive(true);
    }

    public void Init(Animator animator)
    {
        Dictionary<string, AnimationClip> clips = AnimationClipLoader.GetAnimationClip(monName);
        foreach (var clip in clips)
            animCont[$"Monster_{clip.Key}"] = clip.Value;

        animator.runtimeAnimatorController = animCont;
    }
}
