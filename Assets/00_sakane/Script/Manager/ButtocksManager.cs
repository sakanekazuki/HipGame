using System.Collections.Generic;
using UnityEngine;

// �I�u�W�F�N�g�Ǘ��N���X
public class ButtocksManager : ManagerBase<ButtocksManager>
{
	// ���K�̎��
	public enum ButtocksType
	{
		Type2,
		Type3,
		Type4,
		Type5,
		Type6,
		Type7,
		Type8,
		Type9,
		Type10,
		Type11,
		Type12,
	}

	// ���K�̃v���n�u
	[SerializeField]
	GameObject buttocksPrefab;

	// �v�[�����Ă����ʒu
	[SerializeField]
	GameObject poolPosition;

	// ���K�̃C���^�[�t�F�C�X
	Dictionary<GameObject, IButtocks> iButtocks = new Dictionary<GameObject, IButtocks>();

	// ���Ƃ����K�ɑI�΂��ő�l
	[SerializeField]
	ButtocksType dropMaxType = ButtocksType.Type5;

	// ���ɗ��Ƃ����K
	GameObject nextButtocks = null;
	// ���Ƃ����K
	GameObject dropButtocks = null;

	// ���K�̃X�e�[�^�X
	[SerializeField]
	SO_ButtocksStatuses buttocksStatuses;

	// ���Ƃ��n�߂�Y���W
	[SerializeField]
	GameObject dropYPosition;
	// ���Ƃ�X���W�̍ő�l�ɂ���I�u�W�F�N�g
	[SerializeField]
	GameObject dropXMaxPositionObject;
	// ���Ƃ�X���W�̍ŏ��l�ɂ���I�u�W�F�N�g
	[SerializeField]
	GameObject dropXMinPositionObject;

	// ���Ƃ��ʒu
	Vector3 dropXMinPosition = Vector3.zero;
	Vector3 dropXMaxPosition = Vector3.zero;

	// ���Ƃ��ʒu
	Vector2 dropPosition = Vector2.zero;

	// �Ō�̂��K���������Ƃ��̃G�t�F�N�g
	[SerializeField]
	GameObject lastUnionEffect;
	// �Ō�̂��K���������Ƃ��̉�
	[SerializeField]
	AudioClip lastUnionClip;

	// true = ���K�𗎂Ƃ���
	bool canDrop = true;
	public bool CanDrop
	{
		get
		{
			return canDrop;
		}
		set
		{
			canDrop = value;
			if (value)
			{
				Next();
			}
		}
	}

	// �R���{�ŏo��G�t�F�N�g
	[SerializeField]
	List<GameObject> comboEffects = new List<GameObject>();

	// ������������ true
	bool isInit = false;

	// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X
	IGM_Main iGM_Main;

	private void Start()
	{
		// �C���^�[�t�F�C�X�擾
		iGM_Main = GM_Main.Instance.GetComponent<IGM_Main>();

		// ���K����
		NextButtocksDecide();
		// ������
		isInit = true;

		// ���ɗ��Ƃ����K�ݒ�
		Next();
	}

	/// <summary>
	/// ���K����
	/// </summary>
	public GameObject SpawnButtocks()
	{
		// �I�u�W�F�N�g����
		var obj = Instantiate(buttocksPrefab, poolPosition.transform.position, Quaternion.identity);
		// ���O�ݒ�
		obj.name = (iButtocks.Count + 1).ToString();
		// �C���^�[�t�F�C�X�擾
		iButtocks.Add(obj, obj.GetComponent<IButtocks>());
		// ���K������
		iButtocks[obj].Initialize();

		return obj;
	}

	/// <summary>
	/// ���K���m�̏Փ�
	/// </summary>
	/// <param name="hitObj1">�Փ˂����I�u�W�F�N�g1</param>
	/// <param name="hitObj2">�Փ˂����I�u�W�F�N�g2</param>
	/// <returns>������ނ̂��K���ǂ���</returns>
	public bool ButtocksHit(GameObject hitObj1, GameObject hitObj2)
	{
		if ((GM_Main.Instance as GM_Main).IsGameOver)
		{
			return false;
		}

		// ������ނ��ƕЕ����v�[���ɖ߂��āA�ȍ~�Փˏ������s��Ȃ�
		if (iButtocks[hitObj1].GetType() == iButtocks[hitObj2].GetType())
		{
			// �X�R�A�ǉ�
			iGM_Main.AddScore(GetButtocksStatus((int)iButtocks[hitObj1].GetType()).score);
			iGM_Main.AddScoreNextTxt(GetButtocksStatus((int)iButtocks[hitObj1].GetType()).score, hitObj1.transform.position);
			// �I�u�W�F�N�g2�����߂ďՓ˂���̂ł���Η��Ƃ����Ԃɂ���
			if (iButtocks[hitObj2].GetIsFarstHit())
			{
				CanDrop = true;
				iButtocks[hitObj2].SetIsFarstHit(false);
			}

			// �R���{���ɉ������G�t�F�N�g���o��
			var effectNumber = iGM_Main.GetComboValue();
			// �G�t�F�N�g�̐��𒴂��Ȃ��悤�ɂ���
			if (effectNumber > comboEffects.Count - 1)
			{
				effectNumber = comboEffects.Count - 1;
			}
			// �R���{�{���\��
			iGM_Main.SetComboMagnification();
			// �R���{���ǉ�
			iGM_Main.AddComboValue();

			Stage.Instance.RightButtockShake((int)iButtocks[hitObj1].GetType());

			// �Ō�̂��K�������ꍇ����������
			if (iButtocks[hitObj1].GetType() == ButtocksType.Type12)
			{
				// �G�t�F�N�g����
				Instantiate(lastUnionEffect, hitObj1.transform.position, Quaternion.identity);
				// SE��炷
				SoundManager.Instance.SEPlay(lastUnionClip);

				// �����v�[����Ԃɂ���
				iButtocks[hitObj1].SetIsPool(true);
				iButtocks[hitObj2].SetIsPool(true);
				// �����폜
				Destroy(hitObj1);
				Destroy(hitObj2);
			}
			// �Ō�ł͂Ȃ��ꍇ���̂��K�ɐi���H
			else
			{
				// �G�t�F�N�g����
				Instantiate(comboEffects[effectNumber], hitObj1.transform.position, Quaternion.identity);

				// �Е����v�[����Ԃɂ���
				iButtocks[hitObj2].SetIsPool(true);
				// �Е����v�[���ɖ߂�
				Destroy(hitObj2);
				// �v�[�����Ă���ʒu�Ɉړ�������
				hitObj2.transform.position = poolPosition.transform.position;
				// ���̂��K�ɂ���
				iButtocks[hitObj1].SetStatus(GetButtocksStatus((int)iButtocks[hitObj1].GetType() + 1));
				// �������~�߂�
				iButtocks[hitObj1].MoveStop();
				// �i��
				//iButtocks[hitObj1].Evolution();
			}
			// ���K�������
			Stage.Instance.MakeButtocks((int)iButtocks[hitObj1].GetType());
			return true;
		}
		return false;
	}

	/// <summary>
	/// ���Ƃ����K�ړ�
	/// </summary>
	/// <param name="dropPosition">�ړ�������ʒu</param>
	public void DropButtocksMove(Vector2 dropPosition)
	{
		// ���Ƃ��ʒu�ݒ�
		this.dropPosition = dropPosition;
		// y���W�͌Œ肷��
		this.dropPosition.y = dropYPosition.transform.position.y;

		if (!dropButtocks)
		{
			return;
		}

		// �ړ��͈͂̐���
		if (this.dropPosition.x < dropXMinPosition.x)
		{
			this.dropPosition.x = dropXMinPosition.x;
		}
		if (dropXMaxPosition.x < this.dropPosition.x)
		{
			this.dropPosition.x = dropXMaxPosition.x;
		}
		// �K�C�h���C���̈ʒu�ݒ�
		iGM_Main.SetGuideLinePosition(this.dropPosition);

		// �ʒu�w��
		dropButtocks.transform.position = this.dropPosition;
	}

	/// <summary>
	/// ���K�𗎂Ƃ�
	/// </summary>
	public void Drop()
	{
		// ���Ƃ��Ȃ���Ԃł���Ζ߂�
		if (!CanDrop || Stage.Instance.IsShaking)
		{
			return;
		}
		// ���Ƃ����K���Ȃ��������͂��K�̃C���^�[�t�F�C�X���Ȃ��ꍇ�߂�
		if (!dropButtocks || iButtocks.Count <= 0)
		{
			return;
		}
		// ���Ƃ�
		iButtocks[dropButtocks].Drop();
		// �R���{�����Z�b�g
		iGM_Main.ComboReset();
		// �K�C�h���C����\��
		iGM_Main.GuideLineHide();
		// ���Ƃ����K��null�ɂ���
		dropButtocks = null;
		// ���Ƃ��Ȃ���Ԃɂ���
		CanDrop = false;
	}

	/// <summary>
	/// ���̂��K�����߂�
	/// </summary>
	public void NextButtocksDecide()
	{
		nextButtocks = SpawnButtocks();
		// �X�e�[�^�X�̔ԍ�����
		var dropNumber = Random.Range(0, (int)dropMaxType + 1);

		if (!isInit)
		{
			dropNumber = 0;
		}
		// �Ή�����X�e�[�^�X�擾
		var status = GetButtocksStatus(dropNumber);
		// �X�e�[�^�X�ݒ�
		iButtocks[nextButtocks].SetStatus(status);
		// ���ɗ��Ƃ����K�̉摜�ݒ�
		iGM_Main.SetNextButtocksImage(status.sprite);
		// ���K����������Ƃɂ���
		Stage.Instance.MakeButtocks((int)status.type);
	}

	/// <summary>
	/// ���̂��K�𗎂Ƃ����K��
	/// </summary>
	public void Next()
	{
		// ���ɗ��Ƃ����K�𗎂Ƃ����K�ɂ���
		dropButtocks = nextButtocks;
		// �v�[����Ԃ�ݒ�
		iButtocks[dropButtocks].SetIsPool(false);
		// ���ɗ��Ƃ����K���폜
		nextButtocks = null;
		// ��]�����Z�b�g
		dropButtocks.transform.localRotation = Quaternion.identity;

		// �ŏ��l�ƍő�l�̐ݒ�
		var size = GetButtocksStatus((int)iButtocks[dropButtocks].GetType()).size;
		dropXMinPosition = dropXMinPositionObject.transform.position + new Vector3(size.x, 0, 0);
		dropXMaxPosition = dropXMaxPositionObject.transform.position - new Vector3(size.x, 0, 0);
		// ���ɗ��Ƃ����K��ݒ�
		NextButtocksDecide();

		// ���Ƃ��ʒu�擾
		dropPosition = UnityEngine.InputSystem.Mouse.current.position.value;
		// ���[���h���W�ɕϊ�
		dropPosition = Camera.main.ScreenToWorldPoint(dropPosition);
		// ���Ƃ��ʒu�ɐݒ肷�邽�߂Ɉ�x�Ăяo��
		DropButtocksMove(dropPosition);
		// �K�C�h���C���\��
		iGM_Main.GuideLineShow();
	}

	/// <summary>
	/// �X�e�[�^�X�擾
	/// </summary>
	/// <param name="number">�擾����X�e�[�^�X�̔ԍ�</param>
	/// <returns>�擾�����X�e�[�^�X</returns>
	public ButtocksStatus GetButtocksStatus(int number)
	{
		return buttocksStatuses.statuses[number];
	}
}