using UnityEngine;

// メインキャンバスのインターフェイス
public interface IMainCanvas
{
	void SetScoreText(string score);
	void SetAddScoreTxt(string addScore);
	void AddScoreTxtHide();
	void AddScoreNextTxt(string addScore, Vector3 worldPosition);
	void SetHighScoreText(string highScore);
	void SetComboValue(int value);
	void SetComboMagnification(float magnification);
	void SetShakeValue(int shakeValue);
	void SetNextButtocksImage(Sprite sprite);
	void SetControlSprite();
	void SetTimeLimit(float time);
	void TimeLimitReset();
}