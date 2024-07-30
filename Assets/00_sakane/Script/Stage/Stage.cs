using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// �X�e�[�W
public class Stage : Singleton<Stage>
{
	// ��
	[SerializeField]
	GameObject boxObj;

	// true = �h�点��
	[NonSerialized]
	public bool canShake = true;

	// �h��鑬�x
	[SerializeField]
	float speed = 0.08f;

	// �����ʒu
	Vector3 startPosition = Vector3.zero;

	// ���K�摜
	[SerializeField]
	List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
	List<Animator> rightButtockAnimator = new List<Animator>();

	// �ő�p�x
	[SerializeField]
	float maxAngle = 15.0f;

	// true = �h�炵�Ă���
	public bool IsShaking
	{
		get;
		private set;
	}

	private void Start()
	{
		// �����ʒu�ݒ�
		startPosition = boxObj.transform.position;
		var isMakes = GameInstance.saveData.isMakes;
		for (int i = 0; i < isMakes.Count; ++i)
		{
			if (isMakes[i])
			{
				// ���K������Ă���ꍇ��
				spriteRenderers[i].color = Color.white;
			}
			else
			{
				// ���K������Ă��Ȃ��ꍇ��
				spriteRenderers[i].color = Color.black;
			}
		}
		IsShaking = false;

		// ���K�̃A�j���[�^�[�擾
		foreach (var v in spriteRenderers)
		{
			rightButtockAnimator.Add(v.GetComponent<Animator>());
		}
	}

	/// <summary>
	/// �h�炷
	/// </summary>
	public void Shake()
	{
		var token = this.GetCancellationTokenOnDestroy();
		ShakeAsync(token).Forget();
	}

	/// <summary>
	/// �h�炷
	/// </summary>
	/// <returns></returns>
	async UniTask ShakeAsync(CancellationToken token)
	{
		IsShaking = true;
		float progress = 0;
		while (true)
		{
			boxObj.transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(progress) * maxAngle);

			// �ړ�������
			//boxObj.transform.position =
			//	new Vector3(startPosition.x + (Mathf.Sin(progress) * swingWidth), startPosition.y);

			// 1�t���[���҂�
			await UniTask.DelayFrame(1, cancellationToken: token);
			// �i����i�߂�
			progress += speed;
			// �i����4�΁i2���j������I���
			if (progress >= 4 * Mathf.PI)
			{
				break;
			}
		}
		// �J�n�ʒu�ɖ߂�
		boxObj.transform.position = startPosition;
		// ��]��߂�
		boxObj.transform.localEulerAngles = Vector3.zero;

		IsShaking = false;
	}

	/// <summary>
	/// ���K�������
	/// </summary>
	/// <param name="number">��������K�̔ԍ�</param>
	public void MakeButtocks(int number)
	{
		spriteRenderers[number].color = Color.white;
		GameInstance.saveData.isMakes[number] = true;
	}

	/// <summary>
	/// �E�̂��K��h�炷
	/// </summary>
	/// <param name="number">�h�炷���K�̔ԍ�</param>
	public void RightButtockShake(int number)
	{
		rightButtockAnimator[number].SetTrigger("IsShake");
	}
}