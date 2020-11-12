using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayersBuilding : MonoBehaviour
{
    public bool workerEnabled;
    [SerializeField] Image workerButton;
    [SerializeField] GameObject worker;
    public Timer workerTime;
    
    // Start is called before the first frame update
    void Start()
    {
        //buttonImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        if (gameManager_Script.ControlledCreatures.Contains(GameObject.Find("TownCenter")))
        {

        }
    }
}
