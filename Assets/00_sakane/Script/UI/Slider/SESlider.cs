using UnityEngine;
using UnityEngine.EventSystems;

public class SESlider : MonoBehaviour, IPointerUpHandler
{
	// —£‚µ‚½‚Æ‚«‚ÌƒCƒxƒ“ƒg
	[SerializeField]
	//UnityEvent releaseEvent;

	AudioClip clip;

	public void OnPointerUp(PointerEventData eventData)
	{
		SoundManager.Instance.SEPlay(clip);
	}
}
