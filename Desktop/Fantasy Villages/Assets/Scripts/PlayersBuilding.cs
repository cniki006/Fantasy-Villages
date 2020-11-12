using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayersBuilding : MonoBehaviour
{
    [SerializeField] Button button1;
    [SerializeField] GameObject unit1;
    [SerializeField] private int unit1price;
    [SerializeField] public float Health;
    public float unit1TrainingTime;
    private bool ClickBool = false;


    // Start is called before the first frame update
    void Start()
    {
        button1.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();

        if (gameManager_Script.ControlledCreatures.Contains(this.gameObject))
        {
            button1.gameObject.SetActive(true);
        }
        else { button1.gameObject.SetActive(false); }
        button1.onClick.AddListener(ClickButton1);
        CreateUnit1();
        if (Health <= 0) Destroy(this.gameObject);
    }
        
    void ClickButton1()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        if (gameManager_Script.gold >= unit1price)
        {
            ClickBool = true;
        }
    }
    void CreateUnit1()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        //workerTrainingTime -= Time.deltaTime;
        // if (workerTrainingTime <= 0)
        {
            if (ClickBool == true && gameManager_Script.gold >= unit1price)
            {
                Instantiate(unit1, new Vector3(transform.position.x, 0, transform.position.z-50), Quaternion.identity);
                ClickBool = false;
                gameManager_Script.gold -= unit1price;
            }
        }
        //workerTrainingTime = 10;
    }
}
