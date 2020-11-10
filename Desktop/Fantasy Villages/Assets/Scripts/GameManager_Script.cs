using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameManager_Script : MonoBehaviour
{
    List <GameObject> ControlledCreatures = new List<GameObject>();

    [SerializeField] private float speed;
    private Vector3 movingPoint = new Vector3(0,0,0);
    bool moveCommand = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveControlledCreatures", 0.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        ChooseControlledCreatures();
        GiveMoveCommand();
        MoveControlledCreatures();
    }

    void ChooseControlledCreatures ()
    {
        if (ControlledCreatures.Count != 0 && Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.A))
        {
            ControlledCreatures.Clear();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitEvent;
            if (Physics.Raycast(camRay, out hitEvent))
            {
                if (hitEvent.collider.transform.tag == "Creature")
                {
                    ControlledCreatures.Add(hitEvent.collider.gameObject);
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
            }
        }
    }

    void GiveMoveCommand ()
    {
        if (Input.GetMouseButtonDown(1))

        {
            Ray camRay1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitEvent1;
            if (Physics.Raycast(camRay1, out hitEvent1))
            {
                if (hitEvent1.collider.transform.tag == "Plane")
                {
                    movingPoint = new Vector3(hitEvent1.point.x, 23, hitEvent1.point.z);
                    //moveCommand = true;
                }
            }
        }
    }

    void MoveControlledCreatures()
    {
        for (int i = 0; i < ControlledCreatures.Count; i++)
        {
            ControlledCreatures[i].transform.position = Vector3.MoveTowards(ControlledCreatures[i].transform.position, movingPoint, speed * Time.deltaTime);
        }
    }
}
