# Assets/Scripts/ObjectUIMenu/UIStaticFunc.cs
> 오브젝트의 위치와 크기를 지정하는 메서드
<br>

## 앵커 프리셋이 적용되는 오브젝트에 사용
```csharp
public static RectTransform SetRectTransformByPreset(GameObject gameObject,
    AnchorsPreset anchors, float width, float height, float posX, float posY,
    Vector2 pivot, Vector3 scale)
```
<br>

|변수|인스펙터창|설명|
|---|---|---|
|anchors|anchorMin<br>anchorMax|앵커 설정|
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
- min, max의 값이 동일
<br>

