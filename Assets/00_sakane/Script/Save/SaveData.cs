using System.Collections.Generic;

// 保存するデータ
public class SaveData
{
	// 最高スコア
	public int highScore = 0;

	// 音量
	public float masterVolume = 1;
	public float bgmVolume = 0.5f;
	public float seVolume = 0.5f;

	// 言語
	public LanguageManager.LanguageType language = LanguageManager.LanguageType.Japanese;

	// お尻を作った
	public List<bool> isMakes = new List<bool>()
	{
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false
	};
}