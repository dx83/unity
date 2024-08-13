# PlayerPrefs
> 유니티에서 제공해주는 데이터 관리 클래스
<br>

### 목적
- 언어를 설정하고 게임을 재시작하면 언어가 변경되도록 언어 설정만 저장하기 위해 임시로 사용
<br>

### 주의사항
- 유니티 상에서 임의로 조작이 불가능하므로 레지스트리 편집기를 이용해야 한다.
- `regedit` => `HKEY_CURRENT_USER` => `SOFTWARE` => `Unity` => `UnityEditor` => `DefaultCompany`  => `PrductName`
  - `DefaultCompany` 와 `PrductName`은 `ProjectSettings`에서 볼수 있음
<br>
- PlayerPrefs의 이름이 같은 경우 원하는 저장 데이터를 불러올 수 없는 경우가 생기므로 이름이 중복되지 않도록 한다.
