using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �I�v�V�����L�����o�X
public class OptionCanvas : CanvasBase
{
	// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X
	IGM_Main iGM_Main;
	IGM_Title iGM_Title;

	// ���ʐݒ�̃X���C�_�[
	[SerializeField]
	Slider masterSlider;
	[SerializeField]
	Slider bgmSlider;
	[SerializeField]
	Slider seSlider;

	// ���݂̌����\������e�L�X�g
	[SerializeField]
	TMPro.TextMeshProUGUI languageTxt;

	protected override void Start()
	{
		base.Start();
		// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X�擾
		iGM_Main = GM_Main.Instance?.GetComponent<IGM_Main>();
		// �^�C�g���}�l�[�W���[�̃C���^�[�t�F�C�X�擾
		iGM_Title = GM_Title.Instance?.GetComponent<IGM_Title>();

		// ���ʃX���C�_�[�̐ݒ�
		masterSlider.value = GameInstance.saveData.masterVolume;
		bgmSlider.value = GameInstance.saveData.bgmVolume;
		seSlider.value = GameInstance.saveData.seVolume;

		// �x�^�����̌���Ή��i�֐�������www�j
		switch (GameInstance.saveData.language)
		{
		case LanguageManager.LanguageType.Japanese:
			languageTxt.text = "Japanese";
			break;
		case LanguageManager.LanguageType.English:
			languageTxt.text = "English";
			break;
		}
	}

	/// <summary>
	/// �I�v�V���������
	/// </summary>
	public void OptionClose()
	{
		iGM_Main?.OptionClose();
		iGM_Title?.OptionClose();
	}

	/// <summary>
	/// �}�X�^�[�{�����[��
	/// </summary>
	/// <param name="volume">����</param>
	public void MaterVolume(float volume)
	{
		SoundManager.Instance.MasterVolume = volume;
	}

	/// <summary>
	/// BGM�̉��ʐݒ�
	/// </summary>
	/// <param name="volume">�ݒ肷�鉹��</param>
	public void BGMVolume(float volume)
	{
		SoundManager.Instance.BGMVolume = volume;
	}

	/// <summary>
	/// SE�̉��ʐݒ�
	/// </summary>
	/// <param name="volume">�ݒ肷�鉹��</param>
	public void SEVolume(float volume)
	{
		SoundManager.Instance.SEVolume = volume;
	}

	/// <summary>
	/// ����ݒ�
	/// </summary>
	/// <param name="isNext">�����ǂ���</param>
	public void LanguageSetting(bool isNext)
	{
		if (isNext)
		{
			// ���̔ԍ��̌����
			LanguageManager.Language =
				(LanguageManager.LanguageType)(((int)LanguageManager.Language + 1) % ((int)LanguageManager.LanguageType.English + 1));
		}
		else
		{
			// �O�̔ԍ��̌���ɂ���
			LanguageManager.Language =
				(LanguageManager.LanguageType)(((int)LanguageManager.Language - 1) % ((int)LanguageManager.LanguageType.English + 1));
			// �O�̌��ꂪ�����ꍇ�Ō�̌���ɐݒ�
			if ((int)LanguageManager.Language == -1)
			{
				LanguageManager.Language = LanguageManager.LanguageType.English;
			}
		}

		// ����̓x�^�����̌���Ή�
		switch (LanguageManager.Language)
		{
		case LanguageManager.LanguageType.Japanese:
			languageTxt.text = "Japanese";
			break;
		case LanguageManager.LanguageType.English:
			languageTxt.text = "English";
			break;
		}
		// �摜�؂�ւ�
		iGM_Main?.SetControlSprite();
	}

	/// <summary>
	/// ���g���C�{�^��
	/// </summary>
	async public void Retry()
	{
		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �V�[���ړ��֎~
		LevelManager.canLevelMove = false;
		// �V�[���ړ�
		LevelManager.OpenLevel(token, "MainScene").Forget();
		// �t�F�[�h�I����҂�
		await FadeManager.Instance.FadeOutAsync();
		// �V�[���ǂݍ��ݏI���҂�
		await UniTask.WaitUntil(() => !LevelManager.IsLevelLoading, cancellationToken: token);
		// �V�[���ړ��\��Ԃɂ���
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// �^�C�g���ɖ߂�
	/// </summary>
	async public void ToTitle()
	{
		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �V�[���ړ��֎~
		LevelManager.canLevelMove = false;
		// �V�[���ړ�
		LevelManager.OpenLevel(token, "TitleScene").Forget();
		// �t�F�[�h�I����҂�
		await FadeManager.Instance.FadeOutAsync();
		// �V�[���ǂݍ��ݏI���҂�
		await UniTask.WaitUntil(() => !LevelManager.IsLevelLoading, cancellationToken: token);
		// �V�[���ړ��\��Ԃɂ���
		LevelManager.canLevelMove = true;
	}
}