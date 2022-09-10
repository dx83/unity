using System.Collections.Generic;
using UnityEngine;


public class BackGroundInfo
{
    public Transform background;
    
    public bool left;
    public bool right { get => !left; }
    
    public int pair;
}

public class BackgroundParallaxFix : MonoBehaviour
{
	List<BackGroundInfo> bg = new List<BackGroundInfo>();

	[SerializeField] private int backgroundCnt;
	[SerializeField] private float parallaxScale;                 // 배경을 이동시키는 카메라 움직임의 비율
	[SerializeField] private float[] parallaxFactor;              // 각 연속 레이어의 시차를 얼마나 줄일지.
	[SerializeField] private float smoothing;                     // 시차 효과가 얼마나 매끄러워햐 하는지.

	[SerializeField] private Camera cam;
	private Vector3 previousCamPos;             // 이전 카메라 위치
	private Vector3 spriteSize;                 // 배경 이미지 크기

	[Inject] PlayData pd = new PlayData();
	[Inject] UserData ud = new UserData();
	[Inject] GameData gd = new GameData();
	InjectionObj injectionObj = new InjectionObj();

	void Start()
	{
		injectionObj.Inject(this);
		gd.ExcelDataFunc(1);

		previousCamPos = cam.transform.position;          // 이전 프레임에 현재 카메라 위치를 저장

		backgroundCnt = 5;
		int max = backgroundCnt * 2;
		parallaxFactor = new float[max];

		for (int i = 0, idx = 0; i < max; i++, idx++)
        {
			if (idx == backgroundCnt) idx = 0;

			GameObject obj = new GameObject($"BG{i}");
			obj.layer = 6;

			SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
			sr.sprite = SpriteSheetManager.GetSpriteByName(gd.UIimageName(idx));
			sr.sortingLayerName = "Backgrounds";
			sr.sortingOrder = idx;
			if (i == 0) spriteSize = new Vector3(sr.bounds.size.x, 0.0f, 0.0f);
			
			obj.transform.SetParent(this.transform);

			obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			obj.transform.localPosition = new Vector3((i < backgroundCnt ? -1 : 1) * sr.bounds.size.x / 4, 0, 0);

			parallaxFactor[i] = idx;

			bg.Add(new BackGroundInfo
			{
				background = obj.transform,
				left = i < backgroundCnt,
				pair = (i < backgroundCnt ? i + backgroundCnt : i - backgroundCnt)
			});
		}
	}


	void Update()
	{
		// 시차는 카메라 움직임의 반대이기 때문에 이전 프레임에 비율만큼 곱해 줌
		float parallax = (cam.transform.position.x - previousCamPos.x) * parallaxScale;  // a - b : b -> a 

		// 각 연속 배경마다...
		for (int i = 0; i < bg.Count; i++)
		{
			Transform bgTrans = bg[i].background;

			// 대상 배경의 x 위치는 시차에 감소율만큼 곱한 값을 현재 위치에 더한 값
			float backgroundTargetPosX = bgTrans.position.x + parallax * (parallaxFactor[i] + 1);
			// 대상 배경의 새로운 위치값
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, bgTrans.position.y, bgTrans.position.z);
			// 배경을 새로운 위치로 보간법으로 이동
			bgTrans.position = Vector3.Lerp(bgTrans.position, backgroundTargetPos, smoothing * Time.deltaTime);
			BackGroundScrolling(parallax, i);
		}

		previousCamPos = cam.transform.position;  // 이전 프레임에 현재 카메라 위치를 저장
		
		if (pd.changeState == InGameState.End)
			pd.stageEndPosX = cam.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x;

		if (pd.inGameState != InGameState.Start && 
			pd.inGameState != InGameState.End &&
			pd.heroPosX >= cam.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, 0.0f)).x)
			pd.cameraSpeed = ud.heroSpeed + 0.2f;
		else
			pd.cameraSpeed = 0.0f;
	}

	void BackGroundScrolling(float direction, int index)
	{
		Transform bgTrans = bg[index].background;
		
		if (direction < 0.0f)       // <-
		{
			// 해당 배경이 오른쪽에 있을때만 x - width/2 지점에서 체크해서 왼쪽으로 옮기기
			if (bg[index].right)
			{
				Vector3 posLeft = cam.WorldToViewportPoint(bgTrans.position - spriteSize);
				
				if (posLeft.x > 1.0f)
				{
					bgTrans.position = new Vector3(bgTrans.position.x - (spriteSize.x * 4),
												   bgTrans.position.y, bgTrans.position.z);
					bg[index].left = true;
					bg[bg[index].pair].left = false;
				}
			}
		}
		else if (direction > 0.0f)  // ->
		{
			// 해당 배경이 왼쪽에 있을때만 x + width/2 지점에서 체크해서 오른쪽으로 옮기기
			if (bg[index].left)
			{
				Vector3 posRight = cam.WorldToViewportPoint(bgTrans.position + spriteSize);
				
				if (posRight.x < 0.0f)
				{
					bgTrans.position = new Vector3(bgTrans.position.x + (spriteSize.x * 4),
												   bgTrans.position.y, bgTrans.position.z);
					bg[index].left = false;
					bg[bg[index].pair].left = true;
				}
			}
		}
	}
}
