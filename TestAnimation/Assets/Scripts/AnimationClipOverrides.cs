using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.AddressableAssets;
using UnityEngine.U2D;


public class AnimationClipOverrides : MonoBehaviour
{
    [System.Serializable]
    private class AnimationClipOverride
    {
        public string clipNamed;
        public AnimationClip overrideWith;
        // 변경사항
        //public string name;
        //public string state;
        //public AnimationClip ani;
    }

    [SerializeField] AnimationClipOverride[] clipOverrides;//어드레서블 사용, 파일입출력 필요
    
    [SerializeField] AnimatorOverrideController animCont;
    //[SerializeField] Sprite sprite;
    GameObject monster;
    string monName;

    // 엑셀 데이터 : 이름, 위치값, 레이어, 태그
    void Start()
    {
        monName = "Medusa";
        monster = new GameObject("Monster");
        monster.transform.position = new Vector3(0.1f, 0.07f, 0.0f);
        monster.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        monster.layer = 6;
        monster.tag = "Monster";

        Animator anim = monster.AddComponent<Animator>();
        Init(anim);

        Monster ms = monster.AddComponent<Monster>();
        ms.InserthashAniFunc();//여기서 이니셜라이즈해야할듯
        
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
        foreach (AnimationClipOverride clipOverride in clipOverrides)
        {
            string[] str = clipOverride.overrideWith.name.Split('_');
            if (str[0] == monName)
                animCont[$"Monster_{str[1]}"] = clipOverride.overrideWith;
        }
        animator.runtimeAnimatorController = animCont;
    }
}

#if AA
public class SpriteSheetManager
{
    private static Dictionary<string, Sprite> spriteSheets = new Dictionary<string, Sprite>();
    public static void Load()
    {
        var loadCall = Addressables.LoadAssetAsync<SpriteAtlas>("Assets/Images/Atlas/Image_Atlas.spriteatlas");
        var atals = loadCall.WaitForCompletion();

        Sprite[] sprites = new Sprite[atals.spriteCount];
        atals.GetSprites(sprites);

        foreach (Sprite sprite in sprites)
        {
            var newName = sprite.name.Replace("(Clone)", "");
            if (!spriteSheets.ContainsKey(sprite.name))
                spriteSheets.Add(newName, sprite);
        }
    }

    public static Sprite GetSpriteByName(string name)
    {
        if (spriteSheets.ContainsKey(name))
            return spriteSheets[name];

        return null;
    }
}
#endif
