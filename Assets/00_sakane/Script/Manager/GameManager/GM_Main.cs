using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

// �Q�[���}�l�[�W���[
public class GM_Main : GameManagerBase, IGM_Main
{
	// �K�C�h���C���I�u�W�F�N�g
	[SerializeField]
	GameObject guideLineObjectPrefab;
	GameObject guideLineObject;

	// ���C���L�����o�X
	[SerializeField]
	GameObject mainCanvasPrefab;
	GameObject mainCanvas;

	// �I�v�V�����L�����o�X
	[SerializeField]
	GameObject optionCanvasPrefab;
	GameObject optionCanvas;

	// ���U���g�L�����o�X
	[SerializeField]
	GameObject resultCanvasPrefab;
	GameObject resultCanvas;

	// �O�܂ł̃X�R�A
	float beforeScore = 0;
	// �X�R�A
	float score = 0;
	public float Score
	{
		get => score;
	}
	// �X�R�A��ǉ����Ă������x
	[SerializeField]
	float addScoreSpeed = 0.08f;
	// �ō��X�R�A
	int highScore = 0;
	public int HighScore
	{
		get => highScore;
	}

	// true = �n�C�X�R�A�X�V
	bool isNewHighScore = false;
	public bool IsNewHighScore
	{
		get => isNewHighScore;
	}

	// �R���{��
	int comboValue = 0;
	// �R���{�ɂ��{��
	List<float> magnifications = new List<float>()
	{
		1.0f,
		1.1f,
		1.2f,
		1.3f,
		1.5f
	};

	// �h�炵����
	int shakeValue = 0;
	// �h�炷���Ƃ̂ł���ő吔
	[SerializeField]
	int maxShakeValue = 5;
	public int MaxShakeValue
	{
		get => maxShakeValue;
	}

	// �V���b�t������SE
	[SerializeField]
	AudioClip shakeClip;

	// �Q�[���J�n��
	[SerializeField]
	AudioClip gameStartClip;

	// �Q�[���I�[�o�[��BGM
	[SerializeField]
	AudioClip gameOverClip;

	// true = �Q�[���I�[�o�[
	public bool IsGameOver
	{
		get;
		private set;
	}

	// ���C���L�����o�X�̃C���^�[�t�F�C�X
	IMainCanvas iMainCanvas;

	private void Awake()
	{
		IsGameOver = false;
		// �K�C�h���C���I�Ȃ��̐���
		guideLineObject = Instantiate(guideLineObjectPrefab);

		// ���C���L�����o�X����
		mainCanvas = Instantiate(mainCanvasPrefab);
		// ���C���L�����o�X�\��
		mainCanvas.SetActive(true);
		// �C���^�[�t�F�C�X�擾
		iMainCanvas = mainCanvas.GetComponent<IMainCanvas>();

		// �I�v�V�����L�����o�X����
		optionCanvas = Instantiate(optionCanvasPrefab);
		// �I�v�V�����L�����o�X��\��
		optionCanvas.SetActive(false);

		// ���U���g�L�����o�X����
		resultCanvas = Instantiate(resultCanvasPrefab);
		// ���U���g�L�����o�X��\��
		resultCanvas.SetActive(false);
	}

	protected override void Start()
	{
		base.Start();

		// �ۑ�����Ă���f�[�^������
		highScore = GameInstance.saveData.highScore;
		// �X�R�A�e�L�X�g������
		iMainCanvas.SetScoreText("0");
		// �n�C�X�R�A�ݒ�
		iMainCanvas.SetHighScoreText(highScore.ToString());
		// �R���{���ݒ�
		iMainCanvas.SetComboValue(0);
		// �{����������
		iMainCanvas.SetComboMagnification(1);
		// �V���b�t���񐔐ݒ�
		iMainCanvas.SetShakeValue(maxShakeValue);
		// �Q�[���J�n���Đ�
		SoundManager.Instance.SEPlay(gameStartClip);
	}

	protected override void Update()
	{
		base.Update();
		if (Keyboard.current.escapeKey.wasPressedThisFrame)
		{
#if UNITY_EDITOR

			UnityEditor.EditorApplication.isPlaying = false;

#endif
		}
	}

	/// <summary>
	/// �X�R�A�������Âǉ����Ă���
	/// </summary>
	/// <param name="token">�L�����Z���[�V�����g�[�N��</param>
	/// <returns></returns>
	async UniTask AddScoreAsync(CancellationToken token)
	{
		// �i��
		var progress = 0.0f;
		while (true)
		{
			// �i�߂����̃X�R�A���擾
			var s = Mathf.Lerp(beforeScore, score, progress);

			// �؂�̂�
			int dispScore = Mathf.FloorToInt(s);
			// �X�R�A�\��
			iMainCanvas.SetScoreText(dispScore.ToString());
			// 1�t���[���҂�
			await UniTask.DelayFrame(1, cancellationToken: token);
			// �i�߂�
			progress += addScoreSpeed;
			// �\�����Ă���X�R�A�����݂̃X�R�A�ȏ�ɂȂ����ꍇ�o��
			if (s >= Mathf.FloorToInt(score))
			{
				//�؂�̂�
				var sc = Mathf.FloorToInt(score);
				// �؂�̂Ă����̂�\��
				iMainCanvas.SetScoreText(sc.ToString());
				// �ǉ�����e�L�X�g��\��
				iMainCanvas.AddScoreTxtHide();
				break;
			}
		}
	}

	/// <summary>
	/// �v���C���[�̏�Ԑݒ�
	/// </summary>
	/// <param name="isActive">�ݒ肷��v���C���[�̏��</param>
	public void SetPlayerState(bool isActive)
	{
		character.SetActive(isActive);
	}

	/// <summary>
	/// �K�C�h���C���̈ʒu�ݒ�
	/// </summary>
	/// <param name="position">�ʒu</param>
	public void SetGuideLinePosition(Vector2 position)
	{
		guideLineObject.transform.position = position;
	}

	/// <summary>
	/// �K�C�h���C����\��
	/// </summary>
	public void GuideLineHide()
	{
		guideLineObject.SetActive(false);
	}

	/// <summary>
	/// �K�C�h���C���\��
	/// </summary>
	public void GuideLineShow()
	{
		guideLineObject.SetActive(true);
	}

	/// <summary>
	/// �I�v�V�������J��
	/// </summary>
	public void OptionOpen()
	{
		optionCanvas.SetActive(true);
		// �L�����N�^�[�I�t
		SetPlayerState(false);
	}

	/// <summary>
	/// �I�v�V���������
	/// </summary>
	public void OptionClose()
	{
		optionCanvas.SetActive(false);
		// �v���C���[�L�����N�^�[���I��
		SetPlayerState(true);
	}

	/// <summary>
	/// �V���b�t��
	/// </summary>
	public void Shake()
	{
		// �ő吔�𒴂���Ɨh�点�Ȃ�
		if (shakeValue >= maxShakeValue)
		{
			return;
		}
		// �h�炷
		Stage.Instance.Shake();
		// SE��炷
		SoundManager.Instance.SEPlay(shakeClip);
		// �h�炵���񐔂�ǉ�
		++shakeValue;

		// �h�炵���񐔐ݒ�
		iMainCanvas.SetShakeValue(maxShakeValue - shakeValue);
	}

	/// <summary>
	/// �Q�[���I�[�o�[
	/// </summary>
	public void GameOver()
	{
		IsGameOver = true;
		// �X�R�A
		if (score > highScore)
		{
			// �n�C�X�R�A�X�V
			highScore = Mathf.FloorToInt(score);
			// �n�C�X�R�A���L�^
			GameInstance.saveData.highScore = highScore;
			// �n�C�X�R�A�X�V�t���Otrue
			isNewHighScore = true;
		}
		else
		{
			// �n�C�X�R�A�X�V�t���Ofalse
			isNewHighScore = false;
		}
		// �I�v�V�������J���Ă���ꍇ��\���ɂ���
		optionCanvas.SetActive(false);
		// �R���N�V��������\��
		CollectionManager.Instance.CollectionCanvasClose();
		// ���U���g�\��
		resultCanvas.SetActive(true);
		// BGM�ύX
		SoundManager.Instance.BGMChange(gameOverClip).Forget();
	}

	/// <summary>
	/// �X�R�A��ǉ�����
	/// </summary>
	/// <param name="score">�ǉ�����X�R�A</param>
	public void AddScore(int score)
	{
		// �O�܂ł̃X�R�A��ݒ�
		beforeScore = this.score;
		// float�Ōv�Z���邽�߂ɑ��
		float addscore = score;

		if (comboValue > magnifications.Count - 1)
		{
			comboValue = magnifications.Count - 1;
		}
		// �R���{�Œǉ�
		addscore *= magnifications[comboValue];
		// �X�R�A�𑫂�
		this.score += addscore;
		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �X�R�A�������Âǉ�����
		AddScoreAsync(token).Forget();
		// �؂�̂�
		int ads = Mathf.FloorToInt(addscore);
		// �ǉ�����X�R�A����\��
		iMainCanvas.SetAddScoreTxt(ads.ToString());
	}

	/// <summary>
	/// �ǉ������X�R�A�̕\��
	/// </summary>
	/// <param name="score">�ǉ������X�R�A</param>
	/// <param name="worldPosition">�ʒu</param>
	public void AddScoreNextTxt(int score, Vector3 worldPosition)
	{
		iMainCanvas.AddScoreNextTxt(score.ToString(), worldPosition);
	}

	/// <summary>
	/// �R���{���擾
	/// </summary>
	public int GetComboValue()
	{
		return comboValue;
	}

	/// <summary>
	/// �R���{�����Z�b�g
	/// </summary>
	public void ComboReset()
	{
		comboValue = 0;
		// �R���{�����Z�b�g
		iMainCanvas.SetComboValue(comboValue);
		// �R���{�{�������Z�b�g
		iMainCanvas.SetComboMagnification(1);
	}

	/// <summary>
	/// �R���{���ǉ�
	/// </summary>
	public void AddComboValue()
	{
		++comboValue;
		// �R���{���\��
		iMainCanvas.SetComboValue(comboValue);
	}

	/// <summary>
	/// �R���{�{���ݒ�
	/// </summary>
	public void SetComboMagnification()
	{
		iMainCanvas.SetComboMagnification(magnifications[comboValue]);
	}

	/// <summary>
	/// ���ɗ��Ƃ����K�̉摜�ݒ�
	/// </summary>
	/// <param name="sprite">�ݒ肷��摜</param>
	public void SetNextButtocksImage(Sprite sprite)
	{
		iMainCanvas.SetNextButtocksImage(sprite);
	}

	/// <summary>
	/// ��������摜�ݒ�
	/// </summary>
	public void SetControlSprite()
	{
		iMainCanvas.SetControlSprite();
	}

	/// <summary>
	/// �^�C�����~�b�g�ݒ�
	/// </summary>
	/// <param name="time">����</param>
	public void SetTimeLimit(float time)
	{
		if (!ButtocksManager.Instance.CanDrop)
		{
			return;
		}
		iMainCanvas.SetTimeLimit(time);
	}

	/// <summary>
	/// ���Ԑ������Z�b�g
	/// </summary>
	public void TimeLimitReset()
	{
		iMainCanvas.TimeLimitReset();
	}
}