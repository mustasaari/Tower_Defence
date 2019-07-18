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

    public void setNumber(float v){
        initialNumber = currentNumber;
        desiredNumber = v;
    }

    public void addToNumber(float v){
        initialNumber = currentNumber;
        desiredNumber += v;
    }

    void Update(){
        if(currentNumber != desiredNumber && startCount){
            if(initialNumber < desiredNumber){
                currentNumber += (animTime * Time.deltaTime) * (desiredNumber - initialNumber);
                if(currentNumber >= desiredNumber){
                    currentNumber = desiredNumber;
                }
            }
            else{
                currentNumber -= (animTime * Time.deltaTime) * (initialNumber - desiredNumber);
                if(currentNumber <= desiredNumber){
                    currentNumber = desiredNumber;
                }
            }
        }
    }
    public static void setCountBool(){

    }
}
