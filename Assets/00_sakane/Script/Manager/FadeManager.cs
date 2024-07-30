using Cysharp.Threading.Tasks;
using UnityEngine;

// フェード管理クラス
public class FadeManager : ManagerBase<FadeManager>
{
	// フェードを行うキャンバス
	[SerializeField]
	GameObject fadeIOCanvasPrefab;
	GameObject fadeIOCanvas;
	// フェードするキャンバスのインターフェイス
	IFadeIOCanvas iFadeIOCanvas;

	// フェード速度
	[SerializeField]
	float fadeSpeed = 0.08f;
	public float FadeSpeed
	{
		get => fadeSpeed;
	}

	// true = フェード中
	bool isFading = false;
	public bool IsFading
	{
		get => isFading;
	}

	private void Awake()
	{
		// フェードキャンバスプレハブ生成
		fadeIOCanvas = Instantiate(fadeIOCanvasPrefab);
		// フェードキャンバスのインターフェイス取得
		iFadeIOCanvas = fadeIOCanvas.GetComponent<IFadeIOCanvas>();
		// 初期化
		iFadeIOCanvas.Initialize();
	}

	private void Start()
	{
		// フェードイン
		FadeIn();
	}

	/// <summary>
	/// フェードイン
	/// </summary>
	[ContextMenu("fadein")]
	public void FadeIn()
	{
		FadeInAsync().Forget();
	}

	/// <summary>
	/// フェードアウト
	/// </summary>
	[ContextMenu("fadeout")]
	public void FadeOut()
	{
		FadeOutAsync().Forget();
	}

	/// <summary>
	/// フェードイン
	/// </summary>
	async public UniTask FadeInAsync()
	{
		fadeIOCanvas.SetActive(true);
		// フェード中状態にする
		isFading = true;
		// フェードイン
		await iFadeIOCanvas.FadeIn();
		// フェード終了状態にする
		isFading = false;

		fadeIOCanvas.SetActive(false);
	}

	/// <summary>
	/// フェードアウト
	/// </summary>
	async public UniTask FadeOutAsync()
	{
		fadeIOCanvas.SetActive(true);
		// フェード中状態にする
		isFading = true;
		// フェードアウト開始
		await iFadeIOCanvas.FadeOut();
		// フェード終了状態にする
		isFading = false;
	}
}
