using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// �v���C���[
public class Player : MonoBehaviour
{
	// ����
	MainInput input;
	// ���Ƃ��ʒu
	Vector2 dropPosition = Vector2.zero;

	// true = �J�[�\����UI�ɍ����Ă���
	bool isOnUI = false;

	private void Start()
	{
		// ���͐���
		input = new MainInput();
		// ���͗L����
		input.Enable();
		// ���Ƃ�����
		input.Player.Drop.started += (InputAction.CallbackContext context) =>
		{
			// �J�[�\����UI�ɍ����Ă���ꍇ�������Ȃ�
			if (isOnUI || FadeManager.Instance.IsFading)
			{
				return;
			}
			Drop();
		};
		// �}�E�X�̈ʒu�擾
		input.Player.MousePosition.performed += (InputAction.CallbackContext context) =>
		{
			// ���Ƃ��ʒu�擾
			dropPosition = context.ReadValue<Vector2>();
			// ���[���h���W�ɕϊ�
			dropPosition = Camera.main.ScreenToWorldPoint(dropPosition);
			// ���K�̈ʒu�ړ�
			ButtocksManager.Instance.DropButtocksMove(dropPosition);
		};

		// �V���b�t��
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
		// ���͗L����
		input?.Enable();
	}

	private void Update()
	{
		isOnUI = EventSystem.current.IsPointerOverGameObject();
	}

	private void OnDisable()
	{
		// ���͖�����
		input?.Disable();
	}

	/// <summary>
	/// ���Ƃ�
	/// </summary>
	public void Drop()
	{
		ButtocksManager.Instance.Drop();
	}
}