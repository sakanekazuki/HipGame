using UnityEngine;

// �A�j���[�V�������Ȃ��{�^��
public class NonAnimButton : MonoBehaviour
{
	// �N���b�N��
	[SerializeField]
	AudioClip clickClip;

	/// <summary>
	/// �N���b�N
	/// </summary>
	public void Click()
	{
		SoundManager.Instance.SEPlay(clickClip);
	}
}