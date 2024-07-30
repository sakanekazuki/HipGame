// ����Ǘ��N���X
public class LanguageManager : ManagerBase<LanguageManager>
{
	// ����̎��
	public enum LanguageType
	{
		Japanese,
		English,
	}

	// ���݂̌���
	static LanguageType language = LanguageType.Japanese;
	public static LanguageType Language
	{
		get => language;
		set
		{
			language = value;
			GameInstance.saveData.language = value;
		}
	}
}