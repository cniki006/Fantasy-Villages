using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameManager_Script : MonoBehaviour
{
    public List <GameObject> ControlledCreatures = new List<GameObject>();
    [SerializeField] public int gold;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChooseControlledCreatures();
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
                if (hitEvent.collider.transform.tag == "Building")
                {
                    //ControlledCreatures.Clear();
                    ControlledCreatures.Add(hitEvent.collider.gameObject);
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
            }
        }
    }
}
