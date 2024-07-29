# Assets/Scripts/ObjectUIMenu/UIStaticFunc.cs

### SetRectTransformByNormal 정적 메서드
```csharp
public static void SetRectTransformByNormal(GameObject gameObject,
    Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
    Vector2 sizeDelta, Vector2 anchoredPos, Vector3 scale)
```
<br>

- 적용되는 RectTransform 컴포넌트

|변수|인스펙터창|설명|
|---|---|---|
|anchorMin|Anchors Min X,Y|여기에 값이 설정되면 Anchors Preset이 적용된다.|
|anchorMax|Anchors Max X,Y|여기에 값이 설정되면 Anchors Preset이 적용된다.|
|pivot|Pivot X,Y|오브젝트의 기준 좌표|
|sizeDelta|Width, Height|오브젝트의 가로,세로 크기|
|anchoredPos|Pos X, Pos Y|부모 오브젝트 내에서의 좌표|
|scale|Scale X,Y,Z|오브젝트의 크기를 해당 수치만큼 곱함|
<br>

▼ Anchors Preset
||||
|---|---|---|
|TopLeft|TopCenter|TopRight|
|anchorMin (0, 1)<br>anchorMax (0, 1)|anchorMin (0.5f, 1)<br>anchorMax (0.5f, 1)|anchorMin (1, 1)<br>anchorMax (1, 1)|
|MiddleLeft|MiddleCenter|MiddleRight|
|anchorMin (0, 0.5f)<br>anchorMax (0, 0.5f)|anchorMin (0.5f, 0.5f)<br>anchorMax (0.5f, 0.5f)|anchorMin (1, 0.5f)<br>anchorMax (1, 0.5f)|
|BottomLeft|BottomCenter|BottomRight|
|anchorMin (0, 0)<br>anchorMax (0, 0)|anchorMin (0.5f, 0)<br>anchorMax (0.5f, 0)|anchorMin (1, 0)<br>anchorMax (1, 0)|
<br>

▼ 수평 Stretch
||
|---|
|Horizontal Stretch Top|
|anchorMin (0, 1)<br>anchorMax (1, 1)|
|Horizontal Stretch Middle|
|anchorMin (0, 0.5f)<br>anchorMax (1, 0.5f)|
|Horizontal Stretch Bottom|
|anchorMin (0, 0)<br>anchorMax (1, 0)|
<br>

▼ 수직 Stretch
||||
|---|---|---|
|Vertical Stretch Left|Vertical Stretch Center|Vertical Stretch Right|
|anchorMin (0, 0)<br>anchorMax (0, 1)|anchorMin (0.5f, 0)<br>anchorMax (0.5f, 1)|anchorMin (1, 0)<br>anchorMax (1, 1)|
<br>

▼ StretchAll
||
|---|
|anchorMin (0, 0)<br>anchorMax (1, 1)|
<br>

### SetRectTransformByPreset 정적 메서드
- 
<br>
