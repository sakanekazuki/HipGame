using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// コレクションキャンバス
public class CollectionCanvas : CanvasBase
{
	// お尻コレクション画像
	[SerializeField]
	List<Image> buttocks = new List<Image>();

	// 普通の画像
	[SerializeField]
	List<Sprite> normalSprites_en = new List<Sprite>();
	// 黒い画像
	[SerializeField]
	List<Sprite> blackSprites_en = new List<Sprite>();

	// 普通の画像
	[SerializeField]
	List<Sprite> normalSprites_jp = new List<Sprite>();
	// 黒い画像
	[SerializeField]
	List<Sprite> blackSprites_jp = new List<Sprite>();

	// 大きく表示するコレクションの画像
	[SerializeField]
	Image bigCollectionImg;

	protected override void Start()
	{
		base.Start();

		BigCollectionHide();
	}

	private void OnEnable()
	{
		// 名前配列取得
		var names = GameInstance.saveData.language == LanguageManager.LanguageType.Japanese ?
			CollectionManager.Instance.names_jp : CollectionManager.Instance.names_en;

		for (int i = 0; i < buttocks.Count; ++i)
		{
			if (GameInstance.saveData.language == LanguageManager.LanguageType.English)
			{
				buttocks[i].sprite = GameInstance.saveData.isMakes[i] ? normalSprites_en[i] : blackSprites_en[i];
			}
			else
			{
				buttocks[i].sprite = GameInstance.saveData.isMakes[i] ? normalSprites_jp[i] : blackSprites_jp[i];
			}
		}
	}

	/// <summary>
	/// コレクションを閉じる
	/// </summary>
	public void CollectionClose()
	{
		// 閉じる
		CollectionManager.Instance.CollectionCanvasClose();
	}

	/// <summary>
	/// 大きく表示するコレクションの画像設定
	/// </summary>
	/// <param name="sprite">設定する画像</param>
	public void SetBigCollectionSprite(Sprite sprite)
	{
		bigCollectionImg.sprite = sprite;
	}

	/// <summary>
	/// 大きく表示するコレクションの画像を表示する
	/// </summary>
	public void BigCollectionDisplay()
	{
		bigCollectionImg.gameObject.SetActive(true);
	}

	/// <summary>
	/// 大きいカード表示のアニメーション
	/// </summary>
	/// <param name="token">キャンセレーショントークン</param>
	/// <returns></returns>
	public async UniTask BigCollectionDisplayAsync(CancellationToken token)
	{
		await UniTask.DelayFrame(1, cancellationToken: token);
	}

	/// <summary>
	/// 大きく表示するコレクションの画像を非表示にする
	/// </summary>
	public void BigCollectionHide()
	{
		bigCollectionImg.gameObject.SetActive(false);
	}

	/// <summary>
	/// 大きいカード非表示のアニメーション
	/// </summary>
	/// <param name="token">キャンセレーショントークン</param>
	/// <returns></returns>
	public async UniTask BigCollectionHideAsync(CancellationToken token)
	{
		await UniTask.DelayFrame(1, cancellationToken: token);
	}
}
