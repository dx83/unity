## Assets/Scripts/ObjectUIMenu/Functions/TabBase.cs, TabController.cs, TabItem.cs

### 목적
- 지정한 UI 오브젝트내에 동일한 크기의 탭 버튼을 생성하는 스크립트
<br>

### 사용 기술
- 프로퍼티 : 자기 오브젝트의 RectTransform 캐싱하기
- 상속 : proteced, virtual, override
- 인터페이스 : IPointerDownHandler, IPointerUpHandler (유니티 내장, 터치 감지)
- [HideInInspector] : public 변수 인스펙터창에서 숨기기
<br>

### 스크립트 설명
- TabBase.cs
  - 탭 기능 최상위 클래스
  - 자기 오브젝트의 RectTransform을 캐싱하는 프로퍼티만 있음

- TabController.cs
  - 

- TabItem.cs

<br>
