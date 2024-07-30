using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// �ǉ������X�R�A�������摜
public class TitleAddScoreImg : MonoBehaviour
{
	// �ǉ������X�R�A�������摜
	Image addScoreImg;

	// �A�j���[�^�[
	Animator animator;

	// �摜�̔ԍ�
	int number = 0;

	void Start()
	{
		// �e�L�X�g�擾
		addScoreImg = GetComponent<Image>();
		// �A�j���[�^�[�擾
		animator = GetComponent<Animator>();

		// �J������Ă���ꍇ��
		if (GameInstance.saveData.isMakes[0])
		{
			addScoreImg.color = Color.white;
		}
		// �J������Ă��Ȃ��ꍇ��
		else
		{
			addScoreImg.color = Color.black;
		}

		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �A�j���[�V�����J�n
		StartAsync(token).Forget();
	}

	/// <summary>
	/// �����҂��ăA�j���[�V�����J�n
	/// </summary>
	/// <param name="token">�L�����Z���[�V�����g�[�N��</param>
	/// <returns></returns>
	async UniTask StartAsync(CancellationToken token)
	{
		// �t�F�[�h�I���҂�
		await UniTask.WaitUntil(() => !FadeManager.Instance.IsFading, cancellationToken: token);
		// �t�F�[�h�シ�����҂�
		await UniTask.WaitForSeconds(0.3f, cancellationToken: token);
		// �A�j���[�V�����J�n
		animator.SetTrigger("Start");
	}

	/// <summary>
	/// �A�j���[�V�����I��
	/// </summary>
	public void AnimationFinish()
	{
		// �ԍ���i�߂�
		++number;
		// �摜���𒴂��Ȃ��悤�ɂ���
		number %= (GM_Title.Instance as GM_Title).ButtocksSprites.Count;
		// �摜�ݒ�
		addScoreImg.sprite = (GM_Title.Instance as GM_Title).ButtocksSprites[number];
		// �J������Ă���ꍇ��
		if (GameInstance.saveData.isMakes[number])
		{
			addScoreImg.color = Color.white;
		}
		// �J������Ă��Ȃ��ꍇ��
		else
		{
			addScoreImg.color = Color.black;
		}
		// �A�j���[�V�������ēx�J�n
		animator.SetTrigger("Start");
	}
}
