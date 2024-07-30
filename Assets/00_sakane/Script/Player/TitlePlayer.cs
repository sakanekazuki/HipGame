using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// �^�C�g���p�̃v���C���[
public class TitlePlayer : MonoBehaviour
{
	// ���K
	[SerializeField]
	GameObject buttocksPrefab;

	// ���Ƃ��ʒu
	Vector2 dropPosition = Vector2.zero;

	// �C���v�b�g
	MainInput input;

	bool isOnUI = false;

	private void Start()
	{
		// �C���v�b�g����
		input = new MainInput();
		// ���͗L����
		input.Enable();

		// ���K�𗎂Ƃ�
		input.Player.Drop.started += (InputAction.CallbackContext context) =>
		{
			if (isOnUI)
			{
				return;
			}
			// ���K����
			Instantiate(buttocksPrefab, dropPosition, Quaternion.identity);
		};

		// �}�E�X�̈ʒu
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