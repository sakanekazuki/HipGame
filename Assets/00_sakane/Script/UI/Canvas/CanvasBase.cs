using UnityEngine;
using UnityEngine.UI;

// キャンバス基底クラス
public class CanvasBase : MonoBehaviour
{
	protected virtual void Start()
	{
		// カメラの設定
		GetComponent<Canvas>().worldCamera = Camera.main;
		GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
	}
}