using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

// 音量管理クラス
public class SoundManager : ManagerBase<SoundManager>
{
	// オーディオ管理オブジェクト
	[SerializeField]
	AudioMixer audioMixer;

	// マスターボリューム
	float masterVolume = 0;
	public float MasterVolume
	{
		get => masterVolume;
		set
		{
			var db = Mathf.Sqrt(Mathf.Sqrt(value)) * 80 - 80;
			masterVolume = db;
			// ミキサーに設定
			audioMixer.SetFloat("MasterVolume", db);
			// 音量を保存
			masterVolume = db;
			// セーブデータに保存
			GameInstance.saveData.masterVolume = value;
		}
	}

	// BGMの音量
	float bgmVolume = 0;
	public float BGMVolume
	{
		get => bgmVolume;
		set
		{
			var db = Mathf.Sqrt(Mathf.Sqrt(value)) * 80 - 80;
			bgmVolume = db;
			// ミキサーに設定
			audioMixer.SetFloat("BGMVolume", db);
			// 音量を保存
			bgmVolume = db;
			// セーブデータに保存
			GameInstance.saveData.bgmVolume = value;
		}
	}

	// SEの音量
	float seVolume = 0;
	public float SEVolume
	{
		get => seVolume;
		set
		{
			var db = Mathf.Sqrt(Mathf.Sqrt(value)) * 80 - 80;
			seVolume = db;
			// ミキサーに設定
			audioMixer.SetFloat("SEVolume", db);
			// 音量を保存
			seVolume = db;
			// セーブデータに保存
			GameInstance.saveData.seVolume = value;
		}
	}

	// 音を鳴らすオブジェクト
	[SerializeField]
	GameObject seRingObj;

	// bgmを鳴らしているsource
	AudioSource bgmSource;

	private void Start()
	{
		// 音量設定
		MasterVolume = GameInstance.saveData.masterVolume;
		BGMVolume = GameInstance.saveData.bgmVolume;
		SEVolume = GameInstance.saveData.seVolume;
		// ミキサーに設定する
		audioMixer.SetFloat("MasterVolume", masterVolume);
		audioMixer.SetFloat("BGMVolume", bgmVolume);
		audioMixer.SetFloat("SEVolume", seVolume);
		// BGMを鳴らしているsource取得
		bgmSource = Camera.main.GetComponent<AudioSource>();
	}

	/// <summary>
	/// BGM変更
	/// </summary>
	/// <param name="clip">変更するBGM</param>
	public async UniTask BGMChange(AudioClip clip)
	{
		bgmSource.Stop();
		// BGM設定
		bgmSource.clip = clip;
		// 少し待つ
		await UniTask.WaitForSeconds(0.1f);
		// BGM再生
		bgmSource?.Play();
	}

	/// <summary>
	/// SEを鳴らす
	/// </summary>
	/// <param name="clip">鳴らす音</param>
	public void SEPlay(AudioClip clip)
	{
		// 音を鳴らすオブジェクト生成
		var obj = Instantiate(seRingObj, gameObject.transform);
		// 音を鳴らすクラス取得
		var iSound = obj.GetComponent<ISound>();
		// 音を鳴らす
		iSound.Play(clip);
	}
}