using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    public Slider sliderHealth;

    private void Start()
    {
        sliderHealth.value = sliderHealth.maxValue = health;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            Destroy(collision.gameObject);
            health--;
            sliderHealth.value = health;
            /*if (health <= 0)
            {
                StopAllCoroutines();
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<Animator>().Play("Dying");
                this.enabled = false;
            }*/
        }
    }
}
