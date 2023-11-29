using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonDriver : MonoBehaviour
{
    public Button myButton;
    public Tower towerComp;

    void Start()
    {
        
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(UpgradeTower);
    }
    void Update()
    {
        if( towerComp.GetPrice()<GameManager.playerScore)
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }
    }
    void UpgradeTower()
    {
        towerComp.Upgrade();
        

    }
}
