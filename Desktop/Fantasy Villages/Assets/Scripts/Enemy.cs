using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float Ehealth;
    [SerializeField] private float speed;
    private Vector3 movePoint;
    public float HP;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.0f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        movePoint = GameObject.Find("TownCenter").transform.position;
        if (HP <= 0)
        {
            GameObject hud = GameObject.Find("HUD");
            HUD hud_script = hud.GetComponent<HUD>();
            HP = 0;
            Destroy(this.gameObject);
            hud_script.killAmount++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Creature" || other.tag == "Building")
        {
            CreatureBehavior creatureStats = other.GetComponent<CreatureBehavior>();
            PlayersBuilding buildingStats = other.GetComponent<PlayersBuilding>();
            creatureStats.health -= 10;
            buildingStats.BHealth -= 10;
        }
    }

    void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, movePoint, speed * Time.deltaTime);
    }
}
