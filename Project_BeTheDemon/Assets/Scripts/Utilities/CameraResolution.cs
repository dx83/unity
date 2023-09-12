#pragma warning disable CS0414
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class CameraResolution : MonoBehaviour
{
    private int ScreenSizeX;
    private int ScreenSizeY;
    [SerializeField]
    private bool usingH_Pillar;

    public CanvasScaler targetScaler;

    private Camera cam;

    public void RescaleCamera(int screenWidth, int screenHeight)
    {
        if (screenWidth == ScreenSizeX && screenHeight == ScreenSizeY) return;

        float targetAspect = Constants.DEV_WIDTH / Constants.DEV_HEIGHT;
        float windowAspect = (float)screenWidth / (float)screenHeight;
        float scaleHeight = windowAspect / targetAspect;

        if (cam == null)
        {
            return;
        }

        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            usingH_Pillar = true;

            targetScaler.matchWidthOrHeight = 0;

            cam.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
            usingH_Pillar = false;

            targetScaler.matchWidthOrHeight = 1;

            cam.rect = rect;
        }

        ScreenSizeX = screenWidth;
        ScreenSizeY = screenHeight;
    }

    void Start()
    {
        ScreenSizeX = 0;
        ScreenSizeY = 0;
        cam = GetComponent<Camera>();
        cam.ResetProjectionMatrix();
    }

    void Update()
    {
        RescaleCamera(Screen.width, Screen.height);
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}

