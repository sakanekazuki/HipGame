using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���ʐݒ�X���C�_�[�̃n���h��
public class SoundSliderHandle : MonoBehaviour
{
	// �n���h��
	[SerializeField]
	Image handleImg;

	// ���K�摜
	[SerializeField]
	List<Sprite> buttocks = new List<Sprite>();

	// �摜�T�C�Y
	List<Vector2> sizes = new List<Vector2>();

	// �X���C�_�[
	Slider slider;

	private void Start()
	{
		// �X���C�_�[�擾
		slider = GetComponent<Slider>();

		foreach (var v in buttocks)
		{
			sizes.Add(v.bounds.size * 100);
		}
	}

	private void Update()
	{
		var number = Mathf.FloorToInt(((slider.value + 40) / 40) * 10);
		// �摜�ݒ�
		handleImg.sprite = buttocks[number];

		// ���N�g�g�����X�t�H�[���擾
		var rect = handleImg.transform as RectTransform;

		var size = new Vector2(sizes[number].x, sizes[number].y / 2);
		// �T�C�Y�ݒ�
		rect.sizeDelta = size;
	}
}