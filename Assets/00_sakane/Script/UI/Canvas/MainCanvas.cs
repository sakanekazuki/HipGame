using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

// メインキャンバス
public class MainCanvas : CanvasBase, IMainCanvas
{
	// スコアテキスト
	[SerializeField]
	TextMeshProUGUI scoreTxt;
	// ハイスコアのテキスト
	[SerializeField]
	TextMeshProUGUI highScoreTxt;

	// コンボ数を表示するテキスト
	[SerializeField]
	TextMeshProUGUI comboValueTxt;
	// 追加されるスコアのテキスト
	[SerializeField]
	TextMeshProUGUI addScoreTxt;
	// お尻の隣に表示する追加されるスコアのテキスト
	[SerializeField]
	GameObject addScoreNextTxt;

	// コンボ倍率を表示するテキスト
	[SerializeField]
	TextMeshProUGUI comboMagnificationTxt;

	// 揺らした回数を表示するテキスト
	[SerializeField]
	TextMeshProUGUI shakeValueTxt;

	// 日本語のときのテキストのいち
	[SerializeField]
	GameObject shakeValueTxt_jp;
	// 英語のときのテキストのいち
	[SerializeField]
	GameObject shakeValueTxt_en;

	// 次に落とすお尻
	[SerializeField]
	Image nextButtocksImg;

	// 操作説明画像の位置
	[SerializeField]
	GameObject controlImgPosition_jp;
	[SerializeField]
	GameObject controlImgPosition_en;
	// 操作説明画像
	[SerializeField]
	Image controlImg;

	// 操作説明画像
	[SerializeField]
	List<Sprite> controlSprites = new List<Sprite>();

	// 時間制限を表示するテキスト
	[SerializeField]
	TextMeshProUGUI timeLimitTxt;
	// オフセット
	[SerializeField]
	Vector3 addScoreNextTxtOffset = Vector3.zero;

	// ゲームマネージャーのインターフェイス
	IGM_Main iGM_Main;

	protected override void Start()
	{
		base.Start();
		// ゲームマネージャーのインターフェイス取得
		iGM_Main = GM_Main.Instance.GetComponent<IGM_Main>();
		// 操作説明画像設定
		SetControlSprite();
	}

	/// <summary>
	/// オプションボタンをクリック
	/// </summary>
	public void OptionClick()
	{
		iGM_Main.OptionOpen();
	}

	/// <summary>
	/// コレクションボタンをクリック
	/// </summary>
	public void CollectionClick()
	{
		// コレクションを開く
		CollectionManager.Instance.CollectionCanvasOpen();
	}

	/// <summary>
	/// スコアテキスト設定
	/// </summary>
	/// <param name="scoreText">表示するスコア</param>
	public void SetScoreText(string score)
	{
		scoreTxt.text = score;
	}

	/// <summary>
	/// 追加されるスコア設定
	/// </summary>
	/// <param name="addScore">追加されるスコア</param>
	public void SetAddScoreTxt(string addScore)
	{
		addScoreTxt.text = "+" + addScore;
	}

	/// <summary>
	/// 追加されるスコアを隠す
	/// </summary>
	public void AddScoreTxtHide()
	{
		addScoreTxt.text = "";
	}

	/// <summary>
	/// 追加されるスコア設定
	/// </summary>
	/// <param name="addScore">追加されるスコア</param>
	/// <param name="worldPosition">位置</param>
	public void AddScoreNextTxt(string addScore, Vector3 worldPosition)
	{
		// テキスト生成
		var obj = Instantiate(addScoreNextTxt, gameObject.transform);
		// テキスト
		obj.GetComponent<TextMeshProUGUI>().text = "+" + addScore;
		// 位置指定
		obj.transform.position = worldPosition + addScoreNextTxtOffset;
		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// 隠す
		AddScoreNextTxtHide(token, obj).Forget();
	}

	/// <summary>
	/// 追加されるスコアを隠す
	/// </summary>
	/// <param name="token">キャンセレーショントークン</param>
	/// <param name="obj">作成したオブジェクト</param>
	public async UniTask AddScoreNextTxtHide(CancellationToken token, GameObject obj)
	{
		// テキスト取得
		var textMeshPro = obj.GetComponent<TextMeshProUGUI>();
		while (true)
		{
			// 1フレーム待つ
			await UniTask.DelayFrame(1);
			// 1秒かけて透明にしていく
			textMeshPro.color -= new Color(0, 0, 0, 1.0f / 60.0f);
			// 透明になったら出る
			if (textMeshPro.color.a <= 0)
			{
				break;
			}
		}
		// オブジェクト削除
		Destroy(obj);
		obj = null;
	}

	/// <summary>
	/// ハイスコア設定
	/// </summary>
	/// <param name="highScore">表示するハイスコア</param>
	public void SetHighScoreText(string highScore)
	{
		highScoreTxt.text = highScore;
	}

	/// <summary>
	/// コンボ数設定
	/// </summary>
	/// <param name="value">コンボ数</param>
	public void SetComboValue(int value)
	{
		comboValueTxt.text = value.ToString();
	}

	/// <summary>
	/// コンボ倍率設定
	/// </summary>
	/// <param name="magnification">コンボ倍率</param>
	public void SetComboMagnification(float magnification)
	{
		comboMagnificationTxt.text = "×" + magnification.ToString();
	}

	/// <summary>
	/// 揺らした回数設定
	/// </summary>
	/// <param name="shakeValue">揺らした回数</param>
	public void SetShakeValue(int shakeValue)
	{
		shakeValueTxt.text = shakeValue.ToString();
	}

	/// <summary>
	/// 次に落とすお尻の画像設定
	/// </summary>
	/// <param name="sprite">設定する画像</param>
	public void SetNextButtocksImage(Sprite sprite)
	{
		nextButtocksImg.sprite = sprite;
	}

	/// <summary>
	/// 操作説明画像設定
	/// </summary>
	public void SetControlSprite()
	{
		// 画像取得
		var sprite = controlSprites[(int)GameInstance.saveData.language];
		// 画像サイズ取得
		var spriteSize = sprite.bounds.size * 100;
		// 画像サイズ設定
		controlImg.rectTransform.sizeDelta = spriteSize;
		// 画像設定
		controlImg.sprite = sprite;

		// 言語によって位置を変える
		if (GameInstance.saveData.language == LanguageManager.LanguageType.Japanese)
		{
			// シャッフル回数を表示する位置設定
			shakeValueTxt.transform.position = shakeValueTxt_jp.transform.position;
			// 操作説明画像の位置設定
			controlImg.transform.position = controlImgPosition_jp.transform.position;
		}
		else
		{
			// シャッフル回数を表示する位置設定
			shakeValueTxt.transform.position = shakeValueTxt_en.transform.position;
			// 操作説明画像の位置設定
			controlImg.transform.position = controlImgPosition_en.transform.position;
		}
	}

	/// <summary>
	/// 時間制限表示
	/// </summary>
	/// <param name="time">時間</param>
	public void SetTimeLimit(float time)
	{
		// 切り上げ
		var t = Mathf.CeilToInt(time);
		// テキストに設定
		timeLimitTxt.text = t.ToString();
	}

	/// <summary>
	/// 時間制限リセット
	/// </summary>
	public void TimeLimitReset()
	{
		timeLimitTxt.text = "";
	}
}