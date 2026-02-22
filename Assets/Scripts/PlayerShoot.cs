using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour
{
    //https://www.youtube.com/watch?v=dXgb6J46yjk

    public Transform FirePoint;
    private AudioSource audioSource;
    public AudioClip gunShot;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return; // don't shoot if paused

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("SHOOTING");
            audioSource.PlayOneShot(gunShot);
            Shooting();
        }

    }

    public void Shooting()
    {
        RaycastHit hit;

        Ray RaycastDirection = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0)); //Whole thing sends a ray from the center of camera, forward
        //Ray is literally an invisible line in one direction
        //ScreenPointToRay converts the 2D position calculated from scrren.width & height, and positions it in the 3D world

        if (Physics.Raycast(RaycastDirection, out hit, 100)) //(Physics.Raycast(#1, #2, #3)) //#1 = position of beginning of raycast, #2 = stores information of where the raycast hit, 3# = raycast distance
        {
            Debug.DrawRay(RaycastDirection.origin, RaycastDirection.direction * hit.distance, Color.yellow); //creates a visible line to see

            //              Where the line begins    its direction * distance from origin to successful raycast hit     color

            EnemyHit enemy = hit.transform.GetComponent<EnemyHit>();

            if (enemy != null)
            {
                enemy.damage(2);
            }
        }

    }
}
