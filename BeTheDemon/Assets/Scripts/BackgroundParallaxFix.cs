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
	[SerializeField] private float parallaxScale;                 // ����� �̵���Ű�� ī�޶� �������� ����
	[SerializeField] private float[] parallaxFactor;              // �� ���� ���̾��� ������ �󸶳� ������.
	[SerializeField] private float smoothing;                     // ���� ȿ���� �󸶳� �Ų������� �ϴ���.

	[SerializeField] private Camera cam;
	private Vector3 previousCamPos;             // ���� ī�޶� ��ġ
	private Vector3 spriteSize;                 // ��� �̹��� ũ��

	[Inject] PlayData pd = new PlayData();
	[Inject] UserData ud = new UserData();
	[Inject] GameData gd = new GameData();
	InjectionObj injectionObj = new InjectionObj();

	void Start()
	{
		injectionObj.Inject(this);
		gd.ExcelDataFunc(1);

		previousCamPos = cam.transform.position;          // ���� �����ӿ� ���� ī�޶� ��ġ�� ����

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
		// ������ ī�޶� �������� �ݴ��̱� ������ ���� �����ӿ� ������ŭ ���� ��
		float parallax = (cam.transform.position.x - previousCamPos.x) * parallaxScale;  // a - b : b -> a 

		// �� ���� ��渶��...
		for (int i = 0; i < bg.Count; i++)
		{
			Transform bgTrans = bg[i].background;

			// ��� ����� x ��ġ�� ������ ��������ŭ ���� ���� ���� ��ġ�� ���� ��
			float backgroundTargetPosX = bgTrans.position.x + parallax * (parallaxFactor[i] + 1);
			// ��� ����� ���ο� ��ġ��
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, bgTrans.position.y, bgTrans.position.z);
			// ����� ���ο� ��ġ�� ���������� �̵�
			bgTrans.position = Vector3.Lerp(bgTrans.position, backgroundTargetPos, smoothing * Time.deltaTime);
			BackGroundScrolling(parallax, i);
		}

		previousCamPos = cam.transform.position;  // ���� �����ӿ� ���� ī�޶� ��ġ�� ����
		
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
			// �ش� ����� �����ʿ� �������� x - width/2 �������� üũ�ؼ� �������� �ű��
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
			// �ش� ����� ���ʿ� �������� x + width/2 �������� üũ�ؼ� ���������� �ű��
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
