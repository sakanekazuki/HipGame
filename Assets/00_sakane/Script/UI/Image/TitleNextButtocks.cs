using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// タイトルの次のお尻
public class TitleNextButtocks : MonoBehaviour
{
	// 画像
	Image image;
	// アニメーター
	Animator animator;

	// 開放されているお尻の画像（開放されていないものを一つ含む）
	List<Sprite> openButtocksSprite = new List<Sprite>();
	// 表示しているお尻の番号
	int number = 0;

	private void Start()
	{
		// 画像取得
		image = GetComponent<Image>();
		// アニメーター取得
		animator = GetComponent<Animator>();

		for (int i = 0; i < GameInstance.saveData.isMakes.Count; ++i)
		{
			// 開放されているお尻の画像登録
			openButtocksSprite.Add((GM_Title.Instance as GM_Title).ButtocksSprites[i]);
			if (!GameInstance.saveData.isMakes[i])
			{
				break;
			}
		}
		// 番号をあえて-1」にして次のお尻を設定する際に0になるようにする
		number = -1;
		// 次のお尻設定
		NextButtocks();

		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// フェードが終わるまで待ってからアニメーション
		StartAsync(token).Forget();
	}

	/// <summary>
	/// フェードが終わるまで待つ
	/// </summary>
	/// <param name="token">キャンセレーショントークン</param>
	/// <returns></returns>
	async UniTask StartAsync(CancellationToken token)
	{
		// フェード終了まで待つ
		await UniTask.WaitUntil(() => !FadeManager.Instance.IsFading, cancellationToken: token);
		// フェード後すこし待つ
		await UniTask.WaitForSeconds(0.3f, cancellationToken: token);
		// 動かす
		animator.SetTrigger("Move");
	}

	/// <summary>
	/// 次のお尻に変更
	/// </summary>
	void NextButtocks()
	{
		// 次の番号にする
		++number;
		// 画像の数を超えないようにする
		number %= openButtocksSprite.Count;
		// 画像設定
		image.sprite = openButtocksSprite[number];

		// 基本的に白
		image.color = Color.white;
		// 開放されていないお尻が存在する場合
		if (GameInstance.saveData.isMakes.Contains(false))
		{
			// 開放されていない場合最後の画像は黒くする
			if (number == openButtocksSprite.Count - 1)
			{
				image.color = Color.black;
			}
		}
	}

	/// <summary>
	/// アニメーション終了
	/// </summary>
	public void AnimFinish()
	{
		// 次の画像に移行
		NextButtocks();
		// 次のアニメーションを指せる
		animator.SetTrigger("Move");
	}
}