using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMelee : MonoBehaviour
{
    

    
    private AudioSource audioSource;
    public AudioClip meleeSlash;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return; // don't melee if paused

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("MeleeHit");
            audioSource.PlayOneShot(meleeSlash);
            Melee();
        }

    }

    public void Melee()
    {
        RaycastHit hit;

        Ray RaycastDirection = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0)); //Whole thing sends a ray from the center of camera, forward
        //Ray is literally an invisible line in one direction
        //ScreenPointToRay converts the 2D position calculated from screen.width & height, and positions it in the 3D world

        if (Physics.Raycast(RaycastDirection, out hit, 10)) //(Physics.Raycast(#1, #2, #3)) //#1 = position of beginning of raycast, #2 = stores information of where the raycast hit, 3# = raycast distance
        {
            Debug.DrawRay(RaycastDirection.origin, RaycastDirection.direction * hit.distance, Color.yellow); //creates a visible line to see

            //              Where the line begins    its direction * distance from origin to successful raycast hit     color

            EnemyHit enemy = hit.transform.GetComponent<EnemyHit>();

            if (enemy != null)
            {
                enemy.damage(4);
            }
        }

    }
}
