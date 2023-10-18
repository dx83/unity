using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class CreateUIMenuObject
{
    // LowerUIScreen
    RenderTexture renderTexture;
    public GameObject gameCanvas;
    GameObject lowerMenuBase;
    GameObject lowerContents;


    public void CreateLowerUIScreen()
    {
        SetCameraProjectionMatrix();
        //canvas
        CreateCanvas();
        CreateEventSystem();
        // battle screen
        CreateUpperScreen();
        //ui layout
        CreateLowerMenuBase();
        CreateLowerContents();
        //lower menu tab -> all menu created
        CreateLowerMenuTab lowerMenuTab = new CreateLowerMenuTab(lowerMenuBase, lowerContents);
        lowerMenuTab.Create();
        //app quit window
        ApplicationQuit quitWindow = new ApplicationQuit();
        quitWindow.Create(gameCanvas.transform);
    }

    void SetCameraProjectionMatrix()// DEVSIZE 720 1280
    {
        Camera.main.projectionMatrix = new Matrix4x4
        (
            new Vector4(0.35556f, 0.00000f, 0.00000f, 0.00000f),
            new Vector4(0.00000f, 0.20000f, 0.00000f, 0.00000f),
            new Vector4(0.00000f, 0.00000f, -0.00200f, 1.00060f),
            new Vector4(0.00000f, 0.00000f, 0.00000f, 1.00000f)
        );
    }

    void CreateCanvas()
    {
        gameCanvas = new GameObject("Canvas");
        gameCanvas.layer = LayerMask.NameToLayer(Constants.LAYER_UI);

        Canvas canvas = gameCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        CanvasScaler canvasScaler = gameCanvas.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(Constants.DEV_WIDTH, Constants.DEV_HEIGHT);

        gameCanvas.AddComponent<GraphicRaycaster>();

        CameraResolution cameraResolution = Camera.main.gameObject.AddComponent<CameraResolution>();
        cameraResolution.targetScaler = canvasScaler;
    }

    void CreateEventSystem()
    {
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    void CreateUpperScreen()
    {
        GameObject upperObject = new GameObject("BattleScreen");
        UIStaticFunc.SetParentAndLayer(upperObject, gameCanvas.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(upperObject,
            new Vector2(0f, 0.5f), Vector2.one, new Vector2(0.5f, 0.5f));

        GameObject battleScreen = new GameObject("Raw Image");
        UIStaticFunc.SetParentAndLayer(battleScreen, upperObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(battleScreen,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        battleScreen.AddComponent<CanvasRenderer>();

        RawImage rawImage = battleScreen.AddComponent<RawImage>();
        
        AsyncOperationHandle<RenderTexture> textureHandle = 
            Addressables.LoadAssetAsync<RenderTexture>("Assets/Material/RenderTexture.renderTexture");
        renderTexture = textureHandle.WaitForCompletion();
        rawImage.texture = renderTexture;
        
        rawImage.color = Color.white;
        
        AsyncOperationHandle<Material> materialHandle = 
            Addressables.LoadAssetAsync<Material>("Assets/Material/RenderTextureMaterial.mat");
        rawImage.material = materialHandle.WaitForCompletion();
    }

    void CreateLowerMenuBase()
    {
        lowerMenuBase = new GameObject("LowerMenu");
        UIStaticFunc.SetParentAndLayer(lowerMenuBase, gameCanvas.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(lowerMenuBase, 
            Vector2.zero, new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f));

        lowerMenuBase.AddComponent<CanvasRenderer>();
        
        Image image = lowerMenuBase.AddComponent<Image>();
        image.color = new Color(32 / 255f, 42 / 255f, 61 / 255f, 1f);
    }

    void CreateLowerContents()
    {
        lowerContents = new GameObject("LowerContents");
        UIStaticFunc.SetParentAndLayer(lowerContents, lowerMenuBase.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(lowerContents, 
            new Vector2(0f, 0.15f), Vector2.one, new Vector2(0.5f, 0.5f));
    }

    public RenderTexture RenderTexture() => renderTexture;
}
