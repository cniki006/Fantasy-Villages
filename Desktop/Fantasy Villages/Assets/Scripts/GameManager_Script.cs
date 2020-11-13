using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameManager_Script : MonoBehaviour
{
    public List <GameObject> ControlledCreatures = new List<GameObject>();
    [SerializeField] public int gold;
    float mouseHoldTimer = 1;
    Vector3 mouseHoldVector;
    float lengthX, lengthZ;
    float directionX, directionY;
    [SerializeField] GameObject[] allCreatures;
    [SerializeField] public GameObject drawPoint;
    [SerializeField]List<GameObject> drawPoints = new List<GameObject>();
    Vector3 bound1, bound2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChooseControlledCreatures();
        allCreatures = GameObject.FindGameObjectsWithTag("Creature");
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
                    //ControlledCreatures.Clear();
                    ControlledCreatures.Add(hitEvent.collider.gameObject);
                    //Debug.Log(hitEvent.collider.gameObject.name);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //mouseHoldTimer -= Time.deltaTime;
            //if (mouseHoldTimer <= 0)
            //{
                //mouseHoldTimer = 0;
                Physics.Raycast(camRay, out hitEvent);
                mouseHoldVector = hitEvent.point;
                drawControlledAreaPoints();
             
            //}
        }
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(camRay, out hitEvent);
            Debug.Log("resize");
            resizeControlledArea(mouseHoldVector, hitEvent.point);

        }
        if (Input.GetMouseButtonUp(0))
        {
            Physics.Raycast(camRay, out hitEvent);
            for (int i1 = 0; i1 < drawPoints.Count; i1++){

                Destroy(drawPoints[i1]);
            }
            drawPoints.Clear();

            bound1 = new Vector3(mouseHoldVector.x,0,hitEvent.point.z);
            bound2 = new Vector3(hitEvent.point.x, 0, mouseHoldVector.z);

            for (int i = 0; i <= allCreatures.Length; i++)
            {
                if (allCreatures[i].transform.position.x < bound2.x && allCreatures[i].transform.position.x > bound1.x && allCreatures[i].transform.position.z < bound2.z && allCreatures[i].transform.position.z > bound1.z)
                {
                    ControlledCreatures.Add(allCreatures[i]);
                }
            }
            for (int i = 0; i <= allCreatures.Length; i++)
            {
                if (allCreatures[i].transform.position.x < mouseHoldVector.x && allCreatures[i].transform.position.x > hitEvent.point.x && allCreatures[i].transform.position.z < mouseHoldVector.z && allCreatures[i].transform.position.z > hitEvent.point.z)
                {
                    ControlledCreatures.Add(allCreatures[i]);
                }
            }
            for (int i = 0; i <= allCreatures.Length; i++)
            {
                if (allCreatures[i].transform.position.x < bound1.x && allCreatures[i].transform.position.x > bound2.x && allCreatures[i].transform.position.z < bound1.z && allCreatures[i].transform.position.z > bound2.z)
                {
                    ControlledCreatures.Add(allCreatures[i]);
                }
            }
            for (int i = 0; i <= allCreatures.Length; i++)
            {
                if (allCreatures[i].transform.position.x < hitEvent.point.x && allCreatures[i].transform.position.x > mouseHoldVector.x && allCreatures[i].transform.position.z < hitEvent.point.z && allCreatures[i].transform.position.z > mouseHoldVector.z)
                {
                    ControlledCreatures.Add(allCreatures[i]);
                }
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

