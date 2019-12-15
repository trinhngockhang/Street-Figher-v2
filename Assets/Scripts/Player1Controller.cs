using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour {
    public BoxCollider boxCollider;
    private bool rightLook;
    Animator m_Animator;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

    }
    private void OnCollisionStay(Collision collision)
    {
    }
}
