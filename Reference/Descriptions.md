SpriteAtlas : Addressables의 스프라이트아틀라스를 이용한 이미지 처리
****

- 외부 코드
  - SpriteSheetManager : 이미지 아틀라스 불러와서 각 이미지를 리스트에 넣어주는 코드
  - ExcelConnector : 엑셀파일 읽어서 클래스 프로퍼티에 넣어주는 코드
  - CameraResolution : 해상도 변화에 따라 실시간으로 비율 고정시켜주는 코드 (화면에서 남는 부분은 검은 기둥이 생기도록 함)

- 자가 코드
  - FileConverter : 게임 리소스에 해당되는 엑셀 파일을 난독화시켜 게임에 첨부할 데이타 파일 생성하는 기능
  - EventBus, EventList : 이벤트버스, 이벤트를 관리하는 가장 간단한 패턴
  - TabBase, TabController, TabItem : 탭 기능 구현

****
<br>
