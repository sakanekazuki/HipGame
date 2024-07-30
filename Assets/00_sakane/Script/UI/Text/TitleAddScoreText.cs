using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

// タイトルのくっつけた時に追加されるスコアを表示するテキスト
public class TitleAddScoreText : MonoBehaviour
{
	// 追加されるスコアのテキスト
	TextMeshProUGUI addScoreTxt;

	// 追加されるスコアのテキスト
	List<string> addScoreList = new List<string>()
	{
		"+1",
		"+3",
		"+6",
		"+10",
		"+15",
		"+21",
		"+28",
		"+36",
		"+45",
		"+55",
		"+66",
	};

	// アニメーター
	Animator animator;

	// 追加されるスコアの番号
	int addScoreNumber = 0;

	private void Start()
	{
		// テキスト取得
		addScoreTxt = GetComponent<TextMeshProUGUI>();
		// アニメーター取得
		animator = GetComponent<Animator>();
		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// アニメーション開始
		StartAsync(token).Forget();
	}

	/// <summary>
	/// 少し待ってアニメーション開始
	/// </summary>
	/// <param name="token">キャンセレーショントークン</param>
	/// <returns></returns>
	async UniTask StartAsync(CancellationToken token)
	{
		// フェード終了待ち
		await UniTask.WaitUntil(() => !FadeManager.Instance.IsFading, cancellationToken: token);
		// フェード後すこし待つ
		await UniTask.WaitForSeconds(0.3f, cancellationToken: token);
		// アニメーション開始
		animator.SetTrigger("Start");
	}

	/// <summary>
	/// アニメーション終了
	/// </summary>
	public void AnimationFinish()
	{
		// 次の番号のスコアにする
		++addScoreNumber;
		// スコア数を超えないようにする
		addScoreNumber %= addScoreList.Count;
		// テキストを設定
		addScoreTxt.text = addScoreList[addScoreNumber];

		// アニメーションを再度開始
		animator.SetTrigger("Start");
	}
}
