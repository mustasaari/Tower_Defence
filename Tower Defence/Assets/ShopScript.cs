using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour
{

    public int money;

    public int lifeLevel;

    // Start is called before the first frame update
    void Start()
    {
        lifeLevel = PlayerPrefs.GetInt("BonusLife", 0);
        money = PlayerPrefs.GetInt("Money", 0);
        //all available money
        transform.GetChild(2).gameObject.GetComponent<Text>().text = money.ToString();

        //set Life upgrade numbers
        transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = "Upgrade (+1) " + ((lifeLevel+1) * 1000) +" coins";
        transform.GetChild(4).gameObject.GetComponent<Text>().text = "Current bonus : +" +(lifeLevel);
    }

    // Update is called once per frame
    void Update()
    {
        //Delete all playerprefs
        if (Input.GetKeyDown(KeyCode.J)) {
            Debug.Log("PlayerPrefs Deleted");
            PlayerPrefs.DeleteAll();
        }
    }

    public void buyLife() {
        if (money >= (lifeLevel+1) * 1000) {
            money -= (lifeLevel+1) * 1000;
            lifeLevel++;
            transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = "Upgrade (+1) " + ((lifeLevel +1) * 1000) +" coins";
            transform.GetChild(4).gameObject.GetComponent<Text>().text = "Current bonus : +" +(lifeLevel);
            transform.GetChild(2).gameObject.GetComponent<Text>().text = money.ToString();
            PlayerPrefs.SetInt("BonusLife", lifeLevel);
            PlayerPrefs.SetInt("Money", money);
        }
        else {
            Debug.Log("Not Enough Money");
        }
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }
}
