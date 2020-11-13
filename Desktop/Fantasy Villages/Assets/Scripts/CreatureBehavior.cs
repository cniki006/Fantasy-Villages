using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureBehavior : MonoBehaviour
{
    bool UnderControl = false;
    [SerializeField] public bool builder;
    [SerializeField] public bool soldier;
    [SerializeField] public float health;
    public bool mineCommand, bringingGold, fightCommand = false;
    private GameObject fighterTarget;
    
    GameObject mine;
    GameObject enemy;
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
        if (fightCommand == true) FightCommand(enemy);
        if (health <= 0) Destroy(this.gameObject);
        //FighterStation();
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

    //void FighterStation()
    //{
    //    FightCommand(GameObject.Find("Orc")
    //    if (Vector3.Distance(transform.position, GameObject.Find("Orc").transform.position) <= 20000.0f && soldier == true)
    //    {
    //        fightCommand = true;
    //        FightCommand(GameObject.Find("Orc"));
    //    }
    //}

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
                    fightCommand = false;
                }
                if (hitEvent1.collider.transform.tag == "GoldMine" && builder == true)
                {
                    mineCommand = true;
                    mine = hitEvent1.collider.gameObject;
                    
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
                if (hitEvent1.collider.transform.tag == "Enemy" && soldier == true)
                {
                    enemy = hitEvent1.collider.gameObject;
                    fightCommand = true;
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
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

        if (other.tag == "Enemy" && fightCommand == true)
        {
            Enemy enemyStats = other.GetComponent<Enemy>();
            enemyStats.HP -= 10;
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

    private void FightCommand(GameObject chosenEnemy)
    {

        movePoint = enemy.transform.position + new Vector3(Random.Range(-100,100),0,Random.Range(-5,5));

    }
}
