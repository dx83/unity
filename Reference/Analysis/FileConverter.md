# Assets/Scripts/Unity_Editor/FileConverter.cs

### 목적
- 게임 리소스에 해당되는 엑셀 파일을 난독화시켜 게임 파일에 첨부
<br>

### 이유
- 게임중 엑셀 파일을 직접 읽는 것은 게임 성능 저하를 가져올 수 있다.
- 단순히 json 파일로 만들면 해당 파일을 누구나 쉽게 열어 볼 수 있어 보안에 취약하다.
<br>

### 방법
- 엑셀 파일을 유니티로 읽어온 뒤    
1. `JsonConvert.SerializeObject(대상)` : 클래스를 json 문자열로 변환
2. `Encoding.UTF8.GetBytes(문자열)` : 문자열을 byte 배열로 변환(UTF8 인코딩), 바이너리 데이타 생성
3. `Convert.ToBase64String(바이너리 데이타)` : 바이너리 데이타를 아스키 문자열로 표현하는 인코딩 방식
4. `File.WriteAllText(저장 경로, 문자열)` : 최종 인코딩 데이타를 파일로 생성 `ex) Data000.txt 생성`
<br>

- 생성된 데이타 파일 게임 중 로드 (역순으로)
1. `Resources.Load<TextAsset>(데이타 파일).text` : Resources 폴더에 있는 데이타 파일 불러오기
2. `Convert.FromBase64String(문자열)` : base64로 인코딩 데이타를 바이너리 데이타로 변환
3. `Encoding.UTF8.GetString바이너리 데이타)` : byte 배열을 다시 문자열로 변환
4. `JsonConvert.DeserializeObject(문자열)` : json 문자열을 클래스로 변환
<br>

```
직렬화는 객체를 파일의 형태 등으로 저장하거나,
통신하기 쉬운 포맷으로 변환하는 과정을 의미한다.
```
