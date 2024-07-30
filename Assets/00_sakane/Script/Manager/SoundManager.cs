using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

// ���ʊǗ��N���X
public class SoundManager : ManagerBase<SoundManager>
{
	// �I�[�f�B�I�Ǘ��I�u�W�F�N�g
	[SerializeField]
	AudioMixer audioMixer;

	// �}�X�^�[�{�����[��
	float masterVolume = 0;
	public float MasterVolume
	{
		get => masterVolume;
		set
		{
			var db = Mathf.Sqrt(Mathf.Sqrt(value)) * 80 - 80;
			masterVolume = db;
			// �~�L�T�[�ɐݒ�
			audioMixer.SetFloat("MasterVolume", db);
			// ���ʂ�ۑ�
			masterVolume = db;
			// �Z�[�u�f�[�^�ɕۑ�
			GameInstance.saveData.masterVolume = value;
		}
	}

	// BGM�̉���
	float bgmVolume = 0;
	public float BGMVolume
	{
		get => bgmVolume;
		set
		{
			var db = Mathf.Sqrt(Mathf.Sqrt(value)) * 80 - 80;
			bgmVolume = db;
			// �~�L�T�[�ɐݒ�
			audioMixer.SetFloat("BGMVolume", db);
			// ���ʂ�ۑ�
			bgmVolume = db;
			// �Z�[�u�f�[�^�ɕۑ�
			GameInstance.saveData.bgmVolume = value;
		}
	}

	// SE�̉���
	float seVolume = 0;
	public float SEVolume
	{
		get => seVolume;
		set
		{
			var db = Mathf.Sqrt(Mathf.Sqrt(value)) * 80 - 80;
			seVolume = db;
			// �~�L�T�[�ɐݒ�
			audioMixer.SetFloat("SEVolume", db);
			// ���ʂ�ۑ�
			seVolume = db;
			// �Z�[�u�f�[�^�ɕۑ�
			GameInstance.saveData.seVolume = value;
		}
	}

	// ����炷�I�u�W�F�N�g
	[SerializeField]
	GameObject seRingObj;

	// bgm��炵�Ă���source
	AudioSource bgmSource;

	private void Start()
	{
		// ���ʐݒ�
		MasterVolume = GameInstance.saveData.masterVolume;
		BGMVolume = GameInstance.saveData.bgmVolume;
		SEVolume = GameInstance.saveData.seVolume;
		// �~�L�T�[�ɐݒ肷��
		audioMixer.SetFloat("MasterVolume", masterVolume);
		audioMixer.SetFloat("BGMVolume", bgmVolume);
		audioMixer.SetFloat("SEVolume", seVolume);
		// BGM��炵�Ă���source�擾
		bgmSource = Camera.main.GetComponent<AudioSource>();
	}

	/// <summary>
	/// BGM�ύX
	/// </summary>
	/// <param name="clip">�ύX����BGM</param>
	public async UniTask BGMChange(AudioClip clip)
	{
		bgmSource.Stop();
		// BGM�ݒ�
		bgmSource.clip = clip;
		// �����҂�
		await UniTask.WaitForSeconds(0.1f);
		// BGM�Đ�
		bgmSource?.Play();
	}

	/// <summary>
	/// SE��炷
	/// </summary>
	/// <param name="clip">�炷��</param>
	public void SEPlay(AudioClip clip)
	{
		// ����炷�I�u�W�F�N�g����
		var obj = Instantiate(seRingObj, gameObject.transform);
		// ����炷�N���X�擾
		var iSound = obj.GetComponent<ISound>();
		// ����炷
		iSound.Play(clip);
	}
}