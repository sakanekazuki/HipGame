using System.Collections.Generic;

// �ۑ�����f�[�^
public class SaveData
{
	// �ō��X�R�A
	public int highScore = 0;

	// ����
	public float masterVolume = 1;
	public float bgmVolume = 0.5f;
	public float seVolume = 0.5f;

	// ����
	public LanguageManager.LanguageType language = LanguageManager.LanguageType.Japanese;

	// ���K�������
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