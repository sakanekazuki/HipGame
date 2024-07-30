using System.Diagnostics.Tracing;
using UnityEngine;

// SE
public class Sound : MonoBehaviour, ISound
{
	// ����炷�R���|�[�l���g
	AudioSource source;

	// �Đ����
	bool isPlaying = false;

	// ��
	AudioClip clip;

	void Start()
	{
		// ����炷�R���|�[�l���g�擾
		source = GetComponent<AudioSource>();
		// ���[�v���Ȃ�
		source.loop = false;
		// ���ݒ�
		source.clip = clip;
		// �Đ���Ԃɂ���
		isPlaying = true;
		// �Đ�
		source.Play();
	}

	void Update()
	{
		if (isPlaying)
		{
			// �ۂ��G��Ԑݒ�
			isPlaying = source.isPlaying;
			if (!isPlaying)
			{
				// �폜
				Destroy(gameObject);
			}
		}
	}

	/// <summary>
	/// �Đ�
	/// </summary>
	/// <param name="clip">�Đ����鉹</param>
	public void Play(AudioClip clip)
	{
		// �N���b�v�̐ݒ�
		this.clip = clip;
	}
}
