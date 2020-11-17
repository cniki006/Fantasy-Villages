using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayersBuilding : MonoBehaviour
{
    [SerializeField] List <Button> buttons;
    [SerializeField] List <GameObject> units;
    [SerializeField] List <int> unitPrices;
    [SerializeField] public float BHealth;
    public float unit1TrainingTime;
    private bool ClickBool = false;
    public int synchronizerBuilding = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        InvokeRepeating("ButtonCheck",0.0f,0.01f) ;
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


        CreateUnit();
        ButtonCheck();
        if (BHealth <= 0) Destroy(this.gameObject);

    }

    void ButtonCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitEvent;
            Physics.Raycast(camRay, out hitEvent);
            if (Physics.Raycast(camRay, out hitEvent))
            {
                if (hitEvent.collider.transform.tag == "Button")
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        if (hitEvent.collider.gameObject.Equals(buttons[i])) { synchronizerBuilding = i; }
                    }
                    //Debug.Log(hitEvent.collider.gameObject.name);
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
        //workerTrainingTime -= Time.deltaTime;
        // if (workerTrainingTime <= 0)
        {
            if (ClickBool == true && gameManager_Script.gold >= unitPrices[synchronizerBuilding])
            {
                Instantiate(units[synchronizerBuilding], new Vector3(transform.position.x, 0, transform.position.z-50), Quaternion.identity);
                ClickBool = false;
                gameManager_Script.gold -= unitPrices[synchronizerBuilding];
            }
        }
        //workerTrainingTime = 10;
    }
}
