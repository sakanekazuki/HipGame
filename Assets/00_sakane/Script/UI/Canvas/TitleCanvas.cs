using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

// タイトルキャンバス
public class TitleCanvas : CanvasBase, ITitleCanvas
{
	// タイトルマネージャーのインターフェイス
	IGM_Title iGM_Title;

	// ハイスコアテキスト
	[SerializeField]
	TextMeshProUGUI hightScoreTxt;

	// タイトルのボタン
	[SerializeField]
	List<Button> titleButtons = new List<Button>();
	List<ButtonDefault> defaultButton = new List<ButtonDefault>();

	protected override void Start()
	{
		base.Start();
		// タイトルマネージャーのインターフェイス取得
		iGM_Title = GM_Title.Instance.GetComponent<IGM_Title>();
		// ハイスコアを表示
		hightScoreTxt.text = GameInstance.saveData.highScore.ToString();
		// タイトルのボタン有効化
		foreach (var button in titleButtons)
		{
			defaultButton.Add(button.GetComponent<ButtonDefault>());
			button.interactable = true;
		}
	}

	/// <summary>
	/// タイトル有効化
	/// </summary>
	public void TitleEnable()
	{
		// タイトルのボタン有効化
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = true;
			button.interactable = true;
		}
	}

	/// <summary>
	/// ゲーム開始
	/// </summary>
	public void GameStart()
	{
		iGM_Title.GameStart();
	}

	/// <summary>
	/// オプションを開く
	/// </summary>
	public void OptionOpen()
	{
		iGM_Title.OptionOpen();
		// タイトルのボタン無効化
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = false;
			button.interactable = false;
		}
	}

	/// <summary>
	/// クレジット開く
	/// </summary>
	public void CreditOpen()
	{
		iGM_Title.CreditOpen();
		// タイトルのボタン無効化
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = false;
			button.interactable = false;
		}
	}

	/// <summary>
	/// コレクションを開く
	/// </summary>
	public void CollectionOpen()
	{
		CollectionManager.Instance.CollectionCanvasOpen();
		// タイトルのボタン無効化
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = false;
			button.interactable = false;
		}
	}

	/// <summary>
	/// ゲーム終了（終わるか聞かれる）
	/// </summary>
	public void GameQuit()
	{

		// タイトルのボタン無効化
		foreach (var button in titleButtons)
		{
			defaultButton[titleButtons.IndexOf(button)].IsEnable = false;
			button.interactable = false;
		}
	}

	/// <summary>
	/// ゲーム終了
	/// </summary>
	public void ApplicationQuit()
	{
#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPlaying = false;

#else

		Application.Quit();

#endif
	}
}