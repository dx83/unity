using UnityEngine;


public class CreateCharacterTab
{
    GameObject characterTabObj;

    public void Create(Transform parent)
    {
        characterTabObj = new GameObject("Tab_Character");

        UIStaticFunc.SetParentAndLayer(characterTabObj, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(characterTabObj,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        HeaderObjectInCharTab headerInCharacterTab = new HeaderObjectInCharTab();
        headerInCharacterTab.Create(characterTabObj.transform);

        MidTabObjectInCharTab midTabInCharacterTab = new MidTabObjectInCharTab();
        midTabInCharacterTab.Create(characterTabObj.transform);

        MainContentsInCharTab contentsInCharacterTab = new MainContentsInCharTab();
        contentsInCharacterTab.Create(characterTabObj.transform);

        // �̵鿡 �ǹ�ư�� ������ MainContents���� ������ ������� �ϹǷ� mainContents�� transform�� �Ű������� ����
        midTabInCharacterTab.CreateMidTabButtons(contentsInCharacterTab.RectTransform());

        characterTabObj.SetActive(false);
    }
}
