﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehavior : MonoBehaviour
{
    private bool playerVisible = false;
    private bool playerHearable = false;
    public float hearRange = 5f;
    public float sightRange = 15f;
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
        //sprawdz czy "widzimy" gracza
        //jeœli twoje zombiaki maj¹wspó³rzêdn¹ y = 0 to musisz wzi¹æ poprawkê na wysokoœæ wzroku
        Vector3 raySource = transform.position + Vector3.up * 0.8f;
        Vector3 rayDirection = player.transform.position - transform.position;
        UnityEngine.Debug.DrawRay(raySource, rayDirection);
        //Debug.DrawRay(raySource, rayDirection);
        //deklarujemy zmienn¹ na to w co trafi raycast
        RaycastHit hit;
        if (Physics.Raycast(raySource, rayDirection, out hit, sightRange))
        {
            UnityEngine.Debug.Log(hit.transform.gameObject.name.ToString());
            //uruchomi siê wtedy i tylko wtedy jeœli raycast cokolwiek trafi
            if (hit.transform.CompareTag("Player"))
                playerVisible = true;
            else
                playerVisible = false;
        }
        //sprawdzamy czy słyszy
        Collider[] heardObjects = Physics.OverlapSphere(transform.position, hearRange);
        //sprawdz wszystkie kolajdery i jesli najdziesz gracza ustaw flage
        playerHearable = false;
        foreach(Collider collider in heardObjects)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                playerHearable = true;
            }
        }

        //jeœli widzi gracza to idzie
        agent.isStopped = !playerVisible && !playerHearable;
        if (hp > 0)
        {
            //transform.LookAt(player.transform.position);
            //Vector3 playerDirection = transform.position - player.transform.position;

            //transform.Translate(Vector3.forward * Time.deltaTime);

            agent.destination = player.transform.position;
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
              Die();
            }
            
        }
    }
    private void Die()
    {
        agent.enabled = false;
        //transform.Translate(Vector3.up);
        transform.Rotate(transform.right * -90);
        GetComponent<BoxCollider>().enabled = false;
        Destroy(transform.gameObject, 1);
    }
}