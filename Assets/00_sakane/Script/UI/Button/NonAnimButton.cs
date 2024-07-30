using UnityEngine;

// アニメーションしないボタン
public class NonAnimButton : MonoBehaviour
{
	// クリック音
	[SerializeField]
	AudioClip clickClip;

	/// <summary>
	/// クリック
	/// </summary>
	public void Click()
	{
		SoundManager.Instance.SEPlay(clickClip);
	}
}