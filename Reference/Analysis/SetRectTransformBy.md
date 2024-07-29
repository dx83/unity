# Assets/Scripts/ObjectUIMenu/UIStaticFunc.cs
> 오브젝트의 위치와 크기를 지정하는 메서드
<br>

### SetRectTransformByNormal 정적 메서드
```csharp
public static void SetRectTransformByNormal(GameObject gameObject,
    Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
    Vector2 sizeDelta, Vector2 anchoredPos, Vector3 scale)
```
- stretch를 제외한 설정에 적용, 오브젝트 확대 기능이 있음
<br>

▼ 적용되는 RectTransform 컴포넌트
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

### SetRectTransformByPreset 정적 메서드
```csharp
public static RectTransform SetRectTransformByPreset(GameObject gameObject,
    Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
    Vector2 offsetMin, Vector2 offsetMax, Vector2 anchoredPos)
```
- Stretch에 대응한 위치 설정 기능
<br>

▼ 적용되는 RectTransform 컴포넌트
|변수|인스펙터창|설명|
|---|---|---|
|anchorMin|Anchors Min X,Y|여기에 값이 설정되면 Anchors Preset이 적용된다.|
|anchorMax|Anchors Max X,Y|여기에 값이 설정되면 Anchors Preset이 적용된다.|
|pivot|Pivot X,Y|오브젝트의 기준 좌표|
|offsetMin|아래표 참조|Stretch 적용시 값 설정|
|offsetMax|아래표 참조|Stretch 적용시 값 설정|
|anchoredPos|Pos X, Pos Y|부모 오브젝트 내에서의 좌표|
<br>

||Stretch All|Horizontal Stretch|Vertical Stretch|
|---|---|---|---|
|offsetMin|Left, Bottom|Left, PosY||
|offsetMax|Right, Top|Right, Height||
- Stretch All : anchoredPos 은 건들지 않아도 된다.
    - Right 와 Top 은 안쪽으로 들여보내는 경우 음수(-)여야 한다.
- Horizontal Stretch : PosY 와 Heigth가 상쇄되므로 PosY는 anchoredPos 으로 지정해야 한다.
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
