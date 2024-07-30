// �ۑ��ǂݍ��݊Ǘ��N���X
using System.IO;
using UnityEngine;

public class SaveLoadManager : ManagerBase<SaveLoadManager>
{
	private void OnApplicationQuit()
	{
		SaveJson(GameInstance.saveData, GameInstance.saveFilePath);
	}

	/// <summary>
	/// Json�t�@�C������ǂݍ���
	/// </summary>
	/// <param name="fileName">�t�@�C����</param>
	/// <returns>�ǂݍ��񂾃f�[�^</returns>
	public static T LoadJson<T>(string fileName) where T : new()
	{
		// �t�@�C���̗L���𒲂ׂ�
		if (!File.Exists(fileName))
		{
			Debug.Log("�t�@�C�������݂��܂���");
			return new T();
		}
		// �ǂݍ��ރt�@�C�����J��
		StreamReader reader = new StreamReader(fileName);
		// �ǂݍ���
		var jsondata = reader.ReadToEnd();
		// �f�[�^���N���X��
		var data = JsonUtility.FromJson<T>(jsondata);
		// �t�@�C������
		reader.Close();

		return data;
	}

	/// <summary>
	/// Json�t�@�C���ɕۑ�
	/// </summary>
	/// <param name="t">�������ރf�[�^</param>
	/// <param name="fileName">�������݂��s���t�@�C����</param>
	public static void SaveJson<T>(T t, string fileName)
	{
		// �ۑ�����t�@�C�����J��
		StreamWriter writer = new StreamWriter(fileName);

		// �ۑ�����N���X��string�ɕϊ�
		var jsonStr = JsonUtility.ToJson(t);
		// Json�ɏ�������
		writer.Write(jsonStr);
		// �o�b�t�@�N���A
		writer.Flush();
		// �t�@�C�������
		writer.Close();
	}

	public static void SaveJsonBinary<T>(T t, string fileName)
	{

		var stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
		// �ۑ�����t�@�C�����J��
		StreamWriter writer = new StreamWriter(fileName);

		// �ۑ�����N���X��string�ɕϊ�
		var jsonStr = JsonUtility.ToJson(t);
		// Json�ɏ�������
		writer.Write(jsonStr);
		// �o�b�t�@�N���A
		writer.Flush();
		// �t�@�C�������
		writer.Close();
	}
}