using UnityEngine;

// ガイドライン
public class GuideLineObject : MonoBehaviour
{
	// マスクのサイズ
	Vector2 maskSize = Vector2.zero;

	// ガイドラインのマスクオブジェクト
	[SerializeField]
	GameObject guideLineMask;

	private void Start()
	{
		// ガイドラインのスプライトを取得
		var sprite = guideLineMask.GetComponent<SpriteMask>().sprite;
		// スプライトのサイズ取得
		maskSize = sprite.bounds.size;
	}

	private void Update()
	{
		// 衝突情報取得
		var hit = Physics2D.Raycast(transform.position, Vector2.down, maskSize.x, LayerMask.GetMask("Buttocks"));
		if (hit.collider != null)
		{
			// 割合計算
			var ratio = hit.distance / maskSize.x;
			// サイズ設定
			guideLineMask.transform.localScale = new Vector3(ratio, 1, 1);
		}
		else
		{
			guideLineMask.transform.localScale = Vector3.one;
		}
	}
}