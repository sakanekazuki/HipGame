using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TitleHighScoreText : MonoBehaviour
{
	// ハイスコアを表示するテキスト
	TextMeshProUGUI highScoreTxt;

	// アニメーター
	Animator animator;

	void Start()
	{
		// テキスト取得
		highScoreTxt = GetComponent<TextMeshProUGUI>();
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
		// アニメーションを再度開始
		animator.SetTrigger("Start");
	}
}
