#define TEMPUSER
using UnityEngine;
using UnityEngine.AddressableAssets;


public class GameManager : MonoBehaviour
{
    //[SerializeField] private int languageSettings;  // 0 : kor, 1 : eng
    int languageSettings;

#if TEMPUSER
    public UserData userData;
#endif

    void Awake()
    {
        if (PlayerPrefs.HasKey("Language"))
            languageSettings = PlayerPrefs.GetInt("Language");
        else
            languageSettings = 0;

        PlayData playData = new PlayData();
        InjectionContainer.Regist(playData);
        
        GameData gameData = new GameData();
        gameData.GameDataLoad(languageSettings);
        InjectionContainer.Regist(gameData);
        
#if TEMPUSER
        userData = new UserData();
        InjectionContainer.Regist(userData);
        userData.userId = "유저아이디";
        userData.characterLevel = 819;
        userData.expPercent = 95.1111f;
        userData.statsPoint = 30;
        userData.beersPoint = 999999;//MAX 999999
        userData.currentAscent = 18;
        
        userData.strLv = 1052;
        userData.dexLv = 1053;
        userData.agiLv = 992;
        userData.lukLv = 1021;
        
        userData.maxAtkLv = 1950;
        userData.atkPerLv = 1977;
        userData.accuracyLv = 1900;
        userData.dodgeLv = 1911;
        userData.atkSpdLv = 331;
        userData.movSpdLv = 122;
        userData.heroSpeed = 7.5f;
#endif
        SpriteSheetManager.Load();

        Addressables.InstantiateAsync("BuiltInSpritePrefab",
            new Vector3(1000f, 1000f, 1000f), Quaternion.identity).WaitForCompletion();

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        CreateUIMenuObject uiMenuObj = new CreateUIMenuObject();
        uiMenuObj.CreateLowerUIScreen();
        
        CreateBattleObject battleObj = new CreateBattleObject();
        battleObj.CreateBattleScreen();
        battleObj.InsertRenderTextureToCamera(uiMenuObj.RenderTexture());

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ApplicationQuit.VisibleWindow();
        }
    }
}
