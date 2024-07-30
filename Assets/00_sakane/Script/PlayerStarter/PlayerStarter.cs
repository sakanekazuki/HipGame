using UnityEngine;

// プレイヤー生成位置
public class PlayerStarter : MonoBehaviour
{
	private void Awake()
	{
		// ゲームマネージャーの生成位置にこのオブジェクトを追加
		GameManagerBase.Instance.AddStarter(gameObject);
	}
}