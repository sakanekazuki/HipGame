using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 音量設定スレイダーのハンドル
public class SoundSliderHandle : MonoBehaviour
{
	// ハンドル
	[SerializeField]
	Image handleImg;

	// お尻画像
	[SerializeField]
	List<Sprite> buttocks = new List<Sprite>();

	// 画像サイズ
	List<Vector2> sizes = new List<Vector2>();

	// スライダー
	Slider slider;

	private void Start()
	{
		// スライダー取得
		slider = GetComponent<Slider>();

		foreach (var v in buttocks)
		{
			sizes.Add(v.bounds.size * 100);
		}
	}

	private void Update()
	{
		var number = Mathf.FloorToInt(((slider.value + 40) / 40) * 10);
		// 画像設定
		handleImg.sprite = buttocks[number];

		// レクトトランスフォーム取得
		var rect = handleImg.transform as RectTransform;

		var size = new Vector2(sizes[number].x, sizes[number].y / 2);
		// サイズ設定
		rect.sizeDelta = size;
	}
}