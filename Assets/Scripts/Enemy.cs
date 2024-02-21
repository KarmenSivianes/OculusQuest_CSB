using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject stone;
    public Transform shootPoint;
    public float shootForce;
    bool playerDetected;
    public float cadency;
    public int health;
    public Slider sliderHealth;

    private void Start()
    {
        sliderHealth.value = sliderHealth.maxValue = health;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Attack");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine("Attack");
        }
    }

    public IEnumerator Attack()
    {
        while (true)
        {
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            playerPosition.y = transform.position.y;
            transform.LookAt(playerPosition);
            yield return new WaitForSeconds(0.2f);
            GetComponent<Animator>().Play("Attack");
            yield return new WaitForSeconds(cadency);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            health--;
            sliderHealth.value = health;
            if (health <= 0)
            {
                StopAllCoroutines();
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<Animator>().Play("Dying");
                this.enabled = false;
            }
        }
    }

    public void Shoot()
    {
        Instantiate(stone, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce);
    }
}
