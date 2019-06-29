using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    
    Rigidbody rb;
    public void SetLocation(MazeCell cell)
    {
        transform.localPosition = new Vector3(cell.transform.localPosition.x,0.5f, cell.transform.localPosition.z);
        rb = GetComponent<Rigidbody>();
    }   
    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO tengo que hacer la rotación normal xD 
        float moveHorizontal = Input.GetAxis("Horizontal") * (speed ) * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * (speed) * Time.deltaTime;
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.localPosition += transform.TransformDirection(movement);  
    }
}
