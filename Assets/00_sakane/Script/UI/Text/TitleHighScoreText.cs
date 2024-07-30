using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TitleHighScoreText : MonoBehaviour
{
	// �n�C�X�R�A��\������e�L�X�g
	TextMeshProUGUI highScoreTxt;

	// �A�j���[�^�[
	Animator animator;

	void Start()
	{
		// �e�L�X�g�擾
		highScoreTxt = GetComponent<TextMeshProUGUI>();
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
		// �A�j���[�V�������ēx�J�n
		animator.SetTrigger("Start");
	}
}
