using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// �^�C�g���̎��̂��K
public class TitleNextButtocks : MonoBehaviour
{
	// �摜
	Image image;
	// �A�j���[�^�[
	Animator animator;

	// �J������Ă��邨�K�̉摜�i�J������Ă��Ȃ����̂���܂ށj
	List<Sprite> openButtocksSprite = new List<Sprite>();
	// �\�����Ă��邨�K�̔ԍ�
	int number = 0;

	private void Start()
	{
		// �摜�擾
		image = GetComponent<Image>();
		// �A�j���[�^�[�擾
		animator = GetComponent<Animator>();

		for (int i = 0; i < GameInstance.saveData.isMakes.Count; ++i)
		{
			// �J������Ă��邨�K�̉摜�o�^
			openButtocksSprite.Add((GM_Title.Instance as GM_Title).ButtocksSprites[i]);
			if (!GameInstance.saveData.isMakes[i])
			{
				break;
			}
		}
		// �ԍ���������-1�v�ɂ��Ď��̂��K��ݒ肷��ۂ�0�ɂȂ�悤�ɂ���
		number = -1;
		// ���̂��K�ݒ�
		NextButtocks();

		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �t�F�[�h���I���܂ő҂��Ă���A�j���[�V����
		StartAsync(token).Forget();
	}

	/// <summary>
	/// �t�F�[�h���I���܂ő҂�
	/// </summary>
	/// <param name="token">�L�����Z���[�V�����g�[�N��</param>
	/// <returns></returns>
	async UniTask StartAsync(CancellationToken token)
	{
		// �t�F�[�h�I���܂ő҂�
		await UniTask.WaitUntil(() => !FadeManager.Instance.IsFading, cancellationToken: token);
		// �t�F�[�h�シ�����҂�
		await UniTask.WaitForSeconds(0.3f, cancellationToken: token);
		// ������
		animator.SetTrigger("Move");
	}

	/// <summary>
	/// ���̂��K�ɕύX
	/// </summary>
	void NextButtocks()
	{
		// ���̔ԍ��ɂ���
		++number;
		// �摜�̐��𒴂��Ȃ��悤�ɂ���
		number %= openButtocksSprite.Count;
		// �摜�ݒ�
		image.sprite = openButtocksSprite[number];

		// ��{�I�ɔ�
		image.color = Color.white;
		// �J������Ă��Ȃ����K�����݂���ꍇ
		if (GameInstance.saveData.isMakes.Contains(false))
		{
			// �J������Ă��Ȃ��ꍇ�Ō�̉摜�͍�������
			if (number == openButtocksSprite.Count - 1)
			{
				image.color = Color.black;
			}
		}
	}

	/// <summary>
	/// �A�j���[�V�����I��
	/// </summary>
	public void AnimFinish()
	{
		// ���̉摜�Ɉڍs
		NextButtocks();
		// ���̃A�j���[�V�������w����
		animator.SetTrigger("Move");
	}
}