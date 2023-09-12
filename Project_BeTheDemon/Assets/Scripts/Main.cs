using UnityEngine;

public class Main : MonoBehaviour
{
    // Only this Script is attached to Main Camera.
    private void Awake()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.cullingMask = ~-1;// Nothing
        Camera.main.cullingMask |= 1 << 0;
        Camera.main.cullingMask |= 1 << 1;
        Camera.main.cullingMask |= 1 << 2;
        Camera.main.cullingMask |= 1 << 4;
        Camera.main.cullingMask |= 1 << 5;

        GameObject obj = new GameObject("GameManager");
        obj.AddComponent<InjectionContainer>();
        obj.AddComponent<GameManager>();
    }
}
