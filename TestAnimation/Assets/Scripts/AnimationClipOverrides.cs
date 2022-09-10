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
    }

    [SerializeField] AnimationClipOverride[] clipOverrides;
    [SerializeField] RuntimeAnimatorController animCont;
    [SerializeField] Sprite sprite;
    GameObject monster;

    // 엑셀 데이터 : 이름, 위치값, 레이어, 태그
    void Start()
    {
        monster = new GameObject("Lizard");
        monster.transform.position = new Vector3(0.1f, 0.07f, 0.0f);
        monster.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        monster.layer = 6;
        monster.tag = "Monster";

        Animator anim = monster.AddComponent<Animator>();
        anim.runtimeAnimatorController = animCont;
        //Init(anim);
        
        Monster ms = monster.AddComponent<Monster>();

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

    // Use this for initialization
    public void Init(Animator animator)
    {
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;

        foreach (AnimationClipOverride clipOverride in clipOverrides)
        {
            overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;
        }
        animator.runtimeAnimatorController = overrideController;
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
