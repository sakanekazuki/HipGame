using UnityEngine;

// �n�C�X�R�A�X�V�̃e�L�X�g
public class HighScoreUpdateText : MonoBehaviour
{
	// �n�C�X�R�A�\�����Ă���e�L�X�g
	TMPro.TextMeshProUGUI highScoreTxt;
	// �ŏ��̐F
	Color defaultColor = Color.white;

	// �_�ő��x
	[SerializeField]
	float speed = 0.2f;

	// �V�[�^
	float theta = 0;

	void Start()
	{
		// �e�L�X�g�擾
		highScoreTxt = GetComponent<TMPro.TextMeshProUGUI>();
		// �ŏ��̐F
		defaultColor = highScoreTxt.color;
	}

	void Update()
	{
		// �A���t�@�v�Z
		var alpha = Mathf.Sin(theta);
		// ���x�𑫂�
		theta += speed;
		// �A�j���[�V����������
		highScoreTxt.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha);
	}
}
