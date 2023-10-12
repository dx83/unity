using UnityEngine;
using UnityEngine.AddressableAssets;


public class CreateBattleObject
{
    // BattleScreen
    GameObject battleScreen;
    Camera gameCamera;
    GameObject stage;
    GameObject backgrounds;
    GameObject stPos;
    GameObject hero;

    public void CreateBattleScreen()
    {
        battleScreen = new GameObject("BattleScreen");
        battleScreen.transform.localPosition = new Vector3(0f, 200f, 0f);
        BattleCamera();
        CreateStage();
    }

    void BattleCamera()
    {
        GameObject camObj = new GameObject("GameCamera");
        camObj.transform.SetParent(battleScreen.transform);
        camObj.transform.localPosition = new Vector3(0f, 0f, -10f);

        gameCamera = camObj.AddComponent<Camera>();
        gameCamera.clearFlags = CameraClearFlags.SolidColor;
        gameCamera.backgroundColor = new Color(1f, 118 / 255f, 0f, 1f);
        gameCamera.orthographic = true;
        gameCamera.orthographicSize = 5;
        gameCamera.cullingMask = ~-1;// Nothing
        gameCamera.cullingMask |= 1 << 6;
        gameCamera.cullingMask |= 1 << 7;
        gameCamera.cullingMask |= 1 << 8;

        camObj.AddComponent<MoveCameraFix>();
    }

    void CreateStage()
    {
        stage = new GameObject("Stage");
        stage.transform.SetParent(battleScreen.transform);
        stage.transform.localPosition = Vector3.zero;
        stage.AddComponent<StageManager>();

        Backgrounds();
        StagePosition();
        CreateHero();
    }

    void Backgrounds()
    {
        backgrounds = new GameObject("Backgrounds");
        backgrounds.transform.SetParent(stage.transform);
        backgrounds.transform.localPosition = new Vector3(0f, 0f, 0f);
        backgrounds.transform.localScale = new Vector3(2f, 2f, 1f);

        backgrounds.AddComponent<BackgroundParallaxFix>().InsertCamObject(gameCamera);
    }

    void StagePosition()
    {
        stPos = new GameObject("StagePosition");
        UIStaticFunc.SetParentAndLayer(stPos, stage.transform, Constants.LAYER_CHA);
        stPos.transform.localPosition = new Vector3(-0.5f, -2.6f, 0);
        stPos.tag = "Stop";

        BoxCollider2D boxCollider2D = stPos.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        boxCollider2D.size = new Vector2(1f, 2f);
        boxCollider2D.offset = new Vector2(0f, 0.5f);
    }

    void CreateHero()
    {
        hero = new GameObject("Hero");
        UIStaticFunc.SetParentAndLayer(hero, stage.transform, Constants.LAYER_CHA);
        hero.transform.localPosition = new Vector3(-6f, -2.6f, 0f);
        hero.transform.localScale = new Vector3(4f, 4f, 1f);

        hero.AddComponent<ControlCharacterHero>().SetStageTransform(stPos.transform);
        
        SpriteRenderer spriteRenderer = hero.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerID = SortingLayer.NameToID("Hero");
        spriteRenderer.sortingOrder = 1;
        
        var handleAnim = Addressables.LoadAssetAsync<RuntimeAnimatorController>
            ("Assets/Animations/Hero/Hero_Animator.controller");
        
        RuntimeAnimatorController animCont = handleAnim.WaitForCompletion();
        Animator animator = hero.AddComponent<Animator>();
        animator.runtimeAnimatorController = animCont;

        hero.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        BoxCollider2D boxCollider2D = hero.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        boxCollider2D.offset = new Vector2(0f, 0.07f);
        boxCollider2D.size = new Vector2(0.34f, 0.45f);
    }

    public void InsertRenderTextureToCamera(RenderTexture texture)
        => gameCamera.targetTexture = texture;
}
