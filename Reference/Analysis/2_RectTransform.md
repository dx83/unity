# Assets/Scripts/ObjectUIMenu/UIStaticFunc.cs
> 오브젝트의 위치와 크기를 지정하는 메서드
<br>

## 앵커 프리셋이 적용되는 오브젝트에 사용
```csharp
public static RectTransform SetRectTransformByPreset(GameObject gameObject,
    AnchorsPreset anchors, float width, float height, float posX, float posY,
    Vector2 pivot, Vector3 scale)
```

|변수|인스펙터창|설명|
|---|---|---|
|anchors|anchorMin<br>anchorMax|앵커 설정|
|pivot|Pivot X,Y|오브젝트의 기준 좌표|
|sizeDelta|Width, Height|오브젝트의 가로,세로 크기|
|posX, posY<br>(anchoredPos)|Pos X, Pos Y|부모 오브젝트 내에서의 좌표|
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
- min, max의 값이 동일
<br>

## 스트레치가 적용되는 오브젝트에 사용
- Stretch 모드는 해상도가 바뀌어도 자동으로 크기 조절이 되도록 설정된다.
||Stretch All|Horizontal Stretch|Vertical Stretch|
|---|---|---|---|
|offsetMin|Left, Bottom|Left, PosY|PosX, Bottom|
|offsetMax|Right, Top|Right, Height|Width, Top|
- Stretch All : anchoredPos 은 건들지 않아도 된다.
    - Right 와 Top 은 안쪽으로 들여보내는 경우 음수(-)여야 한다.
- Horizontal Stretch : PosY 와 Heigth가 상쇄되므로 PosY는 anchoredPos 으로 지정해야 한다.
<br>

```csharp
public static RectTransform SetRectTransformByStretchAll(GameObject gameObject,
    float left, float right, float top, float bottom,
    Vector2 pivot, Vector2 anchoredPos, Vector3 scale)
```

|Stretch All|
|---|
|anchorMin (0, 0)<br>anchorMax (1, 1)|
<br>

```csharp
public static RectTransform SetRectTransformByStretchHor(GameObject gameObject,
    StretchHor stretch, float left, float right, float posY, float height,
    Vector2 pivot, Vector3 scale)
```

|Horizontal Stretch||
|---|---|
|Top|anchorMin (0, 1)<br>anchorMax (1, 1)|
|Middle|anchorMin (0, 0.5f)<br>anchorMax (1, 0.5f)|
|Bottom|anchorMin (0, 0)<br>anchorMax (1, 0)|
<br>

```csharp
public static RectTransform SetRectTransformByStretchVer(GameObject gameObject,
    StretchVer stretch, float top, float bottom, float posX, float width,
    Vector2 pivot, Vector3 scale)
```

|Vertical Stretch|||
|---|---|---|
|Left|Center|Right|
|anchorMin (0, 0)<br>anchorMax (0, 1)|anchorMin (0.5f, 0)<br>anchorMax (0.5f, 1)|anchorMin (1, 0)<br>anchorMax (1, 1)|
<br>

## 앵커 임의 지정하는 오브젝트에 사용
```csharp
public static RectTransform SetRectTransformByStretch(GameObject gameObject,
    float lowerLeftX, float lowerLeftY, float upperRightX, float upperRightY,
    float left, float right, float topH, float bottomW,
    Vector2 pivot, float posX, float posY, Vector3 scale)
```
- 앵커 설정시 가장 유사한 stretch에 따라서 horizontal / vertical 이 정해질 수 있음
- topH, bottomW
    - horizontal : `topH에 height, bottomW에 0 설정` 또는 `topH에 0, bottomW에 -height 설정`
    - vertical : `bottomW에 width, topH에 0 설정` 또는 `bottomW에 0, topH에 -width 설정`
- posX, posY
    - horizontal : posY 설정, posX에 0 설정
    - vertical : posX 설정, posY에 0 설정
