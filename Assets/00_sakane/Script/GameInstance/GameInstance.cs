using UnityEngine;

// �Q�[���C���X�^���X
public class GameInstance
{
	// �C���X�^���X
	static GameInstance instance;
	// �C���X�^���X�擾
	public static GameInstance Instance
	{
		get => instance;
	}

	// �Z�[�u�f�[�^�̃p�X
	public static string saveFilePath = Application.persistentDataPath + "/saveData.json";
	// �Z�[�u�f�[�^
	public static SaveData saveData = new SaveData();

	/// <summary>
	/// �R���X�g���N�^
	/// </summary>
	private GameInstance()
	{

	}

	// �Q�[���J�n���ɐ���
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Initialize()
	{
		//Debug.Log(Application.persistentDataPath);
		instance = new GameInstance();
		// �Z�[�u�f�[�^�ǂݍ���(C:/Users/PC-0100-sakane/AppData/LocalLow/AiAiFactory/ButtocksGame)
		saveData = SaveLoadManager.LoadJson<SaveData>(saveFilePath);
	}

#if UNITY_EDITOR
	// �f�o�b�O�@�\
	public readonly static bool isDebug = true;
#endif
}