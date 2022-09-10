using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(ScrollRect))]
public class ScrollController<T> : ScrollBase
{
    [SerializeField] private RectOffset padding;
    [SerializeField] private float spacing;
    [SerializeField] private float itemLength;

    private ScrollRect cachedScroll;
    public ScrollRect CachedScroll
    {
        get
        {
            if (cachedScroll == null)
                cachedScroll = this.GetComponent<ScrollRect>();
            return cachedScroll;
        }
    }

    protected int scrollItemMax;
    protected List<T> scrollItemData;

    [SerializeField] private GameObject baseItem;
    private LinkedList<ScrollItem<T>> cells = new LinkedList<ScrollItem<T>>();

    protected virtual void Start()
    {
        CachedRect.GetComponent<Mask>().enabled = true;
        CachedScroll.content = this.transform.GetChild(0).GetComponent<RectTransform>();
        CachedScroll.horizontal = false;
        CachedScroll.vertical = true;
        CachedScroll.movementType = ScrollRect.MovementType.Elastic;

        cachedScroll.onValueChanged.AddListener(OnScrollPosChanged);

        baseItem.SetActive(false);

        CreateItem();
    }

    void CreateItem()
    {
        CachedScroll.content.anchorMin = new Vector2(0.0f, 1.0f);
        CachedScroll.content.anchorMax = new Vector2(0.0f, 1.0f);
        CachedScroll.content.pivot = new Vector2(0.0f, 1.0f);

        float height = itemLength + spacing;
        CachedScroll.content.sizeDelta = new Vector2(
            CachedRect.rect.width,
            (itemLength + spacing) * scrollItemMax - spacing + padding.top + padding.bottom);

        int max = 1;
        float loc = 0.0f;
        while (spacing + loc < CachedRect.rect.height)
        {
            max++;
            loc += itemLength + spacing;
        }

        for (int i = 0; i < max; i++)
        {
            GameObject obj = Instantiate(baseItem, CachedScroll.content);
            obj.SetActive(true);
            
            ScrollItem<T> cell = obj.GetComponent<ScrollItem<T>>();
            cell.CachedRect.anchorMin = new Vector2(0.0f, 1.0f);
            cell.CachedRect.anchorMax = new Vector2(0.0f, 1.0f);
            cell.CachedRect.pivot = new Vector2(0.0f, 1.0f);

            cell.CachedRect.sizeDelta = new Vector2(CachedRect.rect.width - padding.left - padding.right, itemLength);
            cell.CachedRect.anchoredPosition = new Vector2(padding.left, -((cell.CachedRect.sizeDelta.y * i) + padding.top + (spacing * i)));

            cell.InitializeItem(i);
            cell.itemLength = itemLength;
            cell.UpdateContent(scrollItemData[i]);
            
            cells.AddLast(cell);
        }
    }

    void ReuseCells(int direction)
    {
        //-CachedScroll.content.anchoredPosition.y : 스크롤에 따라 실시간 변동
        if (direction > 0)
        {
            LinkedListNode<ScrollItem<T>> first = cells.First;

            while (first.Value.bottom > -CachedScroll.content.anchoredPosition.y)
            {
                LinkedListNode<ScrollItem<T>> last = cells.Last;
                first.Value.gameObject.SetActive(false);

                // last cell next↓ top coordinate
                float nextY = last.Value.bottom - spacing;
                int nextIdx = last.Value.dataIndex + 1;
                // 다음 셀의 위치는 scroll content 범위 내에 있어야 함
                if (nextIdx < scrollItemMax && nextY > -CachedScroll.content.sizeDelta.y)
                {
                    first.Value.top = nextY;
                    //first.Value.bottom = nextY - itemLength;

                    first.Value.CachedRect.anchoredPosition = new Vector2(
                        first.Value.CachedRect.anchoredPosition.x, nextY);

                    first.Value.dataIndex = nextIdx;
                    first.Value.UpdateContent(scrollItemData[nextIdx]);

                    cells.RemoveFirst();
                    cells.AddLast(first);
                    first = cells.First;
                }
                else
                    break;
            }
        }
        else if (direction < 0)
        {
            LinkedListNode<ScrollItem<T>> last = cells.Last;

            while (last.Value.top < -CachedScroll.content.anchoredPosition.y - CachedRect.rect.height)
            {
                LinkedListNode<ScrollItem<T>> first = cells.First;
                last.Value.gameObject.SetActive(false);

                // first cell next↑ bottom coordinate
                float nextBt = first.Value.top + spacing;
                int nextIdx = first.Value.dataIndex - 1;
                // 다음 셀의 위치는 scroll content 범위 내에 있어야 함
                if (nextIdx > -1 && nextBt < 0.0f)
                {
                    //last.Value.bottom = nextBt;
                    last.Value.top = nextBt + itemLength;

                    last.Value.CachedRect.anchoredPosition = new Vector2(
                        last.Value.CachedRect.anchoredPosition.x, last.Value.top);

                    last.Value.dataIndex = nextIdx;
                    last.Value.UpdateContent(scrollItemData[nextIdx]);

                    cells.RemoveLast();
                    cells.AddFirst(last);
                    last = cells.Last;
                }
                else
                    break;
            }
        }

        ScrollItemsActiveFunc();
    }

    void ScrollItemsActiveFunc()
    {
        foreach (var node in cells)
        {
            if (node.top < -CachedScroll.content.anchoredPosition.y &&
                node.top > -CachedScroll.content.anchoredPosition.y - CachedRect.rect.height)
                node.gameObject.SetActive(true);

            if (node.bottom < -CachedScroll.content.anchoredPosition.y &&
                node.bottom > -CachedScroll.content.anchoredPosition.y - CachedRect.rect.height)
                node.gameObject.SetActive(true);
        }
    }

    Vector2 prevScrollPos;
    public void OnScrollPosChanged(Vector2 scrollPos)
    {
        ReuseCells((scrollPos.y < prevScrollPos.y) ? 1 : -1); // 1:↓ -1:↑
        prevScrollPos = scrollPos;
    }

    protected void UpdateItemDataInScroll()
    {
        foreach (var node in cells)
            node.UpdateContent(scrollItemData[node.dataIndex]);
    }

    protected virtual void OnDisable()
    {
        if (scrollItemData == null) return;

        ScrollItemsSortInit();
    }

    void ScrollItemsSortInit()
    {
        CachedScroll.content.anchoredPosition = new Vector2(0.0f, 0.0f);

        LinkedListNode<ScrollItem<T>> first = cells.First;

        for (int i = 0; i < cells.Count; i++)
        {
            first.Value.CachedRect.anchoredPosition = new Vector2(
                        first.Value.CachedRect.anchoredPosition.x, ((-itemLength - spacing) * i) - padding.top);
            first.Value.dataIndex = i;
            first.Value.UpdateContent(scrollItemData[i]);

            cells.RemoveFirst();
            cells.AddLast(first);
            first = cells.First;
        }

        ScrollItemsActiveFunc();
    }

    protected void ScrollItemsToLocate(int itemIndex)
    {   // top 기준
        float loc = 0.0f;
        if (itemIndex == 0)
        {
            loc = 0;
        }
        else if (itemIndex == Constants.ASCENT_MAXIDX)
        {
            loc = (padding.top + (itemLength + spacing) * itemIndex)
                    - CachedRect.rect.height + itemLength + spacing;
        }
        else
        {
            loc = (padding.top + (itemLength + spacing) * itemIndex)    // 해당 아이템까지의 top 위치
                    - CachedRect.rect.height / 2 + itemLength / 2;      // 해당 UI 화면 중앙 위치
        }
        
        CachedScroll.content.anchoredPosition = new Vector2(0.0f, loc);
        ReuseCells(1);
    }
}
