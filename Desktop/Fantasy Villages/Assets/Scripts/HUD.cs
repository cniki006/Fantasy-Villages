using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text goldText;
    [SerializeField] Button workerButton;
    [SerializeField] GameObject worker;

    public void SetGold(int value) { goldText.text = System.String.Format("{0}: {1}","Gold",value) ; }
    // Start is called before the first frame update
    void Start()
    {
        workerButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        SetGold(gameManager_Script.gold);


        if (gameManager_Script.ControlledCreatures.Contains(GameObject.Find("TownCenter")))
        {
            workerButton.gameObject.SetActive(true);
        } else workerButton.gameObject.SetActive(false);

        if (gameManager_Script.gold >= 100) {
          //  workerButton.onClick.CreateWorker;
        }
    }

    void CreateWorker()
    {
        //Instantiate(worker, Vector3(20, 0, 20), Quaternion.identity.Normalize);
    }
}
