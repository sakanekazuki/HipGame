using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// ƒXƒe[ƒW
public class Stage : Singleton<Stage>
{
	// ” 
	[SerializeField]
	GameObject boxObj;

	// true = —h‚ç‚¹‚é
	[NonSerialized]
	public bool canShake = true;

	// —h‚ê‚é‘¬“x
	[SerializeField]
	float speed = 0.08f;

	// ‰ŠúˆÊ’u
	Vector3 startPosition = Vector3.zero;

	// ‚¨K‰æ‘œ
	[SerializeField]
	List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
	List<Animator> rightButtockAnimator = new List<Animator>();

	// Å‘åŠp“x
	[SerializeField]
	float maxAngle = 15.0f;

	// true = —h‚ç‚µ‚Ä‚¢‚é
	public bool IsShaking
	{
		get;
		private set;
	}

	private void Start()
	{
		// ‰ŠúˆÊ’uİ’è
		startPosition = boxObj.transform.position;
		var isMakes = GameInstance.saveData.isMakes;
		for (int i = 0; i < isMakes.Count; ++i)
		{
			if (isMakes[i])
			{
				// ‚¨K‚ğì‚Á‚Ä‚¢‚éê‡”’
				spriteRenderers[i].color = Color.white;
			}
			else
			{
				// ‚¨K‚ğì‚Á‚Ä‚¢‚È‚¢ê‡•
				spriteRenderers[i].color = Color.black;
			}
		}
		IsShaking = false;

		// ‚¨K‚ÌƒAƒjƒ[ƒ^[æ“¾
		foreach (var v in spriteRenderers)
		{
			rightButtockAnimator.Add(v.GetComponent<Animator>());
		}
	}

	/// <summary>
	/// —h‚ç‚·
	/// </summary>
	public void Shake()
	{
		var token = this.GetCancellationTokenOnDestroy();
		ShakeAsync(token).Forget();
	}

	/// <summary>
	/// —h‚ç‚·
	/// </summary>
	/// <returns></returns>
	async UniTask ShakeAsync(CancellationToken token)
	{
		IsShaking = true;
		float progress = 0;
		while (true)
		{
			boxObj.transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(progress) * maxAngle);

			// ˆÚ“®‚³‚¹‚é
			//boxObj.transform.position =
			//	new Vector3(startPosition.x + (Mathf.Sin(progress) * swingWidth), startPosition.y);

			// 1ƒtƒŒ[ƒ€‘Ò‚Â
			await UniTask.DelayFrame(1, cancellationToken: token);
			// i’»‚ği‚ß‚é
			progress += speed;
			// i’»‚ª4ƒÎi2üj‚µ‚½‚çI‚í‚è
			if (progress >= 4 * Mathf.PI)
			{
				break;
			}
		}
		// ŠJnˆÊ’u‚É–ß‚·
		boxObj.transform.position = startPosition;
		// ‰ñ“]‚ğ–ß‚·
		boxObj.transform.localEulerAngles = Vector3.zero;

		IsShaking = false;
	}

	/// <summary>
	/// ‚¨K‚ğì‚Á‚½
	/// </summary>
	/// <param name="number">ì‚Á‚½‚¨K‚Ì”Ô†</param>
	public void MakeButtocks(int number)
	{
		spriteRenderers[number].color = Color.white;
		GameInstance.saveData.isMakes[number] = true;
	}

	/// <summary>
	/// ‰E‚Ì‚¨K‚ğ—h‚ç‚·
	/// </summary>
	/// <param name="number">—h‚ç‚·‚¨K‚Ì”Ô†</param>
	public void RightButtockShake(int number)
	{
		rightButtockAnimator[number].SetTrigger("IsShake");
	}
}