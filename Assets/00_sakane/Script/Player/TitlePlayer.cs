using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// タイトル用のプレイヤー
public class TitlePlayer : MonoBehaviour
{
	// お尻
	[SerializeField]
	GameObject buttocksPrefab;

	// 落とす位置
	Vector2 dropPosition = Vector2.zero;

	// インプット
	MainInput input;

	bool isOnUI = false;

	private void Start()
	{
		// インプット生成
		input = new MainInput();
		// 入力有効化
		input.Enable();

		// お尻を落とす
		input.Player.Drop.started += (InputAction.CallbackContext context) =>
		{
			if (isOnUI)
			{
				return;
			}
			// お尻生成
			Instantiate(buttocksPrefab, dropPosition, Quaternion.identity);
		};

		// マウスの位置
		input.Player.MousePosition.performed += (InputAction.CallbackContext context) =>
		{
			dropPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
		};
	}

	private void OnEnable()
	{
		input?.Enable();
	}

	private void Update()
	{
		isOnUI = EventSystem.current.IsPointerOverGameObject();
	}

	private void OnDisable()
	{
		input?.Disable();
	}

	private void OnDestroy()
	{
		input = null;
	}
}