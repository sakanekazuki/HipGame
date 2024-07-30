// Œ¾ŒêŠÇ—ƒNƒ‰ƒX
public class LanguageManager : ManagerBase<LanguageManager>
{
	// Œ¾Œê‚Ìí—Ş
	public enum LanguageType
	{
		Japanese,
		English,
	}

	// Œ»İ‚ÌŒ¾Œê
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