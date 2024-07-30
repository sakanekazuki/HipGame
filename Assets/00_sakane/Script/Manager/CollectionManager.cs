using System.Collections.Generic;
using UnityEngine;

// �R���N�V�����Ǘ��N���X
public class CollectionManager : ManagerBase<CollectionManager>
{
	// �R���N�V�����L�����o�X
	[SerializeField]
	GameObject collectionCanvasPrefab;
	GameObject collectionCanvas;

	// ���{�ꖼ
	public readonly List<string> names_jp = new List<string>()
	{
		"�X���C��",
		"�S�u����",
		"���[�L���b�g",
		"�A�����E�l",
		"�]���r",
		"���[�E���t",
		"�o�j�[",
		"�I�[�K",
		"�Z�C���[��",
		"�h���S��",
		"�T�L���o�X"
	};

	// �p�ꖼ
	public readonly List<string> names_en = new List<string>()
	{
		"Slime",
		"Goblin",
		"Werecat",
		"Alraune",
		"Zombie",
		"Werewolf",
		"Bunny",
		"Ogre",
		"Siren",
		"Dragon",
		"Succubus"
	};

	// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X
	IGM_Main iGM_Main;

	private void Start()
	{
		// �R���N�V�����L�����o�X����
		collectionCanvas = Instantiate(collectionCanvasPrefab);
		// �R���N�V�����L�����o�X��\��
		collectionCanvas.SetActive(false);

		// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X�擾
		iGM_Main = GM_Main.Instance.GetComponent<IGM_Main>();
	}

	/// <summary>
	/// �R���N�V�����L�����o�X�J��
	/// </summary>
	public void CollectionCanvasOpen()
	{
		// �L�����o�X�J��
		collectionCanvas.SetActive(true);
		// ���͖�����
		iGM_Main?.SetPlayerState(false);
	}

	/// <summary>
	/// �R���N�V�����L�����o�X�����
	/// </summary>
	public void CollectionCanvasClose()
	{
		// �L�����o�X����
		collectionCanvas.SetActive(false);
		// ���͗L����
		iGM_Main?.SetPlayerState(true);

		// �^�C�g����ʂ̏ꍇ�^�C�g���L����
		if (GM_Title.Instance != null)
		{
			(GM_Title.Instance as GM_Title)?.TitleEnable();
		}
	}
}