using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// ���K
public class Buttocks : MonoBehaviour, IButtocks
{
	// ���݂̎��
	ButtocksManager.ButtocksType nowType = ButtocksManager.ButtocksType.Type2;

	// ����
	Rigidbody2D rb2d;
	// �摜
	SpriteRenderer spriteRenderer;
	// �R���W����
	[SerializeField]
	List<Collider2D> collider2d = new List<Collider2D>();
	// �A�j���[�V����
	Animator animator;

	// true = �A�j���[�V�����I�����
	bool isAnimFinish = true;

	// true = �v�[�����
	bool isPool = true;

	// true = ���߂ďՓ˂���
	bool isFarstHit = true;

	// �f�t�H���g�̃T�C�Y
	Vector2 defaultSize = Vector2.zero;

	// �Փˎ���SE
	[SerializeField]
	AudioClip hitClip;

	// ���̃t���[���ŏՓ˂����I�u�W�F�N�g
	List<GameObject> thisFrameHitObject = new List<GameObject>();

	// true = ����点��
	bool canSoundPlay = true;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (isPool)
		{
			return;
		}

		if (isFarstHit)
		{
			// ���Ƃ����Ԃɂ���
			ButtocksManager.Instance.CanDrop = true;
			// �A�j���[�V����������
			animator.SetTrigger("isHit");
		}

		if (isAnimFinish)
		{
			// �A�j���[�V�����ł��Ȃ���Ԃɂ���
			isAnimFinish = false;
		}
		// ��x����������Ԃɂ���
		isFarstHit = false;

		if (canSoundPlay)
		{
			// ����炷
			SoundManager.Instance.SEPlay(hitClip);
			// �点�Ȃ���Ԃɂ���
			canSoundPlay = false;
			// ���ɉ����Đ�����܂ő҂�
			var token = this.GetCancellationTokenOnDestroy();
			SoundDelay(token).Forget();
		}

		if (!collision.gameObject.CompareTag("Buttocks"))
		{
			return;
		}

		if (transform.position.y > collision.transform.position.y)
		{
			// �Փ˂��}�l�[�W���[�ɒm�点��
			ButtocksManager.Instance.ButtocksHit(gameObject, collision.gameObject);
		}
		else
		{
			// �Փ˂��}�l�[�W���[�ɒm�点��
			ButtocksManager.Instance.ButtocksHit(collision.gameObject, gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (isPool)
		{
			return;
		}
		// �Փ˂����I�u�W�F�N�g��ǉ�
		thisFrameHitObject.Add(collision.gameObject);
	}

	private void LateUpdate()
	{
		if (isPool)
		{
			return;
		}
		foreach (var v in thisFrameHitObject)
		{
			if (!v)
			{
				continue;
			}
			if (v.CompareTag("Buttocks"))
			{
				if (transform.position.y > v.transform.position.y)
				{
					// �Փ˂��}�l�[�W���[�ɒm�点��
					ButtocksManager.Instance.ButtocksHit(gameObject, v.gameObject);
				}
				else
				{
					// �Փ˂��}�l�[�W���[�ɒm�点��
					ButtocksManager.Instance.ButtocksHit(v.gameObject, gameObject);
				}
			}
		}
		thisFrameHitObject.Clear();
	}

	//private void OnCollisionStay2D(Collision2D collision)
	//{
	//	if (!collision.transform.parent)
	//	{
	//		return;
	//	}
	//	if ((collision.transform.parent.gameObject == gameObject) || isPool)
	//	{
	//		return;
	//	}

	//	if (collision.gameObject.CompareTag("Buttocks"))
	//	{
	//		ButtocksManager.Instance.ButtocksHit(gameObject, collision.gameObject);
	//	}
	//}

	/// <summary>
	/// �A�j���[�V�����I��
	/// </summary>
	public void AnimFinish()
	{
		var token = this.GetCancellationTokenOnDestroy();
		AnimFinishDelay(token).Forget();
	}

	/// <summary>
	/// �A�j���[�V�����I����̃f�B���C
	/// </summary>
	/// <param name="token">�L�����Z���[�V�����g�[�N��</param>
	/// <returns></returns>
	async UniTask AnimFinishDelay(CancellationToken token)
	{
		await UniTask.WaitForSeconds(0.3f, cancellationToken: token);
		isAnimFinish = true;
	}

	/// <summary>
	/// ����炷�f�B���C
	/// </summary>
	/// <returns></returns>
	async UniTask SoundDelay(CancellationToken token)
	{
		await UniTask.WaitForSeconds(1, cancellationToken: token);
		canSoundPlay = true;
	}

	/// <summary>
	/// ������
	/// </summary>
	public void Initialize()
	{
		// �����擾
		rb2d = GetComponent<Rigidbody2D>();
		// �摜�N���X�擾
		spriteRenderer = GetComponent<SpriteRenderer>();

		foreach (var col in collider2d)
		{
			// �R���C�_�[����
			col.enabled = false;
		}
		// �A�j���[�^�[�擾
		animator = GetComponent<Animator>();
	}

	/// <summary>
	/// �������~�߂�
	/// </summary>
	public void MoveStop()
	{
		// �ړ����~�߂�
		rb2d.velocity = Vector2.zero;
		// ��]���~�߂�
		rb2d.angularVelocity = 0;
	}

	/// <summary>
	/// �X�e�[�^�X�ݒ�
	/// </summary>
	/// <param name="status">�ݒ肷��X�e�[�^�X</param>
	public void SetStatus(ButtocksStatus status)
	{
		// ���
		nowType = status.type;
		// �d��
		rb2d.mass = status.mass;
		// �摜
		spriteRenderer.sprite = status.sprite;

		// �ʒu�ݒ�
		collider2d[0].transform.localPosition = new Vector2(-status.colliderPosition.x, status.colliderPosition.y);
		collider2d[1].transform.localPosition = new Vector2(status.colliderPosition.x, status.colliderPosition.y);

		// �T�C�Y�ݒ�
		(collider2d[0] as CapsuleCollider2D).size = status.size;
		(collider2d[1] as CapsuleCollider2D).size = status.size;
	}

	/// <summary>
	/// ���Ƃ�
	/// </summary>
	public void Drop()
	{
		// �d�͂�߂�
		rb2d.gravityScale = 1;

		foreach (var col in collider2d)
		{
			// �R���C�_�[�L��
			col.enabled = true;
		}
	}

	/// <summary>
	/// �v�[����Ԏ擾
	/// </summary>
	/// <returns>�v�[�����</returns>
	public bool GetIsPool()
	{
		return isPool;
	}

	/// <summary>
	/// �v�[����Ԃ̐ݒ�
	/// </summary>
	/// <param name="isPool">�v�[�����</param>
	public void SetIsPool(bool isPool)
	{
		this.isPool = isPool;
		// �v�[����Ԃ�������R���W�����𖳌��ɂ��āA�d�͂��Ȃ��ɂ���
		if (isPool)
		{
			foreach (var col in collider2d)
			{
				// �R���W��������
				col.enabled = false;
			}
			// �d�͂Ȃ�
			rb2d.gravityScale = 0;
			// �������~�߂�
			MoveStop();
			// ��]�����Ƃɖ߂�
			transform.localEulerAngles = Vector3.zero;
		}
	}

	/// <summary>
	/// �i��
	/// </summary>
	public void Evolution()
	{
		if (isPool)
		{
			return;
		}
		// �Փˏ��
		List<RaycastHit2D> hit2d = new List<RaycastHit2D>();
		foreach (var col in collider2d)
		{
			// �J�v�Z���ɕϊ�
			var capsule = col as CapsuleCollider2D;
			// �Փˏ���o�^
			foreach (var v in Physics2D.CapsuleCastAll(
				col.transform.position,
				capsule.size + new Vector2(0.1f, 0.1f),
				CapsuleDirection2D.Horizontal,
				0,
				new Vector2(0.1f, 0.1f),
				LayerMask.GetMask("Buttocks")))
			{
				if (v.collider.transform.parent.gameObject == gameObject)
				{
					continue;
				}
				if (v.collider.gameObject.CompareTag("Buttocks"))
				{
					hit2d.Add(v);
				}
			}
		}
		// �Փˏ��̒��ɂ��K�����邩���ׂ�
		foreach (var v in hit2d)
		{
			if (v.collider.gameObject == null)
			{
				continue;
			}
			if (v.collider.transform.parent.GetComponent<IButtocks>().GetIsPool())
			{
				continue;
			}
			// �Փ˂�m�点��
			if (ButtocksManager.Instance.ButtocksHit(gameObject, v.collider.transform.parent.gameObject))
			{
				break;
			}
		}
	}

	/// <summary>
	/// ���K�̎�ގ擾
	/// </summary>
	/// <returns>���݂̎��</returns>
	ButtocksManager.ButtocksType IButtocks.GetType()
	{
		return nowType;
	}

	/// <summary>
	/// �Փˏ�Ԏ擾
	/// </summary>
	/// <returns></returns>
	public bool GetIsFarstHit()
	{
		return isFarstHit;
	}

	/// <summary>
	/// �Փˏ�Ԑݒ�
	/// </summary>
	/// <param name="isFarstHit">�ݒ肷����</param>
	public void SetIsFarstHit(bool isFarstHit)
	{
		this.isFarstHit = isFarstHit;
	}
}