using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// タイトルのお尻
public class TitleButtocks : MonoBehaviour
{
	// お尻画像
	[SerializeField]
	List<Sprite> buttocksSprites = new List<Sprite>();

	// 画像表示クラス
	SpriteRenderer spriteRenderer;

	// 消えるまでの時間
	[SerializeField]
	float destroyTime = 5;

	private void Start()
	{
		// 画像表示クラス取得
		spriteRenderer = GetComponent<SpriteRenderer>();
		// ランダムで決める(開放されているお尻のみ)
		var number = Random.Range(0, buttocksSprites.Count);

		if (GameInstance.saveData.isMakes[number])
		{
			// 開放されている場合白
			spriteRenderer.color = Color.white;
		}
		else
		{
			// 開放されていない場合黒
			spriteRenderer.color = Color.black;
		}
		// 画像設定
		spriteRenderer.sprite = buttocksSprites[number];

		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// 数秒後自分を消す
		DestroyDelay(token).Forget();
	}

	/// <summary>
	/// 消すまでの時間
	/// </summary>
	/// <returns></returns>
	async UniTask DestroyDelay(CancellationToken token)
	{
		// 5秒後
		await UniTask.WaitForSeconds(destroyTime, cancellationToken: token);
		// 自身を消す
		Destroy(gameObject);
	}
}