using UnityEngine;
using UnityEngine.UI;

// コレクションカード
public class CollectionCard : MonoBehaviour
{
	// コレクションキャンバス
	[SerializeField]
	CollectionCanvas collectionCanvas;

	// 自分のスプライト画像
	Sprite sprite;

	private void OnEnable()
	{
		// 自分のスプライト取得
		sprite = GetComponent<Image>().sprite;
	}

	/// <summary>
	/// カーソルが合ったときの処理
	/// </summary>
	public void OnCursor()
	{
		// カードの画像表示
		collectionCanvas.SetBigCollectionSprite(sprite);
		// 大きいカード表示
		collectionCanvas.BigCollectionDisplay();
	}

	/// <summary>
	/// カーソルが離れたときの処理
	/// </summary>
	public void ExitCursor()
	{
		// 大きいカード非表示
		collectionCanvas.BigCollectionHide();
	}
}