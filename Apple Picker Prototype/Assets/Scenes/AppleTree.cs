using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // Prefab for instantiating apples
    public GameObject   applePrefab;

    // Speed at which the AppleTree moves
    public float        speed = 1f;

    // How fast the speed of the AppleTree increases
    public float        accel = 0.5f;

    // Limit to how fast the tree can accelerate
    public float        accelLimit = 10f;

    // Seconds before next accel increment
    public float        secBeforeAccelIncrease = 5f;

    // Distance where AppleTree turns around
    public float        leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float        chanceToChangeDirections = 0.1f;

    // Rate at which Apples will be instantiated
    public float        secondsBetweenAppleDrops = 1f;

    float timer = 0.0f;

    void Start () {

        // Dropping apples every second
        Invoke( "DropApple", 2f );

    }//Start


    void DropApple() {

        timer += Time.deltaTime;
        float seconds = timer % 60;

        GameObject apple = Instantiate<GameObject>( applePrefab );
        apple.transform.position = transform.position;
        Invoke( "DropApple", secondsBetweenAppleDrops );

        if (seconds % secBeforeAccelIncrease == 0) {
            secondsBetweenAppleDrops -= accel;
        }

    }//DropApple


    void Update () {

        timer += Time.deltaTime;
        float seconds = timer % 60;

        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if ( pos.x < -leftAndRightEdge ) {
            //Move right
            if (accel < accelLimit) {
                speed = Mathf.Abs(speed) + accel;
            }
            else {
                speed = Mathf.Abs(speed);
            }
        } 
        else if ( pos.x > leftAndRightEdge ) {
            //Move left
            if (accel < accelLimit) {
                speed = -Mathf.Abs(speed) - accel;
            }
            else {
                speed = -Mathf.Abs(speed);
            }
        }

        if (seconds % secBeforeAccelIncrease == 0){
            accel += accel;
        }

    }//Update


    void FixedUpdate() {

        // Changing Direction Randomly is now time-based because of FixedUpdate()
        if ( Random.value < chanceToChangeDirections ) {
            speed *= -1; // Change direction
        }

    }//FixedUpdate


}
