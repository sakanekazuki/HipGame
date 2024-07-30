using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime;

// �R���{�{���e�L�X�g
public class ComboRatioTxt : MonoBehaviour
{
	// �R���{�e�L�X�g
	TextMeshProUGUI comboTxt;

	// �R���{�̓��e
	List<string> comboTxtContent = new List<string>()
	{
		"1Combo\n�~1.0",
		"2Combo\n�~1.1",
		"3Combo\n�~1.2",
		"4Combo\n�~1.3",
		"5Combo\n�~1.5"
	};

	// �R���{�̔ԍ�
	int comboNumber = 0;

	// �A�j���[�^�[
	Animator animator;

	private void Start()
	{
		// �e�L�X�g�擾
		comboTxt = GetComponent<TextMeshProUGUI>();
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
		// ���̃R���{�ɂ���
		++comboNumber;
		// �R���{���𒴂��Ȃ��悤�ɂ���
		comboNumber %= comboTxtContent.Count;
		// �e�L�X�g�ݒ�
		comboTxt.text = comboTxtContent[comboNumber];
		// �A�j���[�V�������ēx�J�n
		animator.SetTrigger("Start");
	}
}