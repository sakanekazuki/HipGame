using UnityEngine;

// ���K���Փ˂����炷���ɃQ�[���I�[�o�[�ɂȂ�I�u�W�F�N�g
public class ImmediatelyGameOverObject : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ���K���Փ˂����u�ԃQ�[���I�[�o�[
		if (collision.CompareTag("Buttocks"))
		{
			(GM_Main.Instance as GM_Main).GameOver();
		}
	}
}