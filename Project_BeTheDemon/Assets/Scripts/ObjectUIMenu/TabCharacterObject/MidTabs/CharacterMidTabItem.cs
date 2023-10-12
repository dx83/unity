using UnityEngine;
using UnityEngine.UI;


public class CharacterMidTabItem : TabItem<CharacterMidTabData>
{
    [SerializeField] private Text titleName;
    GameObject selectionTab;

    int tabIndex;
    GameObject[] tabObject;

    public override void InitializeTab(RectTransform content, int index)
    {
        titleName = GetComponentInChildren<Text>();

        tabIndex = index;
        toggle = this.GetComponent<Toggle>();
        contentTab = content.GetChild(index).GetComponent<RectTransform>();
        selectionTab = this.transform.GetChild(1).gameObject;
    }

    public override void FillContent(CharacterMidTabData tabData)
    {
        titleName.text = tabData.tabTitleText;
        tabObject = tabData.tabObject;
    }

    public override bool ContentActiveSelf()
    {
        return contentTab.gameObject.activeSelf;
    }

    public override void ContentSetActive(bool active)
    {
        contentTab.gameObject.SetActive(active);
    }

    public override void ClickEvent(bool turn)
    {
        selectionTab.SetActive(turn);
        MidContentActive(tabIndex);
    }

    public override void TabSelectionInit()
    {
        MidContentActive(0);
    }

    public void MidContentActive(int tab)
    {
        foreach (var v in tabObject)
            v.SetActive(false);

        if (tab == 0)
        {
            tabObject[0].SetActive(true);
            tabObject[2].SetActive(true);
            tabObject[4].SetActive(true);
        }
        else if (tab == 1)
        {
            tabObject[0].SetActive(true);
            tabObject[2].SetActive(true);
            tabObject[5].SetActive(true);
        }
        else if (tab == 2)
        {
            tabObject[1].SetActive(true);
            tabObject[3].SetActive(true);
        }
        else if (tab == 3)
        {
            tabObject[1].SetActive(true);
            tabObject[3].SetActive(true);
        }
    }
}
