using UnityEngine;

// ゲームインスタンス
public class GameInstance
{
	// インスタンス
	static GameInstance instance;
	// インスタンス取得
	public static GameInstance Instance
	{
		get => instance;
	}

	// セーブデータのパス
	public static string saveFilePath = Application.persistentDataPath + "/saveData.json";
	// セーブデータ
	public static SaveData saveData = new SaveData();

	/// <summary>
	/// コンストラクタ
	/// </summary>
	private GameInstance()
	{

	}

	// ゲーム開始時に生成
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Initialize()
	{
		//Debug.Log(Application.persistentDataPath);
		instance = new GameInstance();
		// セーブデータ読み込み(C:/Users/PC-0100-sakane/AppData/LocalLow/AiAiFactory/ButtocksGame)
		saveData = SaveLoadManager.LoadJson<SaveData>(saveFilePath);
	}

#if UNITY_EDITOR
	// デバッグ機能
	public readonly static bool isDebug = true;
#endif
}