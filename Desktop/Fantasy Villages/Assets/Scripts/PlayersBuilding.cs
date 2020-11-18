using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayersBuilding : MonoBehaviour
{
    [SerializeField] List <Button> buttons;
    [SerializeField] List <GameObject> units;
    [SerializeField] List <int> unitPrices;
    [SerializeField] public float BHealth;
    public float unit1TrainingTime;
    private bool ClickBool = false;
    public int synchronizerBuilding = 0;
    public bool house;
    public int houseLivingSpace;
    

    [SerializeField] Canvas buildingMenu;


    // Start is called before the first frame update
    void Start()
    {


        for (int i=0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            BHealth -= 10;
        }
    }



    // Update is called once per frame
    void Update()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();

        if (gameManager_Script.ControlledCreatures.Contains(this.gameObject))
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }
        }
        else {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
        HouseCheck();
        RayCastCheck();
        CreateUnit();

        if (BHealth <= 0) Destroy(this.gameObject);

    }

    void HouseCheck()
    {
        HUD hud_script = GameObject.Find("HUD").GetComponent<HUD>();
        if (house == true)
        {
            Debug.Log("This is a little house");
            BuildingManager b_script = GameObject.Find("BuilingManager").GetComponent<BuildingManager>();
            for (int i = 0; i < b_script.Buildings.Count; i++)
            {
                Debug.Log("This is a fine house");
                if (b_script.Buildings[i] == this.gameObject)
                {
                    hud_script.unitTreshold += houseLivingSpace;
                    house = false;
                }
            }


        }
    }

    void RayCastCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject GameManager = GameObject.Find("GameManager");
            GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();           
            foreach (RaycastResult result in gameManager_Script.RaycastResults(buildingMenu))
            {
                if (result.gameObject.tag == "Button")
                {
                    Debug.Log("ButtonClicked");

                    for (int i = 0; i < buttons.Count; i++)
                    {
                        if (result.gameObject.name == (buttons[i]).name)
                        {
                            synchronizerBuilding = i;
                            ClickButton();
                        }
                    }
                }
            }
        }
    }

    void ClickButton()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        if (gameManager_Script.gold >= unitPrices[synchronizerBuilding])
        {
            ClickBool = true;
        }
    }
    void CreateUnit()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        HUD hud_script = GameObject.Find("HUD").GetComponent<HUD>();
        //workerTrainingTime -= Time.deltaTime;
        // if (workerTrainingTime <= 0)
        {
            if (ClickBool == true && gameManager_Script.gold >= unitPrices[synchronizerBuilding] && hud_script.unitTreshold > gameManager_Script.allCreatures.Length)
            {
                Instantiate(units[synchronizerBuilding], new Vector3(transform.position.x, 0, transform.position.z-50), Quaternion.identity);
                ClickBool = false;
                gameManager_Script.gold -= unitPrices[synchronizerBuilding];
            }
        }
        //workerTrainingTime = 10;
    }
}
