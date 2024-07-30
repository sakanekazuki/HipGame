using UnityEngine;

// ゲームマネージャーのインターフェイス
public interface IGM_Main
{
    void SetPlayerState(bool isActive);
    void SetGuideLinePosition(Vector2 position);
    void GuideLineHide();
    void GuideLineShow();
	void OptionOpen();
    void OptionClose();
    void GameOver();
    void AddScore(int score);
    void AddScoreNextTxt(int score, Vector3 worldPosition);
	int  GetComboValue();
    void ComboReset();
    void AddComboValue();
    void SetComboMagnification();
	void SetNextButtocksImage(Sprite sprite);
    void SetControlSprite();
    void SetTimeLimit(float time);
	void TimeLimitReset();
}
