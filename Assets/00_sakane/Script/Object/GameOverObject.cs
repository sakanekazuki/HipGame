using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// �Փ˂��Ă���ƃQ�[���I�[�o�[�ɂȂ�I�u�W�F�N�g
public class GameOverObject : MonoBehaviour
{
	// �Q�[���I�[�o�[�ɂȂ�܂ł̎���
	[SerializeField]
	float gameOverTime = 3;
	float time;
	// true = ���K�ɏՓ˒�
	bool isHitButtocks = false;

	// �Փ˂������K
	List<GameObject> hitButtocks = new List<GameObject>();
	// true = ��x�ʂ���
	bool isOnece = false;

	// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X
	IGM_Main iGM_Main;

	private void Start()
	{
		// �Q�[���}�l�[�W���[�̃C���^�[�t�F�C�X�擾
		iGM_Main = GM_Main.Instance.GetComponent<IGM_Main>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Buttocks"))
		{
			isHitButtocks = true;
			// �Փ˒��̃I�u�W�F�N�g�ɒǉ�
			hitButtocks.Add(collision.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (hitButtocks.Contains(collision.gameObject))
		{
			// �Փ˒��̃I�u�W�F�N�g����O��
			hitButtocks.Remove(collision.gameObject);
			// �Փ˒��̃I�u�W�F�N�g�����݂��Ȃ��ꍇ�A�Փ˃t���O��false�ɂ���
			if (hitButtocks.Count <= 0)
			{
				isHitButtocks = false;

				iGM_Main.TimeLimitReset();
			}
		}
	}

	private void LateUpdate()
	{
		if (isHitButtocks)
		{
			// ���Ԃ𑝂₷
			time += Time.deltaTime;
			// ���Ԑݒ�
			iGM_Main.SetTimeLimit(gameOverTime - time);
			// ���Ԃ𒴂���ƃQ�[���I�[�o�[
			if (time >= gameOverTime && !isOnece)
			{
				var token = this.GetCancellationTokenOnDestroy();
				GameOverAsync(token).Forget();
				isOnece = true;
			}
		}
		else
		{
			// ���K�ɏՓ˂��Ă��Ȃ��ꍇ���Ԃ�߂�
			time = 0;
		}
	}

	/// <summary>
	/// �Q�[���I�[�o�[�܂ł̃f�B���C
	/// </summary>
	/// <returns></returns>
	async UniTask GameOverAsync(CancellationToken token)
	{
		await UniTask.WaitForSeconds(0.2f, cancellationToken: token);
		iGM_Main.GameOver();
	}
}