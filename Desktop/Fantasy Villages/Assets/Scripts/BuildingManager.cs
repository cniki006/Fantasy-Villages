using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> Buildings = new List<GameObject>();
    [SerializeField] List<int> buildingPrices;
    [SerializeField] List<Button> buttons;
    [SerializeField] Canvas buildingMenu;
    private bool ClickBool = false;
    public int synchronizer;
    private GameObject buildingPlanner;





    // Start is called before the first frame update
    void Start()
    {
        buildingMenu.gameObject.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        ControlledWorkerCheck();
        CreateBuilding();
        ChooseLocation();
        RayCastCheck();
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
                            synchronizer = i;
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
        if (gameManager_Script.gold >= buildingPrices[synchronizer])
        {
            ClickBool = true;
            gameManager_Script.ControlledCreatures.Clear();
        }
    }

    void ControlledWorkerCheck()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        if (gameManager_Script.ControlledCreatures.Count != 0)
        {
            for (int i = 0; i < gameManager_Script.ControlledCreatures.Count; i++)
            {
                GameObject builderCheck = gameManager_Script.ControlledCreatures[i];
                CreatureBehavior builderScript = builderCheck.GetComponent<CreatureBehavior>();
                if (gameManager_Script.ControlledCreatures[i].tag == "Building") buildingMenu.gameObject.SetActive(false);
                if (gameManager_Script.ControlledCreatures[i].tag == "Creature")
                {
                    if (builderScript.builder == true && gameManager_Script.ControlledCreatures.Count != 0) buildingMenu.gameObject.SetActive(true);
                }
                if (gameManager_Script.ControlledCreatures[i].tag == "Building") buildingMenu.gameObject.SetActive(false);
            }
        }
        if (gameManager_Script.ControlledCreatures.Count == 0) buildingMenu.gameObject.SetActive(false);
    }

    void CreateBuilding()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        //workerTrainingTime -= Time.deltaTime;
        // if (workerTrainingTime <= 0)
        {
            if (ClickBool == true && gameManager_Script.gold >= buildingPrices[synchronizer])
            {
                Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitEvent;
                Physics.Raycast(camRay, out hitEvent);
                buildingPlanner = Instantiate(buildings[synchronizer], new Vector3(hitEvent.point.x, 0, hitEvent.point.z),Quaternion.identity);
                ClickBool = false;               
            }
        }
        //workerTrainingTime = 10;
    }

    void ChooseLocation()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitEvent;
        Physics.Raycast(camRay, out hitEvent);
        if (buildingPlanner != null)
        {
            buildingPlanner.transform.position = new Vector3(hitEvent.point.x, 0, hitEvent.point.z);
        }
        if (Input.GetMouseButtonDown(1)) { Destroy(buildingPlanner); }
        if (Input.GetMouseButtonDown(0) && buildingPlanner != null)
        {
            Buildings.Add(Instantiate(buildingPlanner, buildingPlanner.transform.position, Quaternion.identity));
            gameManager_Script.gold -= buildingPrices[synchronizer];
            Destroy(buildingPlanner);
        }
    }
}
