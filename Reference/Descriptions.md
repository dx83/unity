SpriteAtlas : Addressables의 스프라이트아틀라스를 이용한 이미지 처리

- 외부 코드
  - SpriteSheetManager : 이미지 아틀라스 불러와서 각 이미지를 리스트에 넣어주는 코드
  - ExcelConnector : 엑셀파일 읽어서 클래스 프로퍼티에 넣어주는 코드
  - CameraResolution : 해상도 변화에 따라 실시간으로 비율 고정시켜주는 코드 (화면에서 남는 부분은 검은 기둥이 생기도록 함)

- 자가 코드
  - FileConverter : 엑셀파일을 유니티로 읽은 뒤 Json으로 변환하고 다시 byte로 인코딩하고 base64로 난독화된 txt 파일로 바꾸는 코드



CreateQuitWindow
