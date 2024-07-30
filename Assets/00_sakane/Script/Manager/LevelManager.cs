using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���x���Ǘ��N���X
public class LevelManager : ManagerBase<LevelManager>
{
	// true = ���x���ǂݍ��ݒ�
	static bool isLevelLoading = false;
	public static bool IsLevelLoading
	{
		get => isLevelLoading;
	}

	// true = ���x�����ړ����邱�Ƃ��ł���
	static public bool canLevelMove = true;

	/// <summary>
	/// �V�[���؂�ւ�
	/// </summary>
	/// <param name="levelName">�V�[����</param>
	/// <returns></returns>
	public static async UniTaskVoid OpenLevel(CancellationToken token, string levelName)
	{
		// �ǂݍ��ݒ��ɂ���
		isLevelLoading = true;
		// �ǂݍ���
		var operation = SceneManager.LoadSceneAsync(levelName);
		// �V�[�����ړ����Ȃ���Ԃɂ���
		operation.allowSceneActivation = false;
		// �ǂݍ��݂��I������܂ő҂�
		await UniTask.WaitUntil(() => operation.progress >= 0.9f, cancellationToken: token);
		// �ǂݍ��ݒ�������
		isLevelLoading = false;
		// �ړ��ł����ԂɂȂ�܂ő҂�
		await UniTask.WaitUntil(() => canLevelMove, cancellationToken: token);
		// �ړ��ł����Ԃɂ���
		operation.allowSceneActivation = true;
	}

	/// <summary>
	/// �Q�[���I��
	/// </summary>
	static public void GameQuit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}