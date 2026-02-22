using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("UISounds").GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSound); // plays when mouse hovers
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(clickSound); // plays when clicked
    }
}

