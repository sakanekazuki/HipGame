using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// フェードキャンバス
public class FadeIOCanvas : CanvasBase, IFadeIOCanvas
{
	static Sprite sprite = null;

	// フェード画像
	[SerializeField]
	Image fadeImg;

	// フェードの画像
	[SerializeField]
	List<Sprite> fadeImgSprites = new List<Sprite>();

	// 画像の最大サイズ
	[SerializeField]
	Vector2 maxSize = Vector2.zero;

	protected override void Start()
	{
		base.Start();
	}

	/// <summary>
	/// 初期化
	/// </summary>
	public void Initialize()
	{
		if (!sprite)
		{
			ButtocksDecide();
		}
		// 画像設定
		fadeImg.sprite = sprite;
		// フェードに使うお尻決定
		ButtocksDecide();
		// サイズ設定
		(fadeImg.transform as RectTransform).sizeDelta = maxSize;
	}

	/// <summary>
	/// フェードに使用するお尻の決定
	/// </summary>
	void ButtocksDecide()
	{
		// お尻の番号
		var number = Random.Range(0, fadeImgSprites.Count);

		// 画像が一つ以上開放されている場合
		if (GameInstance.saveData.isMakes.Contains(true))
		{
			// 開放されている番号が出るまで回す
			while (!GameInstance.saveData.isMakes[number])
			{
				// 番号を変える
				number = Random.Range(0, fadeImgSprites.Count);
			}
			// スプライトの設定
			sprite = fadeImgSprites[number];
		}
		// 画像が一つも開放されていない場合
		else
		{
			// スライム固定
			sprite = fadeImgSprites[0];
			// 画像は黒
			fadeImg.color = Color.black;
		}
	}

	/// <summary>
	/// 非同期フェードイン
	/// </summary>
	/// <returns></returns>
	async public UniTask FadeIn()
	{
		if (GameInstance.saveData.isMakes.Contains(true))
		{
			fadeImg.color = Color.white;
		}
		else
		{
			fadeImg.color = Color.black;
		}
		// キャンセレーショントークン
		var token = this.GetCancellationTokenOnDestroy();
		// RectTransfor取得
		var rect = (fadeImg.transform as RectTransform);
		// 比率を取得
		var ratio = rect.sizeDelta.y / rect.sizeDelta.x;
		while (rect.sizeDelta.x >= 0 && rect.sizeDelta.y >= 0)
		{
			// 1フレーム待つ
			await UniTask.DelayFrame(1, cancellationToken: token);
			// 小さくしていく
			rect.sizeDelta -= new Vector2(FadeManager.Instance.FadeSpeed, FadeManager.Instance.FadeSpeed * ratio);
		}
		// サイズ設定
		rect.sizeDelta = Vector2.zero;
	}

	/// <summary>
	/// 非同期フェードアウト
	/// </summary>
	/// <returns></returns>
	async public UniTask FadeOut()
	{
		if (GameInstance.saveData.isMakes.Contains(true))
		{
			fadeImg.color = Color.white;
		}
		else
		{
			fadeImg.color = Color.black;
		}

		// キャンセレーショントークン
		var token = this.GetCancellationTokenOnDestroy();

		fadeImg.sprite = sprite;
		// 比率を取得
		var ratio = fadeImg.sprite.bounds.size.y / fadeImg.sprite.bounds.size.x;
		// RectTransfor取得
		var rect = (fadeImg.transform as RectTransform);
		while (rect.sizeDelta.x <= maxSize.x || rect.sizeDelta.y <= maxSize.y)
		{
			// 1フレーム待つ
			await UniTask.DelayFrame(1, cancellationToken: token);
			// 大きくする
			rect.sizeDelta += new Vector2(FadeManager.Instance.FadeSpeed, FadeManager.Instance.FadeSpeed * ratio);
		}
		// サイズ設定
		rect.sizeDelta = maxSize;
	}
}