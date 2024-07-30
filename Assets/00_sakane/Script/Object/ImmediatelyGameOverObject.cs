using UnityEngine;

// お尻が衝突したらすぐにゲームオーバーになるオブジェクト
public class ImmediatelyGameOverObject : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// お尻が衝突した瞬間ゲームオーバー
		if (collision.CompareTag("Buttocks"))
		{
			(GM_Main.Instance as GM_Main).GameOver();
		}
	}
}