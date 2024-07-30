using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// お尻
public class Buttocks : MonoBehaviour, IButtocks
{
	// 現在の種類
	ButtocksManager.ButtocksType nowType = ButtocksManager.ButtocksType.Type2;

	// 物理
	Rigidbody2D rb2d;
	// 画像
	SpriteRenderer spriteRenderer;
	// コリジョン
	[SerializeField]
	List<Collider2D> collider2d = new List<Collider2D>();
	// アニメーション
	Animator animator;

	// true = アニメーション終了状態
	bool isAnimFinish = true;

	// true = プール状態
	bool isPool = true;

	// true = 初めて衝突する
	bool isFarstHit = true;

	// デフォルトのサイズ
	Vector2 defaultSize = Vector2.zero;

	// 衝突時のSE
	[SerializeField]
	AudioClip hitClip;

	// このフレームで衝突したオブジェクト
	List<GameObject> thisFrameHitObject = new List<GameObject>();

	// true = 音を鳴らせる
	bool canSoundPlay = true;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (isPool)
		{
			return;
		}

		if (isFarstHit)
		{
			// 落とせる状態にする
			ButtocksManager.Instance.CanDrop = true;
			// アニメーションさせる
			animator.SetTrigger("isHit");
		}

		if (isAnimFinish)
		{
			// アニメーションできない状態にする
			isAnimFinish = false;
		}
		// 一度当たった状態にする
		isFarstHit = false;

		if (canSoundPlay)
		{
			// 音を鳴らす
			SoundManager.Instance.SEPlay(hitClip);
			// 鳴らせない状態にする
			canSoundPlay = false;
			// 次に音を再生するまで待つ
			var token = this.GetCancellationTokenOnDestroy();
			SoundDelay(token).Forget();
		}

		if (!collision.gameObject.CompareTag("Buttocks"))
		{
			return;
		}

		if (transform.position.y > collision.transform.position.y)
		{
			// 衝突をマネージャーに知らせる
			ButtocksManager.Instance.ButtocksHit(gameObject, collision.gameObject);
		}
		else
		{
			// 衝突をマネージャーに知らせる
			ButtocksManager.Instance.ButtocksHit(collision.gameObject, gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (isPool)
		{
			return;
		}
		// 衝突したオブジェクトを追加
		thisFrameHitObject.Add(collision.gameObject);
	}

	private void LateUpdate()
	{
		if (isPool)
		{
			return;
		}
		foreach (var v in thisFrameHitObject)
		{
			if (!v)
			{
				continue;
			}
			if (v.CompareTag("Buttocks"))
			{
				if (transform.position.y > v.transform.position.y)
				{
					// 衝突をマネージャーに知らせる
					ButtocksManager.Instance.ButtocksHit(gameObject, v.gameObject);
				}
				else
				{
					// 衝突をマネージャーに知らせる
					ButtocksManager.Instance.ButtocksHit(v.gameObject, gameObject);
				}
			}
		}
		thisFrameHitObject.Clear();
	}

	//private void OnCollisionStay2D(Collision2D collision)
	//{
	//	if (!collision.transform.parent)
	//	{
	//		return;
	//	}
	//	if ((collision.transform.parent.gameObject == gameObject) || isPool)
	//	{
	//		return;
	//	}

	//	if (collision.gameObject.CompareTag("Buttocks"))
	//	{
	//		ButtocksManager.Instance.ButtocksHit(gameObject, collision.gameObject);
	//	}
	//}

	/// <summary>
	/// アニメーション終了
	/// </summary>
	public void AnimFinish()
	{
		var token = this.GetCancellationTokenOnDestroy();
		AnimFinishDelay(token).Forget();
	}

	/// <summary>
	/// アニメーション終了後のディレイ
	/// </summary>
	/// <param name="token">キャンセレーショントークン</param>
	/// <returns></returns>
	async UniTask AnimFinishDelay(CancellationToken token)
	{
		await UniTask.WaitForSeconds(0.3f, cancellationToken: token);
		isAnimFinish = true;
	}

	/// <summary>
	/// 音を鳴らすディレイ
	/// </summary>
	/// <returns></returns>
	async UniTask SoundDelay(CancellationToken token)
	{
		await UniTask.WaitForSeconds(1, cancellationToken: token);
		canSoundPlay = true;
	}

	/// <summary>
	/// 初期化
	/// </summary>
	public void Initialize()
	{
		// 物理取得
		rb2d = GetComponent<Rigidbody2D>();
		// 画像クラス取得
		spriteRenderer = GetComponent<SpriteRenderer>();

		foreach (var col in collider2d)
		{
			// コライダー無効
			col.enabled = false;
		}
		// アニメーター取得
		animator = GetComponent<Animator>();
	}

	/// <summary>
	/// 動きを止める
	/// </summary>
	public void MoveStop()
	{
		// 移動を止める
		rb2d.velocity = Vector2.zero;
		// 回転を止める
		rb2d.angularVelocity = 0;
	}

	/// <summary>
	/// ステータス設定
	/// </summary>
	/// <param name="status">設定するステータス</param>
	public void SetStatus(ButtocksStatus status)
	{
		// 種類
		nowType = status.type;
		// 重さ
		rb2d.mass = status.mass;
		// 画像
		spriteRenderer.sprite = status.sprite;

		// 位置設定
		collider2d[0].transform.localPosition = new Vector2(-status.colliderPosition.x, status.colliderPosition.y);
		collider2d[1].transform.localPosition = new Vector2(status.colliderPosition.x, status.colliderPosition.y);

		// サイズ設定
		(collider2d[0] as CapsuleCollider2D).size = status.size;
		(collider2d[1] as CapsuleCollider2D).size = status.size;
	}

	/// <summary>
	/// 落とす
	/// </summary>
	public void Drop()
	{
		// 重力を戻す
		rb2d.gravityScale = 1;

		foreach (var col in collider2d)
		{
			// コライダー有効
			col.enabled = true;
		}
	}

	/// <summary>
	/// プール状態取得
	/// </summary>
	/// <returns>プール状態</returns>
	public bool GetIsPool()
	{
		return isPool;
	}

	/// <summary>
	/// プール状態の設定
	/// </summary>
	/// <param name="isPool">プール状態</param>
	public void SetIsPool(bool isPool)
	{
		this.isPool = isPool;
		// プール状態だったらコリジョンを無効にして、重力をなしにする
		if (isPool)
		{
			foreach (var col in collider2d)
			{
				// コリジョン無効
				col.enabled = false;
			}
			// 重力なし
			rb2d.gravityScale = 0;
			// 動きを止める
			MoveStop();
			// 回転をもとに戻す
			transform.localEulerAngles = Vector3.zero;
		}
	}

	/// <summary>
	/// 進化
	/// </summary>
	public void Evolution()
	{
		if (isPool)
		{
			return;
		}
		// 衝突情報
		List<RaycastHit2D> hit2d = new List<RaycastHit2D>();
		foreach (var col in collider2d)
		{
			// カプセルに変換
			var capsule = col as CapsuleCollider2D;
			// 衝突情報を登録
			foreach (var v in Physics2D.CapsuleCastAll(
				col.transform.position,
				capsule.size + new Vector2(0.1f, 0.1f),
				CapsuleDirection2D.Horizontal,
				0,
				new Vector2(0.1f, 0.1f),
				LayerMask.GetMask("Buttocks")))
			{
				if (v.collider.transform.parent.gameObject == gameObject)
				{
					continue;
				}
				if (v.collider.gameObject.CompareTag("Buttocks"))
				{
					hit2d.Add(v);
				}
			}
		}
		// 衝突情報の中にお尻があるか調べる
		foreach (var v in hit2d)
		{
			if (v.collider.gameObject == null)
			{
				continue;
			}
			if (v.collider.transform.parent.GetComponent<IButtocks>().GetIsPool())
			{
				continue;
			}
			// 衝突を知らせる
			if (ButtocksManager.Instance.ButtocksHit(gameObject, v.collider.transform.parent.gameObject))
			{
				break;
			}
		}
	}

	/// <summary>
	/// お尻の種類取得
	/// </summary>
	/// <returns>現在の種類</returns>
	ButtocksManager.ButtocksType IButtocks.GetType()
	{
		return nowType;
	}

	/// <summary>
	/// 衝突状態取得
	/// </summary>
	/// <returns></returns>
	public bool GetIsFarstHit()
	{
		return isFarstHit;
	}

	/// <summary>
	/// 衝突状態設定
	/// </summary>
	/// <param name="isFarstHit">設定する状態</param>
	public void SetIsFarstHit(bool isFarstHit)
	{
		this.isFarstHit = isFarstHit;
	}
}