using UnityEngine;
using UnityEngine.UI;

// 追加されるスコア
public class TitleAddScore : MonoBehaviour
{
	// 番号
	[SerializeField]
	int number = 0;

	// 変化する画像
	[SerializeField]
	Image changeImg;

	// 前の色
	Color defaultColor = Color.white;

	// アニメーター
	Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		if (GameInstance.saveData.isMakes[number - 1])
		{
			changeImg.color = Color.white;
			defaultColor = Color.white;
		}
		else
		{
			changeImg.color = Color.black;
			defaultColor = Color.black;
		}
	}

	private void OnEnable()
	{
		animator?.Play("Stay", 0, 0);
	}

	/// <summary>
	/// 画像の色を変更
	/// </summary>
	public void ChangeImageColor()
	{
		if (number == 11)
		{
			return;
		}
		// 開放されているかどうかで色変更
		if (GameInstance.saveData.isMakes[number])
		{
			changeImg.color = Color.white;
		}
		else
		{
			changeImg.color = Color.black;
		}
	}

	/// <summary>
	/// 画像の色をもとに戻す
	/// </summary>
	public void ReturnImgColor()
	{
		changeImg.color = defaultColor;
	}
}