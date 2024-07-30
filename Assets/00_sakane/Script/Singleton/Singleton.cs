using System;
using UnityEngine;

// シングルトン
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	// インスタンス
	static T instance;
	// インスタンス取得
	public static T Instance
	{
		get
		{
			// nullなら生成
			if (instance == null)
			{
				Type t = typeof(T);
				instance = (T)FindObjectOfType(t);
				// 生成に失敗したらnullを返す
				if(instance == null )
				{
					Debug.LogError("オブジェクトが存在しません");
					return null;
				}
			}
			// インスタンスを返す
			return instance;
		}
	}

	// コンストラクタ
	void Awake()
	{
		instance = this as T;
	}

	private void OnDestroy()
	{
		instance = null;
	}

	protected virtual void Update()
	{
		
	}
}