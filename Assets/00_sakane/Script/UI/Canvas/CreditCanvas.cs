// �N���W�b�g�L�����o�X
public class CreditCanvas : CanvasBase
{
	/// <summary>
	/// �N���W�b�g��ʂ����
	/// </summary>
	public void Close()
	{
		gameObject.SetActive(false);
		(GM_Title.Instance as GM_Title).TitleEnable();
	}
}