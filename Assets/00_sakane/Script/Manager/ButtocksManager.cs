using System.Collections.Generic;
using UnityEngine;

// オブジェクト管理クラス
public class ButtocksManager : ManagerBase<ButtocksManager>
{
	// お尻の種類
	public enum ButtocksType
	{
		Type2,
		Type3,
		Type4,
		Type5,
		Type6,
		Type7,
		Type8,
		Type9,
		Type10,
		Type11,
		Type12,
	}

	// お尻のプレハブ
	[SerializeField]
	GameObject buttocksPrefab;

	// プールしておく位置
	[SerializeField]
	GameObject poolPosition;

	// お尻のインターフェイス
	Dictionary<GameObject, IButtocks> iButtocks = new Dictionary<GameObject, IButtocks>();

	// 落とすお尻に選ばれる最大値
	[SerializeField]
	ButtocksType dropMaxType = ButtocksType.Type5;

	// 次に落とすお尻
	GameObject nextButtocks = null;
	// 落とすお尻
	GameObject dropButtocks = null;

	// お尻のステータス
	[SerializeField]
	SO_ButtocksStatuses buttocksStatuses;

	// 落とし始めるY座標
	[SerializeField]
	GameObject dropYPosition;
	// 落とすX座標の最大値にあるオブジェクト
	[SerializeField]
	GameObject dropXMaxPositionObject;
	// 落とすX座標の最小値にあるオブジェクト
	[SerializeField]
	GameObject dropXMinPositionObject;

	// 落とす位置
	Vector3 dropXMinPosition = Vector3.zero;
	Vector3 dropXMaxPosition = Vector3.zero;

	// 落とす位置
	Vector2 dropPosition = Vector2.zero;

	// 最後のお尻を消したときのエフェクト
	[SerializeField]
	GameObject lastUnionEffect;
	// 最後のお尻を消したときの音
	[SerializeField]
	AudioClip lastUnionClip;

	// true = お尻を落とせる
	bool canDrop = true;
	public bool CanDrop
	{
		get
		{
			return canDrop;
		}
		set
		{
			canDrop = value;
			if (value)
			{
				Next();
			}
		}
	}

	// コンボで出るエフェクト
	[SerializeField]
	List<GameObject> comboEffects = new List<GameObject>();

	// 初期化した後 true
	bool isInit = false;

	// ゲームマネージャーのインターフェイス
	IGM_Main iGM_Main;

	private void Start()
	{
		// インターフェイス取得
		iGM_Main = GM_Main.Instance.GetComponent<IGM_Main>();

		// お尻生成
		NextButtocksDecide();
		// 初期化
		isInit = true;

		// 次に落とすお尻設定
		Next();
	}

	/// <summary>
	/// お尻生成
	/// </summary>
	public GameObject SpawnButtocks()
	{
		// オブジェクト生成
		var obj = Instantiate(buttocksPrefab, poolPosition.transform.position, Quaternion.identity);
		// 名前設定
		obj.name = (iButtocks.Count + 1).ToString();
		// インターフェイス取得
		iButtocks.Add(obj, obj.GetComponent<IButtocks>());
		// お尻初期化
		iButtocks[obj].Initialize();

		return obj;
	}

	/// <summary>
	/// お尻同士の衝突
	/// </summary>
	/// <param name="hitObj1">衝突したオブジェクト1</param>
	/// <param name="hitObj2">衝突したオブジェクト2</param>
	/// <returns>同じ種類のお尻かどうか</returns>
	public bool ButtocksHit(GameObject hitObj1, GameObject hitObj2)
	{
		if ((GM_Main.Instance as GM_Main).IsGameOver)
		{
			return false;
		}

		// 同じ種類だと片方をプールに戻して、以降衝突処理を行わない
		if (iButtocks[hitObj1].GetType() == iButtocks[hitObj2].GetType())
		{
			// スコア追加
			iGM_Main.AddScore(GetButtocksStatus((int)iButtocks[hitObj1].GetType()).score);
			iGM_Main.AddScoreNextTxt(GetButtocksStatus((int)iButtocks[hitObj1].GetType()).score, hitObj1.transform.position);
			// オブジェクト2が初めて衝突するのであれば落とせる状態にする
			if (iButtocks[hitObj2].GetIsFarstHit())
			{
				CanDrop = true;
				iButtocks[hitObj2].SetIsFarstHit(false);
			}

			// コンボ数に応じたエフェクトを出す
			var effectNumber = iGM_Main.GetComboValue();
			// エフェクトの数を超えないようにする
			if (effectNumber > comboEffects.Count - 1)
			{
				effectNumber = comboEffects.Count - 1;
			}
			// コンボ倍率表示
			iGM_Main.SetComboMagnification();
			// コンボ数追加
			iGM_Main.AddComboValue();

			Stage.Instance.RightButtockShake((int)iButtocks[hitObj1].GetType());

			// 最後のお尻だった場合両方消える
			if (iButtocks[hitObj1].GetType() == ButtocksType.Type12)
			{
				// エフェクト生成
				Instantiate(lastUnionEffect, hitObj1.transform.position, Quaternion.identity);
				// SEを鳴らす
				SoundManager.Instance.SEPlay(lastUnionClip);

				// 両方プール状態にする
				iButtocks[hitObj1].SetIsPool(true);
				iButtocks[hitObj2].SetIsPool(true);
				// 両方削除
				Destroy(hitObj1);
				Destroy(hitObj2);
			}
			// 最後ではない場合次のお尻に進化？
			else
			{
				// エフェクト生成
				Instantiate(comboEffects[effectNumber], hitObj1.transform.position, Quaternion.identity);

				// 片方をプール状態にする
				iButtocks[hitObj2].SetIsPool(true);
				// 片方をプールに戻す
				Destroy(hitObj2);
				// プールしている位置に移動させる
				hitObj2.transform.position = poolPosition.transform.position;
				// 次のお尻にする
				iButtocks[hitObj1].SetStatus(GetButtocksStatus((int)iButtocks[hitObj1].GetType() + 1));
				// 動きを止める
				iButtocks[hitObj1].MoveStop();
				// 進化
				//iButtocks[hitObj1].Evolution();
			}
			// お尻を作った
			Stage.Instance.MakeButtocks((int)iButtocks[hitObj1].GetType());
			return true;
		}
		return false;
	}

	/// <summary>
	/// 落とすお尻移動
	/// </summary>
	/// <param name="dropPosition">移動させる位置</param>
	public void DropButtocksMove(Vector2 dropPosition)
	{
		// 落とす位置設定
		this.dropPosition = dropPosition;
		// y座標は固定する
		this.dropPosition.y = dropYPosition.transform.position.y;

		if (!dropButtocks)
		{
			return;
		}

		// 移動範囲の制限
		if (this.dropPosition.x < dropXMinPosition.x)
		{
			this.dropPosition.x = dropXMinPosition.x;
		}
		if (dropXMaxPosition.x < this.dropPosition.x)
		{
			this.dropPosition.x = dropXMaxPosition.x;
		}
		// ガイドラインの位置設定
		iGM_Main.SetGuideLinePosition(this.dropPosition);

		// 位置指定
		dropButtocks.transform.position = this.dropPosition;
	}

	/// <summary>
	/// お尻を落とす
	/// </summary>
	public void Drop()
	{
		// 落とせない状態であれば戻す
		if (!CanDrop || Stage.Instance.IsShaking)
		{
			return;
		}
		// 落とすお尻がないもしくはお尻のインターフェイスがない場合戻す
		if (!dropButtocks || iButtocks.Count <= 0)
		{
			return;
		}
		// 落とす
		iButtocks[dropButtocks].Drop();
		// コンボ数リセット
		iGM_Main.ComboReset();
		// ガイドライン非表示
		iGM_Main.GuideLineHide();
		// 落とすお尻をnullにする
		dropButtocks = null;
		// 落とせない状態にする
		CanDrop = false;
	}

	/// <summary>
	/// 次のお尻を決める
	/// </summary>
	public void NextButtocksDecide()
	{
		nextButtocks = SpawnButtocks();
		// ステータスの番号決定
		var dropNumber = Random.Range(0, (int)dropMaxType + 1);

		if (!isInit)
		{
			dropNumber = 0;
		}
		// 対応するステータス取得
		var status = GetButtocksStatus(dropNumber);
		// ステータス設定
		iButtocks[nextButtocks].SetStatus(status);
		// 次に落とすお尻の画像設定
		iGM_Main.SetNextButtocksImage(status.sprite);
		// お尻を作ったことにする
		Stage.Instance.MakeButtocks((int)status.type);
	}

	/// <summary>
	/// 次のお尻を落とすお尻に
	/// </summary>
	public void Next()
	{
		// 次に落とすお尻を落とすお尻にする
		dropButtocks = nextButtocks;
		// プール状態を設定
		iButtocks[dropButtocks].SetIsPool(false);
		// 次に落とすお尻を削除
		nextButtocks = null;
		// 回転をリセット
		dropButtocks.transform.localRotation = Quaternion.identity;

		// 最小値と最大値の設定
		var size = GetButtocksStatus((int)iButtocks[dropButtocks].GetType()).size;
		dropXMinPosition = dropXMinPositionObject.transform.position + new Vector3(size.x, 0, 0);
		dropXMaxPosition = dropXMaxPositionObject.transform.position - new Vector3(size.x, 0, 0);
		// 次に落とすお尻を設定
		NextButtocksDecide();

		// 落とす位置取得
		dropPosition = UnityEngine.InputSystem.Mouse.current.position.value;
		// ワールド座標に変換
		dropPosition = Camera.main.ScreenToWorldPoint(dropPosition);
		// 落とす位置に設定するために一度呼び出す
		DropButtocksMove(dropPosition);
		// ガイドライン表示
		iGM_Main.GuideLineShow();
	}

	/// <summary>
	/// ステータス取得
	/// </summary>
	/// <param name="number">取得するステータスの番号</param>
	/// <returns>取得したステータス</returns>
	public ButtocksStatus GetButtocksStatus(int number)
	{
		return buttocksStatuses.statuses[number];
	}
}