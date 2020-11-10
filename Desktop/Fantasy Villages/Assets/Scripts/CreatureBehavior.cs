using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureBehavior : MonoBehaviour
{
    bool UnderControl = false;
    private Vector3 movePoint;
    [SerializeField] private float speed = 30;

    // Start is called before the first frame update
    void Start()
    {
        movePoint = transform.position;
        InvokeRepeating("Move", 0.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        UnderControlCheck();
        MoveCommand();
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
                }
            }
        }
    }

     void Move()
    {

         transform.position = Vector3.MoveTowards(transform.position, movePoint, speed * Time.deltaTime); 
    }
}
