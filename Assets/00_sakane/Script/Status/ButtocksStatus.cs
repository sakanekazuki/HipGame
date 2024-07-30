using System;
using UnityEngine;

// お尻のステータス
[Serializable]
public class ButtocksStatus
{
	// お尻の種類
	public ButtocksManager.ButtocksType type;
	// コライダーの位置
	public Vector2 colliderPosition = Vector2.zero;
	// 大きさ
	public Vector2 size = Vector2.one;
	// 重さ
	public float mass = 0;
	// 見た目
	public Sprite sprite = null;
	// 同じオブジェクトに衝突した際に追加されるスコア
	public int score = 10;
}