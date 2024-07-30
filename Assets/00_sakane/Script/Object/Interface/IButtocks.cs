using UnityEngine;

// お尻おインターフェース
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