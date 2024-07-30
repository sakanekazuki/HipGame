using UnityEngine;
using UnityEngine.UI;

// �R���N�V�����J�[�h
public class CollectionCard : MonoBehaviour
{
	// �R���N�V�����L�����o�X
	[SerializeField]
	CollectionCanvas collectionCanvas;

	// �����̃X�v���C�g�摜
	Sprite sprite;

	private void OnEnable()
	{
		// �����̃X�v���C�g�擾
		sprite = GetComponent<Image>().sprite;
	}

	/// <summary>
	/// �J�[�\�����������Ƃ��̏���
	/// </summary>
	public void OnCursor()
	{
		// �J�[�h�̉摜�\��
		collectionCanvas.SetBigCollectionSprite(sprite);
		// �傫���J�[�h�\��
		collectionCanvas.BigCollectionDisplay();
	}

	/// <summary>
	/// �J�[�\�������ꂽ�Ƃ��̏���
	/// </summary>
	public void ExitCursor()
	{
		// �傫���J�[�h��\��
		collectionCanvas.BigCollectionHide();
	}
}