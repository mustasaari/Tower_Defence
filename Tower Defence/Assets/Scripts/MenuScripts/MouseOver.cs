using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class MouseOver: MonoBehaviour, IPointerEnterHandler{

    public AudioClip mouseOverAudio;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuff
        Debug.Log("Mouse OVER!!!");
        AudioSource.PlayClipAtPoint(mouseOverAudio, Camera.main.transform.position);
    }
}
