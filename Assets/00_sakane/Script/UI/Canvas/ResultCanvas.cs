using Cysharp.Threading.Tasks;
using UnityEngine;

// ���U���g�L�����o�X
public class ResultCanvas : CanvasBase
{
	// ����̃X�R�A
	[SerializeField]
	TMPro.TextMeshProUGUI scoreTxt;
	// �ō��X�R�A��\������e�L�X�g
	[SerializeField]
	TMPro.TextMeshProUGUI highScoreTxt;

	// �n�C�X�R�A���X�V���ꂽ�Ƃ��ɕ\�������I�u�W�F�N�g
	[SerializeField]
	GameObject newHighScoreObj;

	private void OnEnable()
	{
		// �C���X�^���X��GM_Main�ɕϊ�
		var gm_Main = (GM_Main.Instance as GM_Main);
		// �X�R�A�\��
		scoreTxt.text = Mathf.Floor(gm_Main.Score).ToString() + "pt";
		// �n�C�X�R�A�\��
		highScoreTxt.text = gm_Main.HighScore.ToString() + "pt";

		// �n�C�X�R�A�X�V
		if (gm_Main.IsNewHighScore)
		{
			newHighScoreObj.SetActive(true);
		}
		else
		{
			newHighScoreObj.SetActive(false);
		}
	}

	/// <summary>
	/// �^�C�g���ɖ߂�
	/// </summary>
	async public void ToTile()
	{
		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �V�[���ړ��ł��Ȃ���Ԃɂ���
		LevelManager.canLevelMove = false;
		// �^�C�g���V�[���ǂݍ���
		LevelManager.OpenLevel(token, "TitleScene").Forget();
		// �t�F�[�h�I���܂ő҂�
		await FadeManager.Instance.FadeOutAsync();
		// �ǂݍ��ݏI���܂ő҂�
		await UniTask.WaitUntil(() => !LevelManager.IsLevelLoading);
		// �V�[���ړ����ł����Ԃɂ���
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// ���g���C
	/// </summary>
	async public void Retry()
	{
		// �L�����Z���[�V�����g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();
		// �V�[���ړ��ł��Ȃ���Ԃɂ���
		LevelManager.canLevelMove = false;
		// ���C���Q�[���̃V�[���ǂݍ���
		LevelManager.OpenLevel(token, "MainScene").Forget();
		// �t�F�[�h�I���܂ő҂�
		await FadeManager.Instance.FadeOutAsync();
		// �ǂݍ��ݏI���܂ő҂�
		await UniTask.WaitUntil(() =>!LevelManager.IsLevelLoading);
		// �V�[���ړ����ł����Ԃɂ���
		LevelManager.canLevelMove = true;
	}
}