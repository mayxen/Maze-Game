using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Player player;
    public int speedRotate;
    float mouseY;
    float mouseX;

    // Update is called once per frame
    void Update()
    {
        mouseY -= speedRotate * Input.GetAxis("Mouse Y");
        mouseX += speedRotate * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(Mathf.Clamp(mouseY, -80f, 80f), Mathf.Clamp(mouseX, -80f, 80f), 0.0f);
        if(player != null)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}
