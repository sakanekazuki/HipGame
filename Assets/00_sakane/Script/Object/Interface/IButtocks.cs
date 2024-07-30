using UnityEngine;

// ���K���C���^�[�t�F�[�X
public interface IButtocks
{
	void Initialize();
	void MoveStop();
	void SetStatus(ButtocksStatus status);
	void Drop();
	bool GetIsPool();
	void SetIsPool(bool isPool);
	void Evolution();
	ButtocksManager.ButtocksType GetType();

	bool GetIsFarstHit();
	void SetIsFarstHit(bool isFarstHit);
}