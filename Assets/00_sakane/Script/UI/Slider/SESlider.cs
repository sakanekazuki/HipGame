using UnityEngine;
using UnityEngine.EventSystems;

public class SESlider : MonoBehaviour, IPointerUpHandler
{
	// �������Ƃ��̃C�x���g
	[SerializeField]
	//UnityEvent releaseEvent;

	AudioClip clip;

	public void OnPointerUp(PointerEventData eventData)
	{
		SoundManager.Instance.SEPlay(clip);
	}
}
