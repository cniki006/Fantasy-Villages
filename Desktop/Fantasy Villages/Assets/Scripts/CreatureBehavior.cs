using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureBehavior : MonoBehaviour
{
    bool UnderControl = false;
    public bool mineCommand, bringingGold = false;
    GameObject mine;
    private Vector3 movePoint;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        movePoint = transform.position;
        InvokeRepeating("Move", 0.0f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {

        UnderControlCheck();
        MoveCommand();
        if (mineCommand == true) MineCommand(mine);
    }

    void UnderControlCheck ()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();
        if (gameManager_Script.ControlledCreatures.Contains(this.gameObject))
        {
            UnderControl = true;
        }
        else { UnderControl = false; }
    }

    void MoveCommand()
    {

        if (Input.GetMouseButtonDown(1) && UnderControl == true)

        {
            Ray camRay1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitEvent1;
            if (Physics.Raycast(camRay1, out hitEvent1))
            {
                if (hitEvent1.collider.transform.tag == "Plane")
                {
                    movePoint = new Vector3(hitEvent1.point.x,transform.position.y,hitEvent1.point.z);
                    mineCommand = false;
                }
                if (hitEvent1.collider.transform.tag == "GoldMine")
                {
                    mineCommand = true;
                    mine = hitEvent1.collider.gameObject;
                    
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();

        if (other.tag == "GoldMine" && mineCommand == true)
        {
            bringingGold = true;

        }

        if (other.name == "TownCenter" && mineCommand == true)
        {
            bringingGold = false;
            gameManager_Script.gold += 10;
        }
    }
    private void MineCommand(GameObject chosenMine)

    {

        if (bringingGold == false) movePoint = chosenMine.transform.position;
        else movePoint = GameObject.Find("TownCenter").transform.position;
    }


    void Move()
    {

         transform.position = Vector3.MoveTowards(transform.position, movePoint, speed * Time.deltaTime); 
    }
}
