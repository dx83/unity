using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabController<T> : TabBase
{
    [SerializeField] private bool AllowSwitchOff;// true(Create Faketab) : false(allways toggle 1)
    [SerializeField] private RectTransform ContentTabMenu;

    [SerializeField] private GameObject BaseItem;
    [SerializeField] protected int tabAmount;
    [SerializeField] private RectOffset padding;
    [SerializeField] private float spacing;

    [SerializeField] protected float tabMorphWidth = 1.0f;
    [SerializeField] protected float tabMorphHeight = 0.95f;

    protected float tabSpaceTotal, tabWidth, tabHeight;
    private int totalTabs;

    protected List<T> tabItemData;
    private List<TabItem<T>> tabs = new List<TabItem<T>>();

    protected virtual void Start()
    {
        totalTabs = AllowSwitchOff ? tabAmount + 1 : tabAmount;// all toggle off : allways toggle 1

        BaseItem.SetActive(false);
        tabSpaceTotal = padding.left + padding.right + (spacing * (tabAmount - 1));
        tabWidth = (CachedRect.rect.width - tabSpaceTotal) / tabAmount;
        tabHeight = CachedRect.rect.height - padding.top - padding.bottom;

        CreateTabItems();
    }

    void CreateTabItems()
    {
        for (int i = 0; i < totalTabs; i++)
        {
            GameObject obj = Instantiate(BaseItem, CachedRect);
            obj.SetActive(true);

            if (i == tabAmount) // create a fakeTab
            {
                RectTransform rt = obj.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(0.0f, 0.0f);
                rt.anchoredPosition = new Vector2(-1000.0f, -0.0f);

                TabItem<T> tab = obj.GetComponent<TabItem<T>>();
                tab.fakeTab = true;
                tab.InitializeTab(ContentTabMenu, i);
                tabs.Add(tab);
            }
            else
            {
                TabItem<T> tab = obj.GetComponent<TabItem<T>>();
                tab.fakeTab = false;
                tab.originSize = new Vector2(tabWidth, tabHeight);
                tab.morphSize = new Vector2(tabMorphWidth, tabMorphHeight);
                tab.InitializeTab(ContentTabMenu, i);
                tab.FillContent(tabItemData[i]);

                RectTransform rt = obj.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(tabWidth, tabHeight);

                rt.anchorMin = new Vector2(0.0f, 0.0f);
                rt.anchorMax = new Vector2(0.0f, 0.0f);
                rt.pivot = new Vector2(0.0f, 0.0f);

                tabSpaceTotal = padding.left + (spacing * i);
                rt.anchoredPosition = new Vector2((tabWidth * i) + tabSpaceTotal, padding.bottom);

                tabs.Add(tab);
            }
        }

        for (int i = 0; i < totalTabs; i++)
        {
            bool init = AllowSwitchOff ? (i == tabAmount) : (i == 0);
            tabs[i].toggle.isOn = init;
            tabs[i].ContentSetActive(init);
            tabs[i].toggle.onValueChanged.AddListener(ToggleChanged);
        }

        int index = AllowSwitchOff ? tabAmount : 0;
        SelectTab(tabs[index]);
    }

    void ToggleChanged(bool b)
    {
        if (b)
        {
            int index = 0;
            foreach (var tg in tabs)
            {
                if (tg.toggle.isOn)
                {
                    SelectTab(tg);
                    break;
                }
                index++;
            }
        }
    }

    void SelectTab(TabItem<T> tg)
    {
        // idle contents (Using fakeToggleTab)
        if (tg.fakeTab)
        {
            for (int i = 0; i < tabAmount; i++)
            {
                tg.ClickEvent(false);
                tg.ContentSetActive(false);
            }

            tg.toggle.isOn = true;
            tg.ContentSetActive(true);
            return;
        }
        // same index click => idle contents active
        if (AllowSwitchOff && tg.ContentActiveSelf())
        {
            tg.ClickEvent(false);
            tg.ContentSetActive(false);

            tabs[tabAmount].toggle.isOn = true;
            tabs[tabAmount].ContentSetActive(true);
            return;
        }
        // basic active
        for (int i = 0; i < tabAmount; i++)
        {
            tabs[i].ClickEvent(false);
            tabs[i].ContentSetActive(false);
        }
        tg.ClickEvent(true);
        tg.ContentSetActive(true);

        if (AllowSwitchOff)
            tabs[tabAmount].ContentSetActive(false);
    }

    protected void OnDisable()
    {
        if (tabs.Count < 1) return;

        for (int i = 0; i < tabAmount; i++)
            tabs[i].ContentSetActive(false);

        int index = AllowSwitchOff ? tabAmount : 0;
        SelectTab(tabs[index]);
    }

    protected virtual void TabSelectionInit(TabItem<T> tab) { }
}
