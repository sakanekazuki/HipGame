using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// �R���N�V�����L�����o�X
public class CollectionCanvas : CanvasBase
{
	// ���K�R���N�V�����摜
	[SerializeField]
	List<Image> buttocks = new List<Image>();

	// ���ʂ̉摜
	[SerializeField]
	List<Sprite> normalSprites_en = new List<Sprite>();
	// �����摜
	[SerializeField]
	List<Sprite> blackSprites_en = new List<Sprite>();

	// ���ʂ̉摜
	[SerializeField]
	List<Sprite> normalSprites_jp = new List<Sprite>();
	// �����摜
	[SerializeField]
	List<Sprite> blackSprites_jp = new List<Sprite>();

	// �傫���\������R���N�V�����̉摜
	[SerializeField]
	Image bigCollectionImg;

	protected override void Start()
	{
		base.Start();

		BigCollectionHide();
	}

	private void OnEnable()
	{
		// ���O�z��擾
		var names = GameInstance.saveData.language == LanguageManager.LanguageType.Japanese ?
			CollectionManager.Instance.names_jp : CollectionManager.Instance.names_en;

		for (int i = 0; i < buttocks.Count; ++i)
		{
			if (GameInstance.saveData.language == LanguageManager.LanguageType.English)
			{
				buttocks[i].sprite = GameInstance.saveData.isMakes[i] ? normalSprites_en[i] : blackSprites_en[i];
			}
			else
			{
				buttocks[i].sprite = GameInstance.saveData.isMakes[i] ? normalSprites_jp[i] : blackSprites_jp[i];
			}
		}
	}

	/// <summary>
	/// �R���N�V���������
	/// </summary>
	public void CollectionClose()
	{
		// ����
		CollectionManager.Instance.CollectionCanvasClose();
	}

	/// <summary>
	/// �傫���\������R���N�V�����̉摜�ݒ�
	/// </summary>
	/// <param name="sprite">�ݒ肷��摜</param>
	public void SetBigCollectionSprite(Sprite sprite)
	{
		bigCollectionImg.sprite = sprite;
	}

	/// <summary>
	/// �傫���\������R���N�V�����̉摜��\������
	/// </summary>
	public void BigCollectionDisplay()
	{
		bigCollectionImg.gameObject.SetActive(true);
	}

	/// <summary>
	/// �傫���J�[�h�\���̃A�j���[�V����
	/// </summary>
	/// <param name="token">�L�����Z���[�V�����g�[�N��</param>
	/// <returns></returns>
	public async UniTask BigCollectionDisplayAsync(CancellationToken token)
	{
		await UniTask.DelayFrame(1, cancellationToken: token);
	}

	/// <summary>
	/// �傫���\������R���N�V�����̉摜���\���ɂ���
	/// </summary>
	public void BigCollectionHide()
	{
		bigCollectionImg.gameObject.SetActive(false);
	}

	/// <summary>
	/// �傫���J�[�h��\���̃A�j���[�V����
	/// </summary>
	/// <param name="token">�L�����Z���[�V�����g�[�N��</param>
	/// <returns></returns>
	public async UniTask BigCollectionHideAsync(CancellationToken token)
	{
		await UniTask.DelayFrame(1, cancellationToken: token);
	}
}
