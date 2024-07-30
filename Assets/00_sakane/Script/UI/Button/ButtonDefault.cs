using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 標準ボタン
public class ButtonDefault : MonoBehaviour
{
	// true = 有効
	bool isEnable = true;
	public bool IsEnable
	{
		get => isEnable;
		set
		{
			isEnable = value;
			if (value) { }
			else
			{
				if (isOnCursor)
				{
					isOnCursor = false;
					animator.SetBool("IsOnCursor", isOnCursor);
				}
			}
		}
	}

	// アニメーター
	Animator animator;

	// true = カーソルがあっている状態
	bool isOnCursor = false;

	// クリック時の音
	[SerializeField]
	AudioClip clickClip;

	private void Start()
	{
		// アニメーター取得
		animator = GetComponent<Animator>();
	}

	/// <summary>
	/// クリック時
	/// </summary>
	public void Click()
	{
		SoundManager.Instance.SEPlay(clickClip);
	}

	/// <summary>
	/// カーソルが合ったとき
	/// </summary>
	public void OnCursor()
	{
		if (!isEnable)
		{
			return;
		}
		// カーソルが合っている状態にする
		isOnCursor = true;
		// アニメーションのパラメーター設定
		animator.SetBool("IsOnCursor", isOnCursor);
	}

	/// <summary>
	/// カーソルが離れたとき
	/// </summary>
	public void ReleseCursor()
	{
		if (!isEnable)
		{
			return;
		}
		// カーソルが合っていない状態にする
		isOnCursor = false;
		// アニメーションのパラメーター設定
		animator.SetBool("IsOnCursor", isOnCursor);
	}
}
