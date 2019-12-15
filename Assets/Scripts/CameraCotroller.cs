using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCotroller : MonoBehaviour {
    public Camera m_OrthographicCamera;
    [HideInInspector]
    public GameObject player;       //Public variable to store a reference to the player game object
    public static CameraCotroller instance;

    public Vector3 offset;         //Private variable to store the offset distance between the player and camera
    float m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight;
    void _makeInstance()
    {
        if (instance == null) instance = this;
    }
    
    void start() {
        m_OrthographicCamera.orthographic = true;
        Debug.Log("run camera");
        m_OrthographicCamera.enabled = true;
        m_ViewPositionX = 0;
        m_ViewPositionY = 0;
        //This sets the Camera view rectangle to be smaller so you can compare the orthographic view of this Camera with the perspective view of the Main Camera
        m_ViewWidth = 0.4f;
        m_ViewHeight = 0.4f;
        m_OrthographicCamera.orthographicSize = 1.2f;
        m_OrthographicCamera.rect = new Rect(m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (player != null)
        {
            Debug.Log("da co player");
            transform.position = player.transform.position + offset;
        }
       
        Debug.Log("followwww");
    }
}
