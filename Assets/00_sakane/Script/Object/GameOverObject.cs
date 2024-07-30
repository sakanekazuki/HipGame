using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// 衝突しているとゲームオーバーになるオブジェクト
public class GameOverObject : MonoBehaviour
{
	// ゲームオーバーになるまでの時間
	[SerializeField]
	float gameOverTime = 3;
	float time;
	// true = お尻に衝突中
	bool isHitButtocks = false;

	// 衝突したお尻
	List<GameObject> hitButtocks = new List<GameObject>();
	// true = 一度通った
	bool isOnece = false;

	// ゲームマネージャーのインターフェイス
	IGM_Main iGM_Main;

	private void Start()
	{
		// ゲームマネージャーのインターフェイス取得
		iGM_Main = GM_Main.Instance.GetComponent<IGM_Main>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Buttocks"))
		{
			isHitButtocks = true;
			// 衝突中のオブジェクトに追加
			hitButtocks.Add(collision.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (hitButtocks.Contains(collision.gameObject))
		{
			// 衝突中のオブジェクトから外す
			hitButtocks.Remove(collision.gameObject);
			// 衝突中のオブジェクトが存在しない場合、衝突フラグをfalseにする
			if (hitButtocks.Count <= 0)
			{
				isHitButtocks = false;

				iGM_Main.TimeLimitReset();
			}
		}
	}

	private void LateUpdate()
	{
		if (isHitButtocks)
		{
			// 時間を増やす
			time += Time.deltaTime;
			// 時間設定
			iGM_Main.SetTimeLimit(gameOverTime - time);
			// 時間を超えるとゲームオーバー
			if (time >= gameOverTime && !isOnece)
			{
				var token = this.GetCancellationTokenOnDestroy();
				GameOverAsync(token).Forget();
				isOnece = true;
			}
		}
		else
		{
			// お尻に衝突していない場合時間を戻す
			time = 0;
		}
	}

	/// <summary>
	/// ゲームオーバーまでのディレイ
	/// </summary>
	/// <returns></returns>
	async UniTask GameOverAsync(CancellationToken token)
	{
		await UniTask.WaitForSeconds(0.2f, cancellationToken: token);
		iGM_Main.GameOver();
	}
}