using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    [SerializeField] private Text goldText;
    [SerializeField] private Text killText;
    [SerializeField] private Text defeat;
    public int killAmount = 0;
 
    public void SetGold(int value) { goldText.text = System.String.Format("{0}: {1}","Gold",value) ; }
    public void KillCount(int value) { killText.text = System.String.Format("{0}: {1}", "Killed orcs", value); }
    // Start is called before the first frame update
    void Start()
    {
        defeat.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        GameObject TownCenter = GameObject.Find("TownCenter");
        PlayersBuilding playersBuilding = TownCenter.GetComponent<PlayersBuilding>();
        if (playersBuilding.BHealth <= 0)
        { defeat.gameObject.SetActive(true); 
        }
        SetGold(gameManager_Script.gold);
        KillCount(killAmount);
    }
}
