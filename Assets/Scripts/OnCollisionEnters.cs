using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnters : MonoBehaviour
{
    Vector3 size;
    private GameObject colider;
    private bool One_time = true;
    private Rigidbody enemyRigidbody;
    private Vector3 awayFromPlayer;
    private float powerupStrength = 15.0f;

    public Texture[] Texture_of_killer;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int index = UnityEngine.Random.Range(0, Texture_of_killer.Length);
        if(colider != null)
        {
            if(One_time)
            {
                if(colider.transform.position.y < -1.0f)
                {
                    size = transform.localScale;
                    size.x += 0.4f;
                    size.y += 0.4f;
                    size.z += 0.4f;
                    transform.localScale = size;
                    enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
                    GetComponent<Renderer>().material.mainTexture = Texture_of_killer[index];
                    One_time = false;
                }
            }
        } 

         if(transform.position.y < -1.5f)
        {
            Destroy(gameObject);
        }  
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player") )
        {

            enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            
            colider = collision.gameObject;
            
        }
    }
}
