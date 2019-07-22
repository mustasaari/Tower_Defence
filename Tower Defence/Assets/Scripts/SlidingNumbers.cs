using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlidingNumbers : MonoBehaviour
{
    public Text expCounter;
    public float animTime = 1.5f;

    private float desiredNumber;
    private float initialNumber;
    private float currentNumber;
    private bool startCount;

    void Update(){
        if(currentNumber != desiredNumber && startCount){
            // startPlayingMusic();
            if(initialNumber < desiredNumber){
                currentNumber += (animTime * Time.deltaTime) * (desiredNumber - initialNumber);
                if(currentNumber >= desiredNumber){
                    currentNumber = desiredNumber;
                    transform.parent.GetComponent<AudioSource>().Stop();
                    setCountBool(false);
                }
            }
            else{
                currentNumber -= (animTime * Time.deltaTime) * (initialNumber - desiredNumber);
                if(currentNumber <= desiredNumber){
                    currentNumber = desiredNumber;
                    transform.parent.GetComponent<AudioSource>().Stop();
                    setCountBool(false);
                }
            }
            expCounter.text = currentNumber.ToString("0");
        }
    }

    public void setNumber(float v){
        initialNumber = currentNumber;
        desiredNumber = v;
    }

    public void addToNumber(float v){
        initialNumber = currentNumber;
        desiredNumber += v;
    }

    public void setCountNumbers(int start, int end){
        currentNumber = start;
        desiredNumber = end;
    }

    public void setCountBool(bool a){
        startCount = a;
    }

    public bool getCountBool(){
        return startCount;
    }
}
