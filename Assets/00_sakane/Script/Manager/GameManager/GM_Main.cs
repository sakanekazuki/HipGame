using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

// ゲームマネージャー
public class GM_Main : GameManagerBase, IGM_Main
{
	// ガイドラインオブジェクト
	[SerializeField]
	GameObject guideLineObjectPrefab;
	GameObject guideLineObject;

	// メインキャンバス
	[SerializeField]
	GameObject mainCanvasPrefab;
	GameObject mainCanvas;

	// オプションキャンバス
	[SerializeField]
	GameObject optionCanvasPrefab;
	GameObject optionCanvas;

	// リザルトキャンバス
	[SerializeField]
	GameObject resultCanvasPrefab;
	GameObject resultCanvas;

	// 前までのスコア
	float beforeScore = 0;
	// スコア
	float score = 0;
	public float Score
	{
		get => score;
	}
	// スコアを追加していく速度
	[SerializeField]
	float addScoreSpeed = 0.08f;
	// 最高スコア
	int highScore = 0;
	public int HighScore
	{
		get => highScore;
	}

	// true = ハイスコア更新
	bool isNewHighScore = false;
	public bool IsNewHighScore
	{
		get => isNewHighScore;
	}

	// コンボ数
	int comboValue = 0;
	// コンボによる倍率
	List<float> magnifications = new List<float>()
	{
		1.0f,
		1.1f,
		1.2f,
		1.3f,
		1.5f
	};

	// 揺らした回数
	int shakeValue = 0;
	// 揺らすことのできる最大数
	[SerializeField]
	int maxShakeValue = 5;
	public int MaxShakeValue
	{
		get => maxShakeValue;
	}

	// シャッフル時のSE
	[SerializeField]
	AudioClip shakeClip;

	// ゲーム開始音
	[SerializeField]
	AudioClip gameStartClip;

	// ゲームオーバーのBGM
	[SerializeField]
	AudioClip gameOverClip;

	// true = ゲームオーバー
	public bool IsGameOver
	{
		get;
		private set;
	}

	// メインキャンバスのインターフェイス
	IMainCanvas iMainCanvas;

	private void Awake()
	{
		IsGameOver = false;
		// ガイドライン的なもの生成
		guideLineObject = Instantiate(guideLineObjectPrefab);

		// メインキャンバス生成
		mainCanvas = Instantiate(mainCanvasPrefab);
		// メインキャンバス表示
		mainCanvas.SetActive(true);
		// インターフェイス取得
		iMainCanvas = mainCanvas.GetComponent<IMainCanvas>();

		// オプションキャンバス生成
		optionCanvas = Instantiate(optionCanvasPrefab);
		// オプションキャンバス非表示
		optionCanvas.SetActive(false);

		// リザルトキャンバス生成
		resultCanvas = Instantiate(resultCanvasPrefab);
		// リザルトキャンバス非表示
		resultCanvas.SetActive(false);
	}

	protected override void Start()
	{
		base.Start();

		// 保存されているデータを入れる
		highScore = GameInstance.saveData.highScore;
		// スコアテキスト初期化
		iMainCanvas.SetScoreText("0");
		// ハイスコア設定
		iMainCanvas.SetHighScoreText(highScore.ToString());
		// コンボ数設定
		iMainCanvas.SetComboValue(0);
		// 倍率を初期化
		iMainCanvas.SetComboMagnification(1);
		// シャッフル回数設定
		iMainCanvas.SetShakeValue(maxShakeValue);
		// ゲーム開始音再生
		SoundManager.Instance.SEPlay(gameStartClip);
	}

	protected override void Update()
	{
		base.Update();
		if (Keyboard.current.escapeKey.wasPressedThisFrame)
		{
#if UNITY_EDITOR

			UnityEditor.EditorApplication.isPlaying = false;

#endif
		}
	}

	/// <summary>
	/// スコアを少しづつ追加していく
	/// </summary>
	/// <param name="token">キャンセレーショントークン</param>
	/// <returns></returns>
	async UniTask AddScoreAsync(CancellationToken token)
	{
		// 進捗
		var progress = 0.0f;
		while (true)
		{
			// 進めた分のスコアを取得
			var s = Mathf.Lerp(beforeScore, score, progress);

			// 切り捨て
			int dispScore = Mathf.FloorToInt(s);
			// スコア表示
			iMainCanvas.SetScoreText(dispScore.ToString());
			// 1フレーム待つ
			await UniTask.DelayFrame(1, cancellationToken: token);
			// 進める
			progress += addScoreSpeed;
			// 表示しているスコアが現在のスコア以上になった場合出る
			if (s >= Mathf.FloorToInt(score))
			{
				//切り捨て
				var sc = Mathf.FloorToInt(score);
				// 切り捨てたものを表示
				iMainCanvas.SetScoreText(sc.ToString());
				// 追加するテキスト非表示
				iMainCanvas.AddScoreTxtHide();
				break;
			}
		}
	}

	/// <summary>
	/// プレイヤーの状態設定
	/// </summary>
	/// <param name="isActive">設定するプレイヤーの状態</param>
	public void SetPlayerState(bool isActive)
	{
		character.SetActive(isActive);
	}

	/// <summary>
	/// ガイドラインの位置設定
	/// </summary>
	/// <param name="position">位置</param>
	public void SetGuideLinePosition(Vector2 position)
	{
		guideLineObject.transform.position = position;
	}

	/// <summary>
	/// ガイドライン非表示
	/// </summary>
	public void GuideLineHide()
	{
		guideLineObject.SetActive(false);
	}

	/// <summary>
	/// ガイドライン表示
	/// </summary>
	public void GuideLineShow()
	{
		guideLineObject.SetActive(true);
	}

	/// <summary>
	/// オプションを開く
	/// </summary>
	public void OptionOpen()
	{
		optionCanvas.SetActive(true);
		// キャラクターオフ
		SetPlayerState(false);
	}

	/// <summary>
	/// オプションを閉じる
	/// </summary>
	public void OptionClose()
	{
		optionCanvas.SetActive(false);
		// プレイヤーキャラクターをオン
		SetPlayerState(true);
	}

	/// <summary>
	/// シャッフル
	/// </summary>
	public void Shake()
	{
		// 最大数を超えると揺らせない
		if (shakeValue >= maxShakeValue)
		{
			return;
		}
		// 揺らす
		Stage.Instance.Shake();
		// SEを鳴らす
		SoundManager.Instance.SEPlay(shakeClip);
		// 揺らした回数を追加
		++shakeValue;

		// 揺らした回数設定
		iMainCanvas.SetShakeValue(maxShakeValue - shakeValue);
	}

	/// <summary>
	/// ゲームオーバー
	/// </summary>
	public void GameOver()
	{
		IsGameOver = true;
		// スコア
		if (score > highScore)
		{
			// ハイスコア更新
			highScore = Mathf.FloorToInt(score);
			// ハイスコアを記録
			GameInstance.saveData.highScore = highScore;
			// ハイスコア更新フラグtrue
			isNewHighScore = true;
		}
		else
		{
			// ハイスコア更新フラグfalse
			isNewHighScore = false;
		}
		// オプションを開いている場合非表示にする
		optionCanvas.SetActive(false);
		// コレクションも非表示
		CollectionManager.Instance.CollectionCanvasClose();
		// リザルト表示
		resultCanvas.SetActive(true);
		// BGM変更
		SoundManager.Instance.BGMChange(gameOverClip).Forget();
	}

	/// <summary>
	/// スコアを追加する
	/// </summary>
	/// <param name="score">追加するスコア</param>
	public void AddScore(int score)
	{
		// 前までのスコアを設定
		beforeScore = this.score;
		// floatで計算するために代入
		float addscore = score;

		if (comboValue > magnifications.Count - 1)
		{
			comboValue = magnifications.Count - 1;
		}
		// コンボで追加
		addscore *= magnifications[comboValue];
		// スコアを足す
		this.score += addscore;
		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// スコアを少しづつ追加する
		AddScoreAsync(token).Forget();
		// 切り捨て
		int ads = Mathf.FloorToInt(addscore);
		// 追加するスコア数を表示
		iMainCanvas.SetAddScoreTxt(ads.ToString());
	}

	/// <summary>
	/// 追加されるスコアの表示
	/// </summary>
	/// <param name="score">追加されるスコア</param>
	/// <param name="worldPosition">位置</param>
	public void AddScoreNextTxt(int score, Vector3 worldPosition)
	{
		iMainCanvas.AddScoreNextTxt(score.ToString(), worldPosition);
	}

	/// <summary>
	/// コンボ数取得
	/// </summary>
	public int GetComboValue()
	{
		return comboValue;
	}

	/// <summary>
	/// コンボ数リセット
	/// </summary>
	public void ComboReset()
	{
		comboValue = 0;
		// コンボ数リセット
		iMainCanvas.SetComboValue(comboValue);
		// コンボ倍率をリセット
		iMainCanvas.SetComboMagnification(1);
	}

	/// <summary>
	/// コンボ数追加
	/// </summary>
	public void AddComboValue()
	{
		++comboValue;
		// コンボ数表示
		iMainCanvas.SetComboValue(comboValue);
	}

	/// <summary>
	/// コンボ倍率設定
	/// </summary>
	public void SetComboMagnification()
	{
		iMainCanvas.SetComboMagnification(magnifications[comboValue]);
	}

	/// <summary>
	/// 次に落とすお尻の画像設定
	/// </summary>
	/// <param name="sprite">設定する画像</param>
	public void SetNextButtocksImage(Sprite sprite)
	{
		iMainCanvas.SetNextButtocksImage(sprite);
	}

	/// <summary>
	/// 操作説明画像設定
	/// </summary>
	public void SetControlSprite()
	{
		iMainCanvas.SetControlSprite();
	}

	/// <summary>
	/// タイムリミット設定
	/// </summary>
	/// <param name="time">時間</param>
	public void SetTimeLimit(float time)
	{
		if (!ButtocksManager.Instance.CanDrop)
		{
			return;
		}
		iMainCanvas.SetTimeLimit(time);
	}

	/// <summary>
	/// 時間制限リセット
	/// </summary>
	public void TimeLimitReset()
	{
		iMainCanvas.TimeLimitReset();
	}
}