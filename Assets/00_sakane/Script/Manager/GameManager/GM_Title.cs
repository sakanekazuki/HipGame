using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

// タイトル管理クラス
public class GM_Title : GameManagerBase, IGM_Title
{
	// タイトルキャンバス
	[SerializeField]
	GameObject titleCanvasPrefab;
	GameObject titleCanvas;
	ITitleCanvas iTitleCanvas;

	// オプションキャンバス
	[SerializeField]
	GameObject optionCanvasPrefab;
	GameObject optionCanvas;

	// クレジットキャンバス
	[SerializeField]
	GameObject creditCanvasPrefab;
	GameObject creditCanvas;

	// お尻画像
	[SerializeField]
	List<Sprite> buttocksSprites = new List<Sprite>();
	public List<Sprite> ButtocksSprites
	{
		get => buttocksSprites;
	}

	protected override void Start()
	{
		base.Start();
		// タイトルキャンバス生成
		titleCanvas = Instantiate(titleCanvasPrefab);
		// タイトルキャンバス表示
		titleCanvas.SetActive(true);
		// タイトルキャンバスのインターフェイス取得
		iTitleCanvas = titleCanvas.GetComponent<ITitleCanvas>();

		// オプションキャンバス生成
		optionCanvas = Instantiate(optionCanvasPrefab);
		// オプションキャンバス非表示
		optionCanvas.SetActive(false);

		// クレジットキャンバス生成
		creditCanvas = Instantiate(creditCanvasPrefab);
		// クレジット画面非表示
		creditCanvas.SetActive(false);
	}

	/// <summary>
	/// ゲーム開始
	/// </summary>
	async public UniTask GameStart()
	{
		// 移動できない状態にする
		LevelManager.canLevelMove = false;
		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// シーン読み込み
		LevelManager.OpenLevel(token, "MainScene").Forget();
		// フェードアウト
		await FadeManager.Instance.FadeOutAsync();
		// 読み込み終了まで待つ
		await UniTask.WaitUntil(() => !LevelManager.IsLevelLoading);
		// フェード後移動可能にする
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// タイトル有効化
	/// </summary>
	public void TitleEnable()
	{
		iTitleCanvas.TitleEnable();
	}

	/// <summary>
	/// オプションを開く
	/// </summary>
	public void OptionOpen()
	{
		optionCanvas.SetActive(true);
	}

	/// <summary>
	/// オプションを閉じる
	/// </summary>
	public void OptionClose()
	{
		optionCanvas.SetActive(false);
		TitleEnable();
	}

	/// <summary>
	/// クレジット画面表示
	/// </summary>
	public void CreditOpen()
	{
		creditCanvas.SetActive(true);
	}
}