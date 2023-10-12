using UnityEngine;


public class MainContentsInCharTab
{
    GameObject mainContentsObject;

    public RectTransform RectTransform() => mainContentsObject.GetComponent<RectTransform>();

    public void Create(Transform parent)
    {
        mainContentsObject = new GameObject("MainContents");
        UIStaticFunc.SetParentAndLayer(mainContentsObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(mainContentsObject,
            Vector2.zero, new Vector2(1f, 0.6f), new Vector2(0.5f, 0.5f));

        CharacterMainAttributeContents characterMainAttributeContents = new CharacterMainAttributeContents();
        characterMainAttributeContents.Create(mainContentsObject.transform);
        
        CharacterMainAdvanceContents characterMainAdvanceContents = new CharacterMainAdvanceContents();
        characterMainAdvanceContents.Create(mainContentsObject.transform);
        
        CharacterMainAscentContents characterMainAscentContents = new CharacterMainAscentContents();
        characterMainAscentContents.Create(mainContentsObject.transform);
        
        CharacterMainAbilityContents characterMainAbilityContents = new CharacterMainAbilityContents();
        characterMainAbilityContents.Create(mainContentsObject.transform);
    }
}
