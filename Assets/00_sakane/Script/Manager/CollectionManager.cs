using System.Collections.Generic;
using UnityEngine;

// コレクション管理クラス
public class CollectionManager : ManagerBase<CollectionManager>
{
	// コレクションキャンバス
	[SerializeField]
	GameObject collectionCanvasPrefab;
	GameObject collectionCanvas;

	// 日本語名
	public readonly List<string> names_jp = new List<string>()
	{
		"スライム",
		"ゴブリン",
		"ワーキャット",
		"アルラウネ",
		"ゾンビ",
		"ワーウルフ",
		"バニー",
		"オーガ",
		"セイレーン",
		"ドラゴン",
		"サキュバス"
	};

	// 英語名
	public readonly List<string> names_en = new List<string>()
	{
		"Slime",
		"Goblin",
		"Werecat",
		"Alraune",
		"Zombie",
		"Werewolf",
		"Bunny",
		"Ogre",
		"Siren",
		"Dragon",
		"Succubus"
	};

	// ゲームマネージャーのインターフェイス
	IGM_Main iGM_Main;

	private void Start()
	{
		// コレクションキャンバス生成
		collectionCanvas = Instantiate(collectionCanvasPrefab);
		// コレクションキャンバス非表示
		collectionCanvas.SetActive(false);

		// ゲームマネージャーのインターフェイス取得
		iGM_Main = GM_Main.Instance.GetComponent<IGM_Main>();
	}

	/// <summary>
	/// コレクションキャンバス開く
	/// </summary>
	public void CollectionCanvasOpen()
	{
		// キャンバス開く
		collectionCanvas.SetActive(true);
		// 入力無効化
		iGM_Main?.SetPlayerState(false);
	}

	/// <summary>
	/// コレクションキャンバスを閉じる
	/// </summary>
	public void CollectionCanvasClose()
	{
		// キャンバス閉じる
		collectionCanvas.SetActive(false);
		// 入力有効化
		iGM_Main?.SetPlayerState(true);

		// タイトル画面の場合タイトル有効化
		if (GM_Title.Instance != null)
		{
			(GM_Title.Instance as GM_Title)?.TitleEnable();
		}
	}
}