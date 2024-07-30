using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

// �^�C�g���̂����������ɒǉ������X�R�A��\������e�L�X�g
public class TitleAddScoreText : MonoBehaviour
{
	// �ǉ������X�R�A�̃e�L�X�g
	TextMeshProUGUI addScoreTxt;

	// �ǉ������X�R�A�̃e�L�X�g
	List<string> addScoreList = new List<string>()
	{
		"+1",
		"+3",
		"+6",
		"+10",
		"+15",
		"+21",
		"+28",
		"+36",
		"+45",
		"+55",
		"+66",
	};

	// �A�j���[�^�[
	Animator animator;

	// �ǉ������X�R�A�̔ԍ�
	int addScoreNumber = 0;

	private void Start()
	{
		// �e�L�X�g�擾
		addScoreTxt = GetComponent<TextMeshProUGUI>();
		// �A�j���[�^�[�擾
		animator = GetComponent<Animator>();
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
		// ���̔ԍ��̃X�R�A�ɂ���
		++addScoreNumber;
		// �X�R�A���𒴂��Ȃ��悤�ɂ���
		addScoreNumber %= addScoreList.Count;
		// �e�L�X�g��ݒ�
		addScoreTxt.text = addScoreList[addScoreNumber];

		// �A�j���[�V�������ēx�J�n
		animator.SetTrigger("Start");
	}
}
