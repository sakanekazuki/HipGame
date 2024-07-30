// クレジットキャンバス
public class CreditCanvas : CanvasBase
{
	/// <summary>
	/// クレジット画面を閉じる
	/// </summary>
	public void Close()
	{
		gameObject.SetActive(false);
		(GM_Title.Instance as GM_Title).TitleEnable();
	}
}