using Cysharp.Threading.Tasks;
using UnityEngine;

// リザルトキャンバス
public class ResultCanvas : CanvasBase
{
	// 今回のスコア
	[SerializeField]
	TMPro.TextMeshProUGUI scoreTxt;
	// 最高スコアを表示するテキスト
	[SerializeField]
	TMPro.TextMeshProUGUI highScoreTxt;

	// ハイスコアが更新されたときに表示されるオブジェクト
	[SerializeField]
	GameObject newHighScoreObj;

	private void OnEnable()
	{
		// インスタンスをGM_Mainに変換
		var gm_Main = (GM_Main.Instance as GM_Main);
		// スコア表示
		scoreTxt.text = Mathf.Floor(gm_Main.Score).ToString() + "pt";
		// ハイスコア表示
		highScoreTxt.text = gm_Main.HighScore.ToString() + "pt";

		// ハイスコア更新
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
	/// タイトルに戻る
	/// </summary>
	async public void ToTile()
	{
		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// シーン移動できない状態にする
		LevelManager.canLevelMove = false;
		// タイトルシーン読み込み
		LevelManager.OpenLevel(token, "TitleScene").Forget();
		// フェード終了まで待つ
		await FadeManager.Instance.FadeOutAsync();
		// 読み込み終了まで待つ
		await UniTask.WaitUntil(() => !LevelManager.IsLevelLoading);
		// シーン移動をできる状態にする
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// リトライ
	/// </summary>
	async public void Retry()
	{
		// キャンセレーショントークン取得
		var token = this.GetCancellationTokenOnDestroy();
		// シーン移動できない状態にする
		LevelManager.canLevelMove = false;
		// メインゲームのシーン読み込み
		LevelManager.OpenLevel(token, "MainScene").Forget();
		// フェード終了まで待つ
		await FadeManager.Instance.FadeOutAsync();
		// 読み込み終了まで待つ
		await UniTask.WaitUntil(() =>!LevelManager.IsLevelLoading);
		// シーン移動をできる状態にする
		LevelManager.canLevelMove = true;
	}
}