using System.Diagnostics.Tracing;
using UnityEngine;

// SE
public class Sound : MonoBehaviour, ISound
{
	// 音を鳴らすコンポーネント
	AudioSource source;

	// 再生状態
	bool isPlaying = false;

	// 音
	AudioClip clip;

	void Start()
	{
		// 音を鳴らすコンポーネント取得
		source = GetComponent<AudioSource>();
		// ループしない
		source.loop = false;
		// 音設定
		source.clip = clip;
		// 再生状態にする
		isPlaying = true;
		// 再生
		source.Play();
	}

	void Update()
	{
		if (isPlaying)
		{
			// 際し絵状態設定
			isPlaying = source.isPlaying;
			if (!isPlaying)
			{
				// 削除
				Destroy(gameObject);
			}
		}
	}

	/// <summary>
	/// 再生
	/// </summary>
	/// <param name="clip">再生する音</param>
	public void Play(AudioClip clip)
	{
		// クリップの設定
		this.clip = clip;
	}
}
