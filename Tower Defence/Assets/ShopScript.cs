using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour
{

    public int xp;

    public int lifeLevel;
    public int moneyLevel;
    public int criticalDamageLevel;
    public int bonusDamageLevel;

    // Start is called before the first frame update
    void Start()
    {
        lifeLevel = PlayerPrefs.GetInt("BonusLife", 0);
        moneyLevel = PlayerPrefs.GetInt("BonusMoney", 0);
        criticalDamageLevel = PlayerPrefs.GetInt("BonusCritDMG", 0);
        bonusDamageLevel = PlayerPrefs.GetInt("BonusDamage",0);

        xp = PlayerPrefs.GetInt("Experience", 0);
        //all available money
        transform.GetChild(2).gameObject.GetComponent<Text>().text = xp.ToString();

        //set Life upgrade numbers
        //transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = "Upgrade (+1) " + ((lifeLevel+1) * 1000) +" coins";
        //transform.GetChild(4).gameObject.GetComponent<Text>().text = "Current bonus : +" +(lifeLevel);


        //purchase life text
        transform.GetChild(3).GetChild(1).gameObject.GetComponent<Text>().text = "Current bonus : +" +(lifeLevel) +"\n Next level cost " + ((lifeLevel+1) * 1000) +" xp  ( +1 )";
        //purchase money text
        transform.GetChild(4).GetChild(1).gameObject.GetComponent<Text>().text = "Current bonus : +" +(moneyLevel) +"\n Next level cost " + ((moneyLevel+1) * 1000) +" xp  ( +1 )";
        //critical damage multiplier
        transform.GetChild(5).GetChild(1).gameObject.GetComponent<Text>().text = "Current multiplier : +" +( 2 + ((float)criticalDamageLevel/4 )) +"\n Next level cost " + ((criticalDamageLevel+1)*(criticalDamageLevel+1))*1000 +" xp  ( +0.25 )";
        //tower bonus damage
        transform.GetChild(6).GetChild(1).gameObject.GetComponent<Text>().text = "Current damage bonus : +" +bonusDamageLevel +"\n Next level cost " + ((bonusDamageLevel * bonusDamageLevel + 1) *1000) +" xp  ( +1 )";
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
        if (xp >= (lifeLevel+1) * 1000) {
            xp -= (lifeLevel+1) * 1000;
            lifeLevel++;

            //set upgrade text to correspond new situation
            transform.GetChild(3).GetChild(1).gameObject.GetComponent<Text>().text = "Current bonus : +" +(lifeLevel) +"\n Next level cost " + ((lifeLevel+1) * 1000) +" xp  ( +1 )";
            //update overall experience
            transform.GetChild(2).gameObject.GetComponent<Text>().text = xp.ToString();

            //save new values to prefs
            PlayerPrefs.SetInt("BonusLife", lifeLevel);
            PlayerPrefs.SetInt("Experience", xp);
        }
        else {
            Debug.Log("Not Enough Money");
        }
    }

    public void buyMoney() {
        if (xp >= (moneyLevel+1) * 1000) {
            xp -= (moneyLevel+1) * 1000;
            moneyLevel++;

            //set upgrade text to correspond new situation
            transform.GetChild(4).GetChild(1).gameObject.GetComponent<Text>().text = "Current bonus : +" +(moneyLevel) +"\n Next level cost " + ((moneyLevel+1) * 1000) +" xp  ( +1 )";
            //update overall experience
            transform.GetChild(2).gameObject.GetComponent<Text>().text = xp.ToString();

            //save new values to prefs
            PlayerPrefs.SetInt("BonusMoney", moneyLevel);
            PlayerPrefs.SetInt("Experience", xp);
        }
        else {
            Debug.Log("Not Enough Money");
        }
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void buyCriticalDamage() {
        int cost = ((criticalDamageLevel+1)*(criticalDamageLevel + 1)) * 1000;

        if (xp >= cost) {
            xp -= cost;
            criticalDamageLevel++;
             cost = ((criticalDamageLevel+1)*(criticalDamageLevel+1)) * 1000;

            transform.GetChild(5).GetChild(1).gameObject.GetComponent<Text>().text = "Current multiplier : +" +( 2 + ((float)criticalDamageLevel/4 )) +"\n Next level cost " + cost +" xp  ( +0.25 )";
            transform.GetChild(2).gameObject.GetComponent<Text>().text = xp.ToString();

            PlayerPrefs.SetInt("BonusCritDMG", criticalDamageLevel);
            PlayerPrefs.SetInt("Experience", xp);
        }
    }

    public void buyDamage() {
        int cost = (bonusDamageLevel * bonusDamageLevel +1) *1000;

        if (xp >= cost) {
            xp -= cost;
            bonusDamageLevel++;
            cost = (bonusDamageLevel * bonusDamageLevel +1) *1000;

            transform.GetChild(6).GetChild(1).gameObject.GetComponent<Text>().text = "Current damage bonus : +" +bonusDamageLevel +"\n Next level cost " + cost +" xp  ( +1 )";
            transform.GetChild(2).gameObject.GetComponent<Text>().text = xp.ToString();

            PlayerPrefs.SetInt("BonusDamage", bonusDamageLevel);
            PlayerPrefs.SetInt("Experience", xp);
        }
    }
}
