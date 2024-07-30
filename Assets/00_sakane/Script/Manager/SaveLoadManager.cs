// 保存読み込み管理クラス
using System.IO;
using UnityEngine;

public class SaveLoadManager : ManagerBase<SaveLoadManager>
{
	private void OnApplicationQuit()
	{
		SaveJson(GameInstance.saveData, GameInstance.saveFilePath);
	}

	/// <summary>
	/// Jsonファイルから読み込み
	/// </summary>
	/// <param name="fileName">ファイル名</param>
	/// <returns>読み込んだデータ</returns>
	public static T LoadJson<T>(string fileName) where T : new()
	{
		// ファイルの有無を調べる
		if (!File.Exists(fileName))
		{
			Debug.Log("ファイルが存在しません");
			return new T();
		}
		// 読み込むファイルを開く
		StreamReader reader = new StreamReader(fileName);
		// 読み込み
		var jsondata = reader.ReadToEnd();
		// データをクラス化
		var data = JsonUtility.FromJson<T>(jsondata);
		// ファイル閉じる
		reader.Close();

		return data;
	}

	/// <summary>
	/// Jsonファイルに保存
	/// </summary>
	/// <param name="t">書き込むデータ</param>
	/// <param name="fileName">書き込みを行うファイル名</param>
	public static void SaveJson<T>(T t, string fileName)
	{
		// 保存するファイルを開く
		StreamWriter writer = new StreamWriter(fileName);

		// 保存するクラスをstringに変換
		var jsonStr = JsonUtility.ToJson(t);
		// Jsonに書き込み
		writer.Write(jsonStr);
		// バッファクリア
		writer.Flush();
		// ファイルを閉じる
		writer.Close();
	}

	public static void SaveJsonBinary<T>(T t, string fileName)
	{

		var stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
		// 保存するファイルを開く
		StreamWriter writer = new StreamWriter(fileName);

		// 保存するクラスをstringに変換
		var jsonStr = JsonUtility.ToJson(t);
		// Jsonに書き込み
		writer.Write(jsonStr);
		// バッファクリア
		writer.Flush();
		// ファイルを閉じる
		writer.Close();
	}
}