using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CameraResolution : MonoBehaviour
{


    #region Pola
    private int ScreenSizeX = 0;
    private int ScreenSizeY = 0;

    public bool usingH_Pillar;

    public CanvasScaler[] scalers;
    #endregion

    #region metody

    #region rescale camera
    private void RescaleCamera()
    {

        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;



        float targetaspect = 720f / 1280f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = GetComponent<Camera>();
        if (camera == null)
        {
            return;
        }



        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;
            usingH_Pillar = true;

            foreach (var s in scalers)
            {
                s.matchWidthOrHeight = 0;
            }


            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
            usingH_Pillar = false;
            foreach (var s in scalers)
            {
                s.matchWidthOrHeight = 1;
            }
            camera.rect = rect;
        }



        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }
    #endregion

    #endregion

    #region metody unity

    void OnPreCull()
    {
        if (Application.isEditor) return;
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;

    }

    // Use this for initialization
    void Start()
    {
        RescaleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        RescaleCamera();
    }
    #endregion
}
