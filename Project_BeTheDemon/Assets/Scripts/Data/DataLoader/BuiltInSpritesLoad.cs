using UnityEngine;
using UnityEngine.UI;


public class BuiltInSpritesLoad : MonoBehaviour
{
    public static Sprite UI_SKIN_BACKGROUND;
    public static Sprite UI_SKIN_CHECKMARK;
    public static Sprite UI_SKIN_DROPDOWN;
    public static Sprite UI_SKIN_INPUTFIELD;
    public static Sprite UI_SKIN_KNOB;
    public static Sprite UI_SKIN_UIMASK;
    public static Sprite UI_SKIN_UISPRITE;

    void Awake()
    {
        UI_SKIN_BACKGROUND = transform.GetChild(0).GetComponent<Image>().sprite;
        UI_SKIN_CHECKMARK = transform.GetChild(1).GetComponent<Image>().sprite;
        UI_SKIN_DROPDOWN = transform.GetChild(2).GetComponent<Image>().sprite;
        UI_SKIN_INPUTFIELD = transform.GetChild(3).GetComponent<Image>().sprite;
        UI_SKIN_KNOB = transform.GetChild(4).GetComponent<Image>().sprite;
        UI_SKIN_UIMASK = transform.GetChild(5).GetComponent<Image>().sprite;
        UI_SKIN_UISPRITE = transform.GetChild(6).GetComponent<Image>().sprite;

        DontDestroyOnLoad(this);
    }
}
