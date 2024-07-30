using UnityEngine;

// �K�C�h���C��
public class GuideLineObject : MonoBehaviour
{
	// �}�X�N�̃T�C�Y
	Vector2 maskSize = Vector2.zero;

	// �K�C�h���C���̃}�X�N�I�u�W�F�N�g
	[SerializeField]
	GameObject guideLineMask;

	private void Start()
	{
		// �K�C�h���C���̃X�v���C�g���擾
		var sprite = guideLineMask.GetComponent<SpriteMask>().sprite;
		// �X�v���C�g�̃T�C�Y�擾
		maskSize = sprite.bounds.size;
	}

	private void Update()
	{
		// �Փˏ��擾
		var hit = Physics2D.Raycast(transform.position, Vector2.down, maskSize.x, LayerMask.GetMask("Buttocks"));
		if (hit.collider != null)
		{
			// �����v�Z
			var ratio = hit.distance / maskSize.x;
			// �T�C�Y�ݒ�
			guideLineMask.transform.localScale = new Vector3(ratio, 1, 1);
		}
		else
		{
			guideLineMask.transform.localScale = Vector3.one;
		}
	}
}