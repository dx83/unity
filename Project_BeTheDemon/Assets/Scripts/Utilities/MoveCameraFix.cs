using UnityEngine;

public class MoveCameraFix : MonoBehaviour
{
    [Inject] PlayData pd = new PlayData();
    InjectionObj injectionObj = new InjectionObj();
    
    void Start()
    {
        injectionObj.Inject(this);
        pd.cameraSpeed = 0.0f;
    }

    void Update()
    {
        this.transform.Translate(pd.cameraSpeed * Time.deltaTime, 0, 0);
    }
}
