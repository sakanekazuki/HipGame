using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime;

// コンボ倍率テキスト
public class ComboRatioTxt : MonoBehaviour
{
	// コンボテキスト
	TextMeshProUGUI comboTxt;

	// コンボの内容
	List<string> comboTxtContent = new List<string>()
	{
		"1Combo\n×1.0",
		"2Combo\n×1.1",
		"3Combo\n×1.2",
		"4Combo\n×1.3",
		"5Combo\n×1.5"
	};

	// コンボの番号
	int comboNumber = 0;

	// アニメーター
	Animator animator;

	private void Start()
	{
		// テキスト取得
		comboTxt = GetComponent<TextMeshProUGUI>();
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
		// 次のコンボにする
		++comboNumber;
		// コンボ数を超えないようにする
		comboNumber %= comboTxtContent.Count;
		// テキスト設定
		comboTxt.text = comboTxtContent[comboNumber];
		// アニメーションを再度開始
		animator.SetTrigger("Start");
	}
}