using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehavior : MonoBehaviour
{
    int hp = 10;
    GameObject player;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if(hp>0) { 
        //transform.LookAt(player.transform.position);
        //Vector3 playerDirection = transform.position - player.transform.position;

        //transform.Translate(Vector3.forward * Time.deltaTime);
        agent.destination= player.transform.position;}
        else
        {
            agent.isStopped= true;
        }
        Vector3 raysource = transform.position + Vector3.up *1.8f;
        Vector3 rayDest = player.transform.position + Vector3.up * 1.8f;
        RaycastHit hit;
        if(Physics.Raycast(raysource,rayDest,out hit, 5f))
        {
            if (hit.transform.CompareTag("Player"))
            {
              UnityEngine.Debug.Log("WIdze");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            hp--;
            if (hp <= 0)
            {
                transform.Translate(Vector3.up);
                transform.Rotate(Vector3.right * -90);
                GetComponent<BoxCollider>().enabled = false;
                Destroy(transform.gameObject, 1);
            }
            
        }
    }
}