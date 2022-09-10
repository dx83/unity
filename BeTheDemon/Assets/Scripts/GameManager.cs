using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int languageSettings;  // 0 : kor, 1 : eng

#if UNITY_EDITOR
    public UserData userData;
#endif

    void Awake()
    {
        PlayData playData = new PlayData();
        InjectionContainer.Regist(playData);

        GameData gameData = new GameData();
        gameData.GameDataLoad(languageSettings);
        InjectionContainer.Regist(gameData);

        ExcelLoader el = new ExcelLoader();
        el.ExcelLoadFunc(languageSettings);
        InjectionContainer.Regist(el);

#if UNITY_EDITOR
        InjectionContainer.Regist(userData);
        //userData.userId = "유저아이디";
        //userData.characterLevel = 819;
        //userData.expPercent = 95.1111f;
        //userData.statsPoint = 30;
        //userData.beersPoint = 999;//MAX 999999
        //userData.currentAscent = 18;
        //
        //userData.strLv = 1052;
        //userData.dexLv = 1053;
        //userData.agiLv = 992;
        //userData.lukLv = 1021;
        //
        //userData.maxAtkLv = 1950;
        //userData.atkPerLv = 1977;
        //userData.accuracyLv = 1900;
        //userData.dodgeLv = 1911;
        //userData.atkSpdLv = 331;
        //userData.movSpdLv = 122;
#endif

        SpriteSheetManager.Load();

        DontDestroyOnLoad(this);
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 5; i++)
                EventStatic.CharacterMidTextUpdate(i);
            
            EventStatic.BtnActiveByStatPt(userData.statsPoint);
            EventStatic.BtnActiveByBeerPt(userData.beersPoint);
        }
    }
#endif
}
