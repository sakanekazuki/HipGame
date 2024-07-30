using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// プレイヤー
public class Player : MonoBehaviour
{
	// 入力
	MainInput input;
	// 落とす位置
	Vector2 dropPosition = Vector2.zero;

	// true = カーソルがUIに合っている
	bool isOnUI = false;

	private void Start()
	{
		// 入力生成
		input = new MainInput();
		// 入力有効化
		input.Enable();
		// 落とす入力
		input.Player.Drop.started += (InputAction.CallbackContext context) =>
		{
			// カーソルがUIに合っている場合処理しない
			if (isOnUI || FadeManager.Instance.IsFading)
			{
				return;
			}
			Drop();
		};
		// マウスの位置取得
		input.Player.MousePosition.performed += (InputAction.CallbackContext context) =>
		{
			// 落とす位置取得
			dropPosition = context.ReadValue<Vector2>();
			// ワールド座標に変換
			dropPosition = Camera.main.ScreenToWorldPoint(dropPosition);
			// お尻の位置移動
			ButtocksManager.Instance.DropButtocksMove(dropPosition);
		};

		// シャッフル
		input.Player.Shake.started += (InputAction.CallbackContext context) =>
		{
			if (isOnUI || FadeManager.Instance.IsFading)
			{
				return;
			}
			(GM_Main.Instance as GM_Main).Shake();
		};
	}

	private void OnEnable()
	{
		// 入力有効化
		input?.Enable();
	}

	private void Update()
	{
		isOnUI = EventSystem.current.IsPointerOverGameObject();
	}

	private void OnDisable()
	{
		// 入力無効化
		input?.Disable();
	}

	/// <summary>
	/// 落とす
	/// </summary>
	public void Drop()
	{
		ButtocksManager.Instance.Drop();
	}
}