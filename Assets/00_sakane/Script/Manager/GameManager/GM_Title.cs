using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

// �^�C�g���Ǘ��N���X
public class GM_Title : GameManagerBase, IGM_Title
{
	// �^�C�g���L�����o�X
	[SerializeField]
	GameObject titleCanvasPrefab;
	GameObject titleCanvas;
	ITitleCanvas iTitleCanvas;

	// �I�v�V�����L�����o�X
	[SerializeField]
	GameObject optionCanvasPrefab;
	GameObject optionCanvas;

	// �N���W�b�g�L�����o�X
	[SerializeField]
	GameObject creditCanvasPrefab;
	GameObject creditCanvas;

	// ���K�摜
	[SerializeField]
	List<Sprite> buttocksSprites = new List<Sprite>();
	public List<Sprite> ButtocksSprites
	{
		get => buttocksSprites;
	}

	protected override void Start()
	{
		base.Start();
		// �^�C�g���L�����o�X����
		titleCanvas = Instantiate(titleCanvasPrefab);
		// �^�C�g���L�����o�X�\��
		titleCanvas.SetActive(true);
		// �^�C�g���L�����o�X�̃C���^�[�t�F�C�X�擾
		iTitleCanvas = titleCanvas.GetComponent<ITitleCanvas>();

		// �I�v�V�����L�����o�X����
		optionCanvas = Instantiate(optionCanvasPrefab);
		// �I�v�V�����L�����o�X��\��
		optionCanvas.SetActive(false);

		// �N���W�b�g�L�����o�X����
		creditCanvas = Instantiate(creditCanvasPrefab);
		// �N���W�b�g��ʔ�\��
		creditCanvas.SetActive(false);
	}

	/// <summary>
	/// �Q�[���J�n
	/// </summary>
	async public UniTask GameStart()
	{
		// �ړ��ł��Ȃ���Ԃɂ���
		LevelManager.canLevelMove = false;
		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �V�[���ǂݍ���
		LevelManager.OpenLevel(token, "MainScene").Forget();
		// �t�F�[�h�A�E�g
		await FadeManager.Instance.FadeOutAsync();
		// �ǂݍ��ݏI���܂ő҂�
		await UniTask.WaitUntil(() => !LevelManager.IsLevelLoading);
		// �t�F�[�h��ړ��\�ɂ���
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// �^�C�g���L����
	/// </summary>
	public void TitleEnable()
	{
		iTitleCanvas.TitleEnable();
	}

	/// <summary>
	/// �I�v�V�������J��
	/// </summary>
	public void OptionOpen()
	{
		optionCanvas.SetActive(true);
	}

	/// <summary>
	/// �I�v�V���������
	/// </summary>
	public void OptionClose()
	{
		optionCanvas.SetActive(false);
		TitleEnable();
	}

	/// <summary>
	/// �N���W�b�g��ʕ\��
	/// </summary>
	public void CreditOpen()
	{
		creditCanvas.SetActive(true);
	}
}