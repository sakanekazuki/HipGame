using UnityEngine;
using UnityEngine.UI;

// �L�����o�X���N���X
public class CanvasBase : MonoBehaviour
{
	protected virtual void Start()
	{
		// �J�����̐ݒ�
		GetComponent<Canvas>().worldCamera = Camera.main;
		GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
	}
}