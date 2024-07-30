using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �t�F�[�h�L�����o�X
public class FadeIOCanvas : CanvasBase, IFadeIOCanvas
{
	static Sprite sprite = null;

	// �t�F�[�h�摜
	[SerializeField]
	Image fadeImg;

	// �t�F�[�h�̉摜
	[SerializeField]
	List<Sprite> fadeImgSprites = new List<Sprite>();

	// �摜�̍ő�T�C�Y
	[SerializeField]
	Vector2 maxSize = Vector2.zero;

	protected override void Start()
	{
		base.Start();
	}

	/// <summary>
	/// ������
	/// </summary>
	public void Initialize()
	{
		if (!sprite)
		{
			ButtocksDecide();
		}
		// �摜�ݒ�
		fadeImg.sprite = sprite;
		// �t�F�[�h�Ɏg�����K����
		ButtocksDecide();
		// �T�C�Y�ݒ�
		(fadeImg.transform as RectTransform).sizeDelta = maxSize;
	}

	/// <summary>
	/// �t�F�[�h�Ɏg�p���邨�K�̌���
	/// </summary>
	void ButtocksDecide()
	{
		// ���K�̔ԍ�
		var number = Random.Range(0, fadeImgSprites.Count);

		// �摜����ȏ�J������Ă���ꍇ
		if (GameInstance.saveData.isMakes.Contains(true))
		{
			// �J������Ă���ԍ����o��܂ŉ�
			while (!GameInstance.saveData.isMakes[number])
			{
				// �ԍ���ς���
				number = Random.Range(0, fadeImgSprites.Count);
			}
			// �X�v���C�g�̐ݒ�
			sprite = fadeImgSprites[number];
		}
		// �摜������J������Ă��Ȃ��ꍇ
		else
		{
			// �X���C���Œ�
			sprite = fadeImgSprites[0];
			// �摜�͍�
			fadeImg.color = Color.black;
		}
	}

	/// <summary>
	/// �񓯊��t�F�[�h�C��
	/// </summary>
	/// <returns></returns>
	async public UniTask FadeIn()
	{
		if (GameInstance.saveData.isMakes.Contains(true))
		{
			fadeImg.color = Color.white;
		}
		else
		{
			fadeImg.color = Color.black;
		}
		// �L�����Z���[�V�����g�[�N��
		var token = this.GetCancellationTokenOnDestroy();
		// RectTransfor�擾
		var rect = (fadeImg.transform as RectTransform);
		// �䗦���擾
		var ratio = rect.sizeDelta.y / rect.sizeDelta.x;
		while (rect.sizeDelta.x >= 0 && rect.sizeDelta.y >= 0)
		{
			// 1�t���[���҂�
			await UniTask.DelayFrame(1, cancellationToken: token);
			// ���������Ă���
			rect.sizeDelta -= new Vector2(FadeManager.Instance.FadeSpeed, FadeManager.Instance.FadeSpeed * ratio);
		}
		// �T�C�Y�ݒ�
		rect.sizeDelta = Vector2.zero;
	}

	/// <summary>
	/// �񓯊��t�F�[�h�A�E�g
	/// </summary>
	/// <returns></returns>
	async public UniTask FadeOut()
	{
		if (GameInstance.saveData.isMakes.Contains(true))
		{
			fadeImg.color = Color.white;
		}
		else
		{
			fadeImg.color = Color.black;
		}

		// �L�����Z���[�V�����g�[�N��
		var token = this.GetCancellationTokenOnDestroy();

		fadeImg.sprite = sprite;
		// �䗦���擾
		var ratio = fadeImg.sprite.bounds.size.y / fadeImg.sprite.bounds.size.x;
		// RectTransfor�擾
		var rect = (fadeImg.transform as RectTransform);
		while (rect.sizeDelta.x <= maxSize.x || rect.sizeDelta.y <= maxSize.y)
		{
			// 1�t���[���҂�
			await UniTask.DelayFrame(1, cancellationToken: token);
			// �傫������
			rect.sizeDelta += new Vector2(FadeManager.Instance.FadeSpeed, FadeManager.Instance.FadeSpeed * ratio);
		}
		// �T�C�Y�ݒ�
		rect.sizeDelta = maxSize;
	}
}