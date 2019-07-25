using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class MouseOver: MonoBehaviour, IPointerEnterHandler{

    public AudioClip mouseOverAudio;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if(sceneName.Equals("Scene1")){
            transform.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
            transform.GetComponent<AudioSource>().Play();
        }
        else if(transform.GetComponent<Button>().interactable && !sceneName.Equals("Scene1")){
            AudioSource.PlayClipAtPoint(mouseOverAudio, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXvolume", 0.5f));
        }
    }
}
