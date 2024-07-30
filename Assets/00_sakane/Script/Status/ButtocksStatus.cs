using System;
using UnityEngine;

// ���K�̃X�e�[�^�X
[Serializable]
public class ButtocksStatus
{
	// ���K�̎��
	public ButtocksManager.ButtocksType type;
	// �R���C�_�[�̈ʒu
	public Vector2 colliderPosition = Vector2.zero;
	// �傫��
	public Vector2 size = Vector2.one;
	// �d��
	public float mass = 0;
	// ������
	public Sprite sprite = null;
	// �����I�u�W�F�N�g�ɏՓ˂����ۂɒǉ������X�R�A
	public int score = 10;
}