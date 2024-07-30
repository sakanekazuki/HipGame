using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// オプションキャンバス
public class OptionCanvas : CanvasBase
{
	// ゲームマネージャーのインターフェイス
	IGM_Main iGM_Main;
	IGM_Title iGM_Title;

	// 音量設定のスライダー
	[SerializeField]
	Slider masterSlider;
	[SerializeField]
	Slider bgmSlider;
	[SerializeField]
	Slider seSlider;

	// 現在の言語を表示するテキスト
	[SerializeField]
	TMPro.TextMeshProUGUI languageTxt;

	protected override void Start()
	{
		base.Start();
		// ゲームマネージャーのインターフェイス取得
		iGM_Main = GM_Main.Instance?.GetComponent<IGM_Main>();
		// タイトルマネージャーのインターフェイス取得
		iGM_Title = GM_Title.Instance?.GetComponent<IGM_Title>();

		// 音量スライダーの設定
		masterSlider.value = GameInstance.saveData.masterVolume;
		bgmSlider.value = GameInstance.saveData.bgmVolume;
		seSlider.value = GameInstance.saveData.seVolume;

		// ベタがきの言語対応（関数化しろwww）
		switch (GameInstance.saveData.language)
		{
		case LanguageManager.LanguageType.Japanese:
			languageTxt.text = "Japanese";
			break;
		case LanguageManager.LanguageType.English:
			languageTxt.text = "English";
			break;
		}
	}

	/// <summary>
	/// オプションを閉じる
	/// </summary>
	public void OptionClose()
	{
		iGM_Main?.OptionClose();
		iGM_Title?.OptionClose();
	}

	/// <summary>
	/// マスターボリューム
	/// </summary>
	/// <param name="volume">音量</param>
	public void MaterVolume(float volume)
	{
		SoundManager.Instance.MasterVolume = volume;
	}

	/// <summary>
	/// BGMの音量設定
	/// </summary>
	/// <param name="volume">設定する音量</param>
	public void BGMVolume(float volume)
	{
		SoundManager.Instance.BGMVolume = volume;
	}

	/// <summary>
	/// SEの音量設定
	/// </summary>
	/// <param name="volume">設定する音量</param>
	public void SEVolume(float volume)
	{
		SoundManager.Instance.SEVolume = volume;
	}

	/// <summary>
	/// 言語設定
	/// </summary>
	/// <param name="isNext">次かどうか</param>
	public void LanguageSetting(bool isNext)
	{
		if (isNext)
		{
			// 次の番号の言語に
			LanguageManager.Language =
				(LanguageManager.LanguageType)(((int)LanguageManager.Language + 1) % ((int)LanguageManager.LanguageType.English + 1));
		}
		else
		{
			// 前の番号の言語にする
			LanguageManager.Language =
				(LanguageManager.LanguageType)(((int)LanguageManager.Language - 1) % ((int)LanguageManager.LanguageType.English + 1));
			// 前の言語が無い場合最後の言語に設定
			if ((int)LanguageManager.Language == -1)
			{
				LanguageManager.Language = LanguageManager.LanguageType.English;
			}
		}

		// 今回はベタがきの言語対応
		switch (LanguageManager.Language)
		{
		case LanguageManager.LanguageType.Japanese:
			languageTxt.text = "Japanese";
			break;
		case LanguageManager.LanguageType.English:
			languageTxt.text = "English";
			break;
		}
		// 画像切り替え
		iGM_Main?.SetControlSprite();
	}

	/// <summary>
	/// リトライボタン
	/// </summary>
	async public void Retry()
	{
		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// シーン移動禁止
		LevelManager.canLevelMove = false;
		// シーン移動
		LevelManager.OpenLevel(token, "MainScene").Forget();
		// フェード終了を待つ
		await FadeManager.Instance.FadeOutAsync();
		// シーン読み込み終了待ち
		await UniTask.WaitUntil(() => !LevelManager.IsLevelLoading, cancellationToken: token);
		// シーン移動可能状態にする
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// タイトルに戻る
	/// </summary>
	async public void ToTitle()
	{
		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// シーン移動禁止
		LevelManager.canLevelMove = false;
		// シーン移動
		LevelManager.OpenLevel(token, "TitleScene").Forget();
		// フェード終了を待つ
		await FadeManager.Instance.FadeOutAsync();
		// シーン読み込み終了待ち
		await UniTask.WaitUntil(() => !LevelManager.IsLevelLoading, cancellationToken: token);
		// シーン移動可能状態にする
		LevelManager.canLevelMove = true;
	}
}