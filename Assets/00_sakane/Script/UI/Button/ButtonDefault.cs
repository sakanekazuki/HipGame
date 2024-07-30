using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// �W���{�^��
public class ButtonDefault : MonoBehaviour
{
	// true = �L��
	bool isEnable = true;
	public bool IsEnable
	{
		get => isEnable;
		set
		{
			isEnable = value;
			if (value) { }
			else
			{
				if (isOnCursor)
				{
					isOnCursor = false;
					animator.SetBool("IsOnCursor", isOnCursor);
				}
			}
		}
	}

	// �A�j���[�^�[
	Animator animator;

	// true = �J�[�\���������Ă�����
	bool isOnCursor = false;

	// �N���b�N���̉�
	[SerializeField]
	AudioClip clickClip;

	private void Start()
	{
		// �A�j���[�^�[�擾
		animator = GetComponent<Animator>();
	}

	/// <summary>
	/// �N���b�N��
	/// </summary>
	public void Click()
	{
		SoundManager.Instance.SEPlay(clickClip);
	}

	/// <summary>
	/// �J�[�\�����������Ƃ�
	/// </summary>
	public void OnCursor()
	{
		if (!isEnable)
		{
			return;
		}
		// �J�[�\���������Ă����Ԃɂ���
		isOnCursor = true;
		// �A�j���[�V�����̃p�����[�^�[�ݒ�
		animator.SetBool("IsOnCursor", isOnCursor);
	}

	/// <summary>
	/// �J�[�\�������ꂽ�Ƃ�
	/// </summary>
	public void ReleseCursor()
	{
		if (!isEnable)
		{
			return;
		}
		// �J�[�\���������Ă��Ȃ���Ԃɂ���
		isOnCursor = false;
		// �A�j���[�V�����̃p�����[�^�[�ݒ�
		animator.SetBool("IsOnCursor", isOnCursor);
	}
}
