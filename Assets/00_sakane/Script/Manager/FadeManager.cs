using Cysharp.Threading.Tasks;
using UnityEngine;

// �t�F�[�h�Ǘ��N���X
public class FadeManager : ManagerBase<FadeManager>
{
	// �t�F�[�h���s���L�����o�X
	[SerializeField]
	GameObject fadeIOCanvasPrefab;
	GameObject fadeIOCanvas;
	// �t�F�[�h����L�����o�X�̃C���^�[�t�F�C�X
	IFadeIOCanvas iFadeIOCanvas;

	// �t�F�[�h���x
	[SerializeField]
	float fadeSpeed = 0.08f;
	public float FadeSpeed
	{
		get => fadeSpeed;
	}

	// true = �t�F�[�h��
	bool isFading = false;
	public bool IsFading
	{
		get => isFading;
	}

	private void Awake()
	{
		// �t�F�[�h�L�����o�X�v���n�u����
		fadeIOCanvas = Instantiate(fadeIOCanvasPrefab);
		// �t�F�[�h�L�����o�X�̃C���^�[�t�F�C�X�擾
		iFadeIOCanvas = fadeIOCanvas.GetComponent<IFadeIOCanvas>();
		// ������
		iFadeIOCanvas.Initialize();
	}

	private void Start()
	{
		// �t�F�[�h�C��
		FadeIn();
	}

	/// <summary>
	/// �t�F�[�h�C��
	/// </summary>
	[ContextMenu("fadein")]
	public void FadeIn()
	{
		FadeInAsync().Forget();
	}

	/// <summary>
	/// �t�F�[�h�A�E�g
	/// </summary>
	[ContextMenu("fadeout")]
	public void FadeOut()
	{
		FadeOutAsync().Forget();
	}

	/// <summary>
	/// �t�F�[�h�C��
	/// </summary>
	async public UniTask FadeInAsync()
	{
		fadeIOCanvas.SetActive(true);
		// �t�F�[�h����Ԃɂ���
		isFading = true;
		// �t�F�[�h�C��
		await iFadeIOCanvas.FadeIn();
		// �t�F�[�h�I����Ԃɂ���
		isFading = false;

		fadeIOCanvas.SetActive(false);
	}

	/// <summary>
	/// �t�F�[�h�A�E�g
	/// </summary>
	async public UniTask FadeOutAsync()
	{
		fadeIOCanvas.SetActive(true);
		// �t�F�[�h����Ԃɂ���
		isFading = true;
		// �t�F�[�h�A�E�g�J�n
		await iFadeIOCanvas.FadeOut();
		// �t�F�[�h�I����Ԃɂ���
		isFading = false;
	}
}
