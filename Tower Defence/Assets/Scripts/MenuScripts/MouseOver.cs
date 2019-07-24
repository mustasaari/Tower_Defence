using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class MouseOver: MonoBehaviour, IPointerEnterHandler{

    public AudioClip mouseOverAudio;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioSource.PlayClipAtPoint(mouseOverAudio, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXvolume", 0.5f));
    }
}
