using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

// �^�C�g���L�����o�X
public class TitleCanvas : CanvasBase, ITitleCanvas
{
	// �^�C�g���}�l�[�W���[�̃C���^�[�t�F�C�X
	IGM_Title iGM_Title;

	// �n�C�X�R�A�e�L�X�g
	[SerializeField]
	TextMeshProUGUI hightScoreTxt;

	// �^�C�g���̃{�^��
	[SerializeField]
	List<Button> titleButtons = new List<Button>();
	List<ButtonDefault> defaultButton = new List<ButtonDefault>();

	protected override void Start()
	{
		base.Start();
		// �^�C�g���}�l�[�W���[�̃C���^�[�t�F�C�X�擾
		iGM_Title = GM_Title.Instance.GetComponent<IGM_Title>();
		// �n�C�X�R�A��\��
		hightScoreTxt.text = GameInstance.saveData.highScore.ToString();
		// �^�C�g���̃{�^���L����
		foreach (var button in titleButtons)
		{
			defaultButton.Add(button.GetComponent<ButtonDefault>());
			button.interactable = true;
		}
	}

	/// <summary>
	/// �^�C�g���L����
	/// </summary>
	public void TitleEnable()
	{
		// �^�C�g���̃{�^���L����
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = true;
			button.interactable = true;
		}
	}

	/// <summary>
	/// �Q�[���J�n
	/// </summary>
	public void GameStart()
	{
		iGM_Title.GameStart();
	}

	/// <summary>
	/// �I�v�V�������J��
	/// </summary>
	public void OptionOpen()
	{
		iGM_Title.OptionOpen();
		// �^�C�g���̃{�^��������
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = false;
			button.interactable = false;
		}
	}

	/// <summary>
	/// �N���W�b�g�J��
	/// </summary>
	public void CreditOpen()
	{
		iGM_Title.CreditOpen();
		// �^�C�g���̃{�^��������
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = false;
			button.interactable = false;
		}
	}

	/// <summary>
	/// �R���N�V�������J��
	/// </summary>
	public void CollectionOpen()
	{
		CollectionManager.Instance.CollectionCanvasOpen();
		// �^�C�g���̃{�^��������
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = false;
			button.interactable = false;
		}
	}

	/// <summary>
	/// �Q�[���I���i�I��邩�������j
	/// </summary>
	public void GameQuit()
	{

		// �^�C�g���̃{�^��������
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = false;
			button.interactable = false;
		}
	}

	/// <summary>
	/// �Q�[���I��
	/// </summary>
	public void ApplicationQuit()
	{
#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPlaying = false;

#else

		Application.Quit();

#endif
	}
}