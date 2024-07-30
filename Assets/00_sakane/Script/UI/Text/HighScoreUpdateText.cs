using UnityEngine;

// ハイスコア更新のテキスト
public class HighScoreUpdateText : MonoBehaviour
{
	// ハイスコア表示しているテキスト
	TMPro.TextMeshProUGUI highScoreTxt;
	// 最初の色
	Color defaultColor = Color.white;

	// 点滅速度
	[SerializeField]
	float speed = 0.2f;

	// シータ
	float theta = 0;

	void Start()
	{
		// テキスト取得
		highScoreTxt = GetComponent<TMPro.TextMeshProUGUI>();
		// 最初の色
		defaultColor = highScoreTxt.color;
	}

	void Update()
	{
		// アルファ計算
		var alpha = Mathf.Sin(theta);
		// 速度を足す
		theta += speed;
		// アニメーションさせる
		highScoreTxt.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha);
	}
}
