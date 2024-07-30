using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

// ���C���L�����o�X
public class MainCanvas : CanvasBase, IMainCanvas
{
	// �X�R�A�e�L�X�g
	[SerializeField]
	TextMeshProUGUI scoreTxt;
	// �n�C�X�R�A�̃e�L�X�g
	[SerializeField]
	TextMeshProUGUI highScoreTxt;

	// �R���{����\������e�L�X�g
	[SerializeField]
	TextMeshProUGUI comboValueTxt;
	// �ǉ������X�R�A�̃e�L�X�g
	[SerializeField]
	TextMeshProUGUI addScoreTxt;
	// ���K�ׂ̗ɕ\������ǉ������X�R�A�̃e�L�X�g
	[SerializeField]
	GameObject addScoreNextTxt;

	// �R���{�{����\������e�L�X�g
	[SerializeField]
	TextMeshProUGUI comboMagnificationTxt;

	// �h�炵���񐔂�\������e�L�X�g
	[SerializeField]
	TextMeshProUGUI shakeValueTxt;

	// ���{��̂Ƃ��̃e�L�X�g�̂���
	[SerializeField]
	GameObject shakeValueTxt_jp;
	// �p��̂Ƃ��̃e�L�X�g�̂���
	[SerializeField]
	GameObject shakeValueTxt_en;

	// ���ɗ��Ƃ����K
	[SerializeField]
	Image nextButtocksImg;

	// ��������摜�̈ʒu
	[SerializeField]
	GameObject controlImgPosition_jp;
	[SerializeField]
	GameObject controlImgPosition_en;
	// ��������摜
	[SerializeField]
	Image controlImg;

	// ��������摜
	[SerializeField]
	List<Sprite> controlSprites = new List<Sprite>();

	// ���Ԑ�����\������e�L�X�g
	[SerializeField]
	TextMeshProUGUI timeLimitTxt;
	// �I�t�Z�b�g
	[SerializeField]
	Vector3 addScoreNextTxtOffset = Vector3.zero;

	// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X
	IGM_Main iGM_Main;

	protected override void Start()
	{
		base.Start();
		// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X�擾
		iGM_Main = GM_Main.Instance.GetComponent<IGM_Main>();
		// ��������摜�ݒ�
		SetControlSprite();
	}

	/// <summary>
	/// �I�v�V�����{�^�����N���b�N
	/// </summary>
	public void OptionClick()
	{
		iGM_Main.OptionOpen();
	}

	/// <summary>
	/// �R���N�V�����{�^�����N���b�N
	/// </summary>
	public void CollectionClick()
	{
		// �R���N�V�������J��
		CollectionManager.Instance.CollectionCanvasOpen();
	}

	/// <summary>
	/// �X�R�A�e�L�X�g�ݒ�
	/// </summary>
	/// <param name="scoreText">�\������X�R�A</param>
	public void SetScoreText(string score)
	{
		scoreTxt.text = score;
	}

	/// <summary>
	/// �ǉ������X�R�A�ݒ�
	/// </summary>
	/// <param name="addScore">�ǉ������X�R�A</param>
	public void SetAddScoreTxt(string addScore)
	{
		addScoreTxt.text = "+" + addScore;
	}

	/// <summary>
	/// �ǉ������X�R�A���B��
	/// </summary>
	public void AddScoreTxtHide()
	{
		addScoreTxt.text = "";
	}

	/// <summary>
	/// �ǉ������X�R�A�ݒ�
	/// </summary>
	/// <param name="addScore">�ǉ������X�R�A</param>
	/// <param name="worldPosition">�ʒu</param>
	public void AddScoreNextTxt(string addScore, Vector3 worldPosition)
	{
		// �e�L�X�g����
		var obj = Instantiate(addScoreNextTxt, gameObject.transform);
		// �e�L�X�g
		obj.GetComponent<TextMeshProUGUI>().text = "+" + addScore;
		// �ʒu�w��
		obj.transform.position = worldPosition + addScoreNextTxtOffset;
		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �B��
		AddScoreNextTxtHide(token, obj).Forget();
	}

	/// <summary>
	/// �ǉ������X�R�A���B��
	/// </summary>
	/// <param name="token">�L�����Z���[�V�����g�[�N��</param>
	/// <param name="obj">�쐬�����I�u�W�F�N�g</param>
	public async UniTask AddScoreNextTxtHide(CancellationToken token, GameObject obj)
	{
		// �e�L�X�g�擾
		var textMeshPro = obj.GetComponent<TextMeshProUGUI>();
		while (true)
		{
			// 1�t���[���҂�
			await UniTask.DelayFrame(1);
			// 1�b�����ē����ɂ��Ă���
			textMeshPro.color -= new Color(0, 0, 0, 1.0f / 60.0f);
			// �����ɂȂ�����o��
			if (textMeshPro.color.a <= 0)
			{
				break;
			}
		}
		// �I�u�W�F�N�g�폜
		Destroy(obj);
		obj = null;
	}

	/// <summary>
	/// �n�C�X�R�A�ݒ�
	/// </summary>
	/// <param name="highScore">�\������n�C�X�R�A</param>
	public void SetHighScoreText(string highScore)
	{
		highScoreTxt.text = highScore;
	}

	/// <summary>
	/// �R���{���ݒ�
	/// </summary>
	/// <param name="value">�R���{��</param>
	public void SetComboValue(int value)
	{
		comboValueTxt.text = value.ToString();
	}

	/// <summary>
	/// �R���{�{���ݒ�
	/// </summary>
	/// <param name="magnification">�R���{�{��</param>
	public void SetComboMagnification(float magnification)
	{
		comboMagnificationTxt.text = "�~" + magnification.ToString();
	}

	/// <summary>
	/// �h�炵���񐔐ݒ�
	/// </summary>
	/// <param name="shakeValue">�h�炵����</param>
	public void SetShakeValue(int shakeValue)
	{
		shakeValueTxt.text = shakeValue.ToString();
	}

	/// <summary>
	/// ���ɗ��Ƃ����K�̉摜�ݒ�
	/// </summary>
	/// <param name="sprite">�ݒ肷��摜</param>
	public void SetNextButtocksImage(Sprite sprite)
	{
		nextButtocksImg.sprite = sprite;
	}

	/// <summary>
	/// ��������摜�ݒ�
	/// </summary>
	public void SetControlSprite()
	{
		// �摜�擾
		var sprite = controlSprites[(int)GameInstance.saveData.language];
		// �摜�T�C�Y�擾
		var spriteSize = sprite.bounds.size * 100;
		// �摜�T�C�Y�ݒ�
		controlImg.rectTransform.sizeDelta = spriteSize;
		// �摜�ݒ�
		controlImg.sprite = sprite;

		// ����ɂ���Ĉʒu��ς���
		if (GameInstance.saveData.language == LanguageManager.LanguageType.Japanese)
		{
			// �V���b�t���񐔂�\������ʒu�ݒ�
			shakeValueTxt.transform.position = shakeValueTxt_jp.transform.position;
			// ��������摜�̈ʒu�ݒ�
			controlImg.transform.position = controlImgPosition_jp.transform.position;
		}
		else
		{
			// �V���b�t���񐔂�\������ʒu�ݒ�
			shakeValueTxt.transform.position = shakeValueTxt_en.transform.position;
			// ��������摜�̈ʒu�ݒ�
			controlImg.transform.position = controlImgPosition_en.transform.position;
		}
	}

	/// <summary>
	/// ���Ԑ����\��
	/// </summary>
	/// <param name="time">����</param>
	public void SetTimeLimit(float time)
	{
		// �؂�グ
		var t = Mathf.CeilToInt(time);
		// �e�L�X�g�ɐݒ�
		timeLimitTxt.text = t.ToString();
	}

	/// <summary>
	/// ���Ԑ������Z�b�g
	/// </summary>
	public void TimeLimitReset()
	{
		timeLimitTxt.text = "";
	}
}