using System.Collections.Generic;
using UnityEngine;

// ���K�̃X�e�[�^�X��ێ�����
[CreateAssetMenu(fileName = "ButtocksStatus", menuName = "ScriptableObject/ButtocksStatus")]
public class SO_ButtocksStatuses : ScriptableObject
{
	// ���K�̏��
	public List<ButtocksStatus> statuses = new List<ButtocksStatus>();
}
