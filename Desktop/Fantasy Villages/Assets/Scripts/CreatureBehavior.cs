using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureBehavior : MonoBehaviour
{
    bool UnderControl = false;
    //[SerializeField] private GameObject range;
    [SerializeField] public bool builder;
    [SerializeField] public bool soldier;
    [SerializeField] public bool archer;
    [SerializeField] private GameObject arrow;
    [SerializeField] public float health;

    public int goldAdd;

    public bool mineCommand, bringingGold, fightCommand = false;
    private GameObject fighterTarget;
    Vector3 tempVect;
    GameObject mine;
    public GameObject enemy;
    private Vector3 movePoint;
    [SerializeField] private float speed;
    private float tempSpeed;
    public float timer = 0.5f;
    public float shootSpeed;
    public bool shootCommand = false;
    List <GameObject> flyingArrows;

    public bool hasShotArrow = false;

    // Start is called before the first frame update
    void Start()
    {
        tempSpeed = speed;
        movePoint = transform.position;
        InvokeRepeating("Move", 0.0f, 0.01f);
        InvokeRepeating("ArrowMove", 0.0f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //speed = tempSpeed;
        UnderControlCheck();
        MoveCommand();
        Archery();

        GameManager_Script menuCheck = GameObject.Find("GameManager").GetComponent<GameManager_Script>();

        if (menuCheck.menu.activeSelf == true)
        {
            speed = 0;
        }
        else
        {
            speed = tempSpeed;
        }

        if (mineCommand == true) MineCommand(mine);
        if (fightCommand == true) FightCommand(enemy);
        if (health <= 0) Destroy(this.gameObject);
        //FighterStation();
        if (enemy != null)
        {
            enemy = enemy;
        } else
        {
            enemy = null;
        }

        if (enemy == null)
        {
            for (int i = 0; i < flyingArrows.Count; i++)
            {
                Destroy(flyingArrows[i]);
                flyingArrows.RemoveAt(i);
            }
        }
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
                    fightCommand = false;
                    shootCommand = false;
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

                if (hitEvent1.collider.transform.tag == "Enemy" && archer == true)
                {
                    enemy = hitEvent1.collider.gameObject;
                    shootCommand = true;
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
            }
        }
    }

    void Archery()
    {
        if (enemy == null)
        {
            shootCommand = false;
        }

        if (arrow != null && shootCommand == true && timer <= 0 && hasShotArrow == false)
        {

             timer = 1.2f;
            hasShotArrow = true;
        }      

        if (timer <= 0)
        {
            hasShotArrow = false;
            timer = 0;
        }
        if (hasShotArrow == true)
        {
            /*flyingArrows.Add(*/Instantiate(arrow, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity)/*)*/;
        }
    }

    void ArrowMove()
    {
        for (int i = 0; i < flyingArrows.Count; i++)
        {
            flyingArrows[i].transform.position = Vector3.MoveTowards(flyingArrows[i].transform.position, enemy.transform.position, 200 * Time.deltaTime);
            flyingArrows[i].transform.LookAt(enemy.transform.position);
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
            gameManager_Script.gold += goldAdd;
        }

        if (other.tag == "Enemy" && fightCommand == true)
        {
            Enemy enemyStats = other.GetComponent<Enemy>();
            enemyStats.HP -= 10;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Creature")
        {
            //if (timer >= 0)
            //{
                SolveCollision(other.gameObject);
            //}
        }
    }

    void SolveCollision(GameObject other)
    {
        tempVect = movePoint;
        timer = 1f;
        
        if (timer >= 0)
        {
            movePoint.x= (2 * transform.position.x)-other.transform.position.x;
            movePoint.z = (2 * transform.position.z) - other.transform.position.z;
            movePoint.z = 10;

        }
        movePoint = tempVect;
    }

    private void OnTriggerExit(Collider other)
    {
        speed = tempSpeed;
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
