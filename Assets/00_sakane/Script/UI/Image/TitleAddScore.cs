using UnityEngine;
using UnityEngine.UI;

// �ǉ������X�R�A
public class TitleAddScore : MonoBehaviour
{
	// �ԍ�
	[SerializeField]
	int number = 0;

	// �ω�����摜
	[SerializeField]
	Image changeImg;

	// �O�̐F
	Color defaultColor = Color.white;

	// �A�j���[�^�[
	Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		if (GameInstance.saveData.isMakes[number - 1])
		{
			changeImg.color = Color.white;
			defaultColor = Color.white;
		}
		else
		{
			changeImg.color = Color.black;
			defaultColor = Color.black;
		}
	}

	private void OnEnable()
	{
		animator?.Play("Stay", 0, 0);
	}

	/// <summary>
	/// �摜�̐F��ύX
	/// </summary>
	public void ChangeImageColor()
	{
		if (number == 11)
		{
			return;
		}
		// �J������Ă��邩�ǂ����ŐF�ύX
		if (GameInstance.saveData.isMakes[number])
		{
			changeImg.color = Color.white;
		}
		else
		{
			changeImg.color = Color.black;
		}
	}

	/// <summary>
	/// �摜�̐F�����Ƃɖ߂�
	/// </summary>
	public void ReturnImgColor()
	{
		changeImg.color = defaultColor;
	}
}