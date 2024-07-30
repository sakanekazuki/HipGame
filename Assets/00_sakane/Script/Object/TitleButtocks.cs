using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// �^�C�g���̂��K
public class TitleButtocks : MonoBehaviour
{
	// ���K�摜
	[SerializeField]
	List<Sprite> buttocksSprites = new List<Sprite>();

	// �摜�\���N���X
	SpriteRenderer spriteRenderer;

	// ������܂ł̎���
	[SerializeField]
	float destroyTime = 5;

	private void Start()
	{
		// �摜�\���N���X�擾
		spriteRenderer = GetComponent<SpriteRenderer>();
		// �����_���Ō��߂�(�J������Ă��邨�K�̂�)
		var number = Random.Range(0, buttocksSprites.Count);

		if (GameInstance.saveData.isMakes[number])
		{
			// �J������Ă���ꍇ��
			spriteRenderer.color = Color.white;
		}
		else
		{
			// �J������Ă��Ȃ��ꍇ��
			spriteRenderer.color = Color.black;
		}
		// �摜�ݒ�
		spriteRenderer.sprite = buttocksSprites[number];

		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// ���b�㎩��������
		DestroyDelay(token).Forget();
	}

	/// <summary>
	/// �����܂ł̎���
	/// </summary>
	/// <returns></returns>
	async UniTask DestroyDelay(CancellationToken token)
	{
		// 5�b��
		await UniTask.WaitForSeconds(destroyTime, cancellationToken: token);
		// ���g������
		Destroy(gameObject);
	}
}