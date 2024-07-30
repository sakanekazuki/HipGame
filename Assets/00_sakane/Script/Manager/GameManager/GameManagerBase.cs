using System.Collections.Generic;
using UnityEngine;

// ゲームマネージャーのベース
public class GameManagerBase : ManagerBase<GameManagerBase>
{
	// 生成するオブジェクト
	[SerializeField]
	protected GameObject defaultPlayerObjectPrefab;

	// プレイヤーを生成する位置
	protected List<GameObject> playerStarter = new List<GameObject>();

	// 生成したキャラクター
	public GameObject character
	{
		get;
		private set;
	}

	protected virtual void Start()
	{
		// プレイヤースターターが内場合エラー
		if (playerStarter.Count == 0)
		{
			//Debug.LogError("プレイヤースターターがありません");
		}
		foreach (var starter in playerStarter)
		{
			// プレイヤー生成
			character = Instantiate(defaultPlayerObjectPrefab, starter.transform.position, Quaternion.identity);
		}
	}

	/// <summary>
	/// プレイヤースタート追加
	/// </summary>
	/// <param name="starter">プレイヤースタートの位置に存在するオブジェクト</param>
	public void AddStarter(GameObject starter)
	{
		playerStarter.Add(starter);
	}
}