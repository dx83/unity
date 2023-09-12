using UnityEngine;

public class AppResize : MonoBehaviour
{
    // devSize.x        : width     devSize.y       : height
    // viewport.size.x  : width     viewport.size.y : height
    // screen.x         : width     screen.y        : height
    public Vector2 devSize; 
    public Rect viewport;
    public Vector2 screen;


    void Awake()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.cullingMask = ~-1;// Nothing
        Camera.main.cullingMask |= 1 << 0;
        Camera.main.cullingMask |= 1 << 1;
        Camera.main.cullingMask |= 1 << 2;
        Camera.main.cullingMask |= 1 << 4;
        Camera.main.cullingMask |= 1 << 5;

        devSize.x = Constants.DEV_WIDTH;
        devSize.y = Constants.DEV_HEIGHT;
    }
    void Update()
    {
        ScreenResize(Screen.width, Screen.height);
    }

    void ScreenResize(int width, int height)
    {
#if Origin
        float rx = width / devSize.x;
        float ry = height / devSize.y;

        if (rx < ry)
        {
            viewport.x = 0;
            viewport.size.Set(width, viewport.size.y);

            viewport.size.Set(viewport.size.x, devSize.y * rx);
            viewport.y = (height - viewport.size.y) / 2;
        }
        else
        {
            viewport.y = 0;
            viewport.size.Set(viewport.size.x, height);

            viewport.size.Set(devSize.x * ry, viewport.size.y);
            viewport.x = (width - viewport.size.x) / 2;
        }

        Camera.main.rect = new Rect(viewport.x / width, viewport.y / height,
            viewport.size.x / width, viewport.size.y / height);
#else
        float rx = width / devSize.x;
        float ry = height / devSize.y;

        if (rx < ry)
        {
            viewport.x = 0;
            viewport.size.Set(width, viewport.size.y);

            viewport.size.Set(viewport.size.x, devSize.y * rx);
            viewport.y = (height - viewport.size.y) / 2;
        }
        else
        {
            viewport.y = 0;
            viewport.size.Set(viewport.size.x, height);

            viewport.size.Set(devSize.x * ry, viewport.size.y);
            viewport.x = (width - viewport.size.x) / 2;
        }

        Camera.main.rect = new Rect(viewport.x / width, viewport.y / height,
            viewport.size.x / width, viewport.size.y / height);
        Camera.main.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
#endif
#if UNITY_EDITOR_
        Debug.Log($"devSize({devSize.x},{devSize.y}), real({width}, {height})");
        Debug.Log($"viewport({viewport.x},{viewport.y},{viewport.size.x},{viewport.size.y})");
#endif
    }
}
