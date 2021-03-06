﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameManager_Script : MonoBehaviour
{
    public List <GameObject> ControlledCreatures = new List<GameObject>();
    [SerializeField] public int gold;
    public float mouseHoldTimer = 1;
    Vector3 mouseHoldVector;
    float lengthX, lengthZ;
    float directionX, directionY;
    [SerializeField] GameObject hud;
    [SerializeField] public GameObject[] allCreatures;
    [SerializeField] public GameObject[] allEnemies;
    [SerializeField] public GameObject drawPoint;
    [SerializeField]List<GameObject> drawPoints = new List<GameObject>();
    Vector3 lineCheck;
    bool boxCheck = false, bound1 = false, bound2 = false;

    [SerializeField] public GameObject menu;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    [SerializeField] Text a, b, c;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1024, 768, true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChooseControlledCreatures();
        allCreatures = GameObject.FindGameObjectsWithTag("Creature");
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
 

        if (Input.GetKey(KeyCode.Escape))
        {
            menu.SetActive(true);
        }

        if (menu.gameObject.activeSelf == false)
        {
            Time.timeScale = 1;
        }

        if (menu.gameObject.activeSelf == true)
        {
            Time.timeScale = 0;
        }

        if (menu.gameObject.activeSelf == true)
        {
            a.gameObject.SetActive(false);
            b.gameObject.SetActive(false);
            c.gameObject.SetActive(false);
        }
        
        if (menu.gameObject.activeSelf == false)
        {
            a.gameObject.SetActive(true);
            b.gameObject.SetActive(true);
            c.gameObject.SetActive(true);
        }

        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    menu.gameObject.SetActive(true);
        //}

    }

    public List<RaycastResult> RaycastResults(Canvas menu)
    {
        m_Raycaster = menu.GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        //Check if the left Mouse button is clicked

            //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        return results;
    }

    void ChooseControlledCreatures ()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitEvent;
        Physics.Raycast(camRay, out hitEvent);
        if (ControlledCreatures.Count != 0 && Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.A) && !(Input.mousePosition.x > Screen.width-200 && Input.mousePosition.y < 120))
        {
                    ControlledCreatures.Clear();
        }    
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camRay, out hitEvent))
            {
                if (hitEvent.collider.transform.tag == "Creature")
                {
                    ControlledCreatures.Add(hitEvent.collider.gameObject);
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
                if (hitEvent.collider.transform.tag == "Building")
                {
                    ControlledCreatures.Clear();
                    ControlledCreatures.Add(hitEvent.collider.gameObject);
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
           // mouseHoldTimer -= Time.deltaTime;
            //if (mouseHoldTimer <= 0)
            {
              //  mouseHoldTimer = 0;
                Physics.Raycast(camRay, out hitEvent);
                mouseHoldVector = hitEvent.point;
                drawControlledAreaPoints();
             
            }
        }
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(camRay, out hitEvent);
            //Debug.Log("resize");
            resizeControlledArea(mouseHoldVector, hitEvent.point);

        }
        if (Input.GetMouseButtonUp(0))
        {
            Physics.Raycast(camRay, out hitEvent);
            for (int i1 = 0; i1 < drawPoints.Count; i1++){

                Destroy(drawPoints[i1]);
            }
            drawPoints.Clear();
            insideControllAreaCheck(mouseHoldVector, hitEvent.point);
        }
    }

    void insideControllAreaCheck(Vector3 a, Vector3 b)
    {

        for (int i = 0; i < allCreatures.Length; i++)
        {
            boxCheck = false;

            lineCheck = allCreatures[i].transform.position;
            if (a.z > b.z)
            {
                if (lineCheck.z > b.z & lineCheck.z < a.z) { boxCheck = true; }
            }
            else if  (lineCheck.z < b.z & lineCheck.z > a.z) { boxCheck = true; }
            else boxCheck = false;

            if (boxCheck == true)
            {
                bound2 = false;
                bound2 = false;
                boxCheck = false;
                for (int i1 = 0; i1 < Screen.width - lineCheck.x; i1++)
                {
                    if ((int)(lineCheck.x + i1) == (int)(a.x))
                    {
                        bound1 = true;
  
                    }
                    if ((int)(lineCheck.x + i1) == (int)(b.x))
                    {
                        bound2 = true;
                    }
                }
                if (bound1 == true && bound2 == false)
                {
                    ControlledCreatures.Add(allCreatures[i]);
                }
                else if (bound1 == false && bound2 == true)
                {
                    ControlledCreatures.Add(allCreatures[i]);
                }
                bound1 = false;
                bound2 = false;
            }
        }
    }

    void drawControlledAreaPoints()
    {
        for (int i = 0; i <= 40; i++)
        {
            drawPoints.Add(Instantiate(drawPoint, Vector3.zero, Quaternion.Euler(90, 0, 0)));
        }
    }

    void resizeControlledArea(Vector3 pointStart, Vector3 pointEnd)
    {
        lengthX = Vector3.Distance(new Vector3(pointStart.x, 0, 0), new Vector3(pointEnd.x, 0, 0));
        lengthZ = Vector3.Distance(new Vector3(0, 0, pointStart.z), new Vector3(0, 0, pointEnd.z));
        if (pointStart.x < pointEnd.x) directionX = 1; else directionX = -1;
        if (pointStart.z < pointEnd.z) directionY = 1; else directionY = -1;

        for (int i = 0; i < 10; i++)
        {
            drawPoints[i].transform.position = new Vector3(pointStart.x + i * (lengthX / 10) * directionX, 10, pointStart.z);
        }

        for (int i = 0; i < 10; i++)
        {
            drawPoints[i+10].transform.position = new Vector3(pointStart.x, 10, pointStart.z + i * (lengthZ / 10) * directionY);
        }

        for (int i = 0; i < 10; i++)
        {
            drawPoints[i+20].transform.position = new Vector3(pointStart.x + i * (lengthX / 10) * directionX, 10, pointEnd.z);
        }

        for (int i = 0; i <= 10; i++)
        {
            drawPoints[i+30].transform.position = new Vector3(pointEnd.x, 10, pointStart.z + i * (lengthZ / 10) * directionY);
        }
    }

}

