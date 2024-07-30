using System.Collections.Generic;
using UnityEngine;

// お尻のステータスを保持する
[CreateAssetMenu(fileName = "ButtocksStatus", menuName = "ScriptableObject/ButtocksStatus")]
public class SO_ButtocksStatuses : ScriptableObject
{
	// お尻の状態
	public List<ButtocksStatus> statuses = new List<ButtocksStatus>();
}
