using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

// レベル管理クラス
public class LevelManager : ManagerBase<LevelManager>
{
	// true = レベル読み込み中
	static bool isLevelLoading = false;
	public static bool IsLevelLoading
	{
		get => isLevelLoading;
	}

	// true = レベルを移動することができる
	static public bool canLevelMove = true;

	/// <summary>
	/// シーン切り替え
	/// </summary>
	/// <param name="levelName">シーン名</param>
	/// <returns></returns>
	public static async UniTaskVoid OpenLevel(CancellationToken token, string levelName)
	{
		// 読み込み中にする
		isLevelLoading = true;
		// 読み込み
		var operation = SceneManager.LoadSceneAsync(levelName);
		// シーンを移動しない状態にする
		operation.allowSceneActivation = false;
		// 読み込みが終了するまで待つ
		await UniTask.WaitUntil(() => operation.progress >= 0.9f, cancellationToken: token);
		// 読み込み中を解除
		isLevelLoading = false;
		// 移動できる状態になるまで待つ
		await UniTask.WaitUntil(() => canLevelMove, cancellationToken: token);
		// 移動できる状態にする
		operation.allowSceneActivation = true;
	}

	/// <summary>
	/// ゲーム終了
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