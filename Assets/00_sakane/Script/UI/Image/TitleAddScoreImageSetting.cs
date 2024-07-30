using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// �^�C�g���̃X�R�A�摜�̐ݒ�
public class TitleAddScoreImageSetting : MonoBehaviour
{
	//// �摜�N���X
	//[SerializeField]
	//List<Image> images = new List<Image>();

	//// �摜
	//[SerializeField]
	//List<Image> subImages = new List<Image>();

	[SerializeField]
	Image img;

	[SerializeField]
	Image subImg;

	[SerializeField]
	TMPro.TextMeshProUGUI txt;

	List<int> scores = new List<int>()
	{
	1,3,6,10,15,21,28,36,45,55,66
	};

	int number = -1;

	private void Start()
	{
		//foreach (var v in images)
		//{
		//	// �ԍ��擾
		//	var number = images.IndexOf(v);
		//	// �J������Ă����Ԃł���Δ�
		//	if (GameInstance.saveData.isMakes[number])
		//	{
		//		v.color = Color.white;
		//		subImages[number].color = Color.white;

		//	}
		//	// �J������Ă��Ȃ���΍�
		//	else
		//	{
		//		v.color = Color.black;
		//		subImages[number].color = Color.black;
		//	}
		//}

		//if(GameInstance.saveData.isMakes[0])
		//{
		//	img.color = Color.white;
		//	subImg.color = Color.white;
		//}
		//else
		//{
		//	img.color = Color.black;
		//	subImg.color = Color.black;
		//}
		ChangeColor();
	}

	public void ChangeColor()
	{
		if (number == -1)
		{
			txt.text = "+" + scores[0].ToString();
		}
		else
		{
			txt.text = "+" + scores[number].ToString();
		}
		++number;
		number %= GameInstance.saveData.isMakes.Count;
		if (GameInstance.saveData.isMakes[number])
		{
			img.color = Color.white;
			subImg.color = Color.white;
		}
		else
		{
			img.color = Color.black;
			subImg.color = Color.black;
		}
	}
}
