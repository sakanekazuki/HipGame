using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// 追加されるスコアを示す画像
public class TitleAddScoreImg : MonoBehaviour
{
	// 追加されるスコアを示す画像
	Image addScoreImg;

	// アニメーター
	Animator animator;

	// 画像の番号
	int number = 0;

	void Start()
	{
		// テキスト取得
		addScoreImg = GetComponent<Image>();
		// アニメーター取得
		animator = GetComponent<Animator>();

		// 開放されている場合白
		if (GameInstance.saveData.isMakes[0])
		{
			addScoreImg.color = Color.white;
		}
		// 開放されていない場合黒
		else
		{
			addScoreImg.color = Color.black;
		}

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
		// 番号を進める
		++number;
		// 画像数を超えないようにする
		number %= (GM_Title.Instance as GM_Title).ButtocksSprites.Count;
		// 画像設定
		addScoreImg.sprite = (GM_Title.Instance as GM_Title).ButtocksSprites[number];
		// 開放されている場合白
		if (GameInstance.saveData.isMakes[number])
		{
			addScoreImg.color = Color.white;
		}
		// 開放されていない場合黒
		else
		{
			addScoreImg.color = Color.black;
		}
		// アニメーションを再度開始
		animator.SetTrigger("Start");
	}
}
