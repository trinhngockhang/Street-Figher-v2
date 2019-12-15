using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : MonoBehaviour {
    public delegate void OnMove(Vector2 vector2,int angle);
    public WachButton Left;
    public WachButton Right;
    public WachButton BackWard;
    public WachButton Forward;
    public WachButton Punch;
    public WachButton Kick;
    public WachButton Block;
    public Player player;
    Animator m_Animator;

    private Rigidbody myBody;
    public bool leftMove;
    public bool rightMove;
    public bool downMove;
    public bool upMove;
    public bool punch;
    public bool kick;
    public bool jump;
    public bool block;
    private bool lastLeft, lastRight, lastUp, lastDown, lastIdle;

    //bien i dung de xac dinh da an nut Block hay chua
    int i = 0;

    private float punchTimer = 0;
    private float kickTimer = 0;

    void Start()
    {
        // playerObject = new GameObject();
        ActionJoystick();

    }
    public void ActionJoystick()
    {
        Left.OnPress += OnPress;
        Right.OnPress += OnPress;
        BackWard.OnPress += OnPress;
        Forward.OnPress += OnPress;
        Punch.OnPress += OnPress;
        Kick.OnPress += OnPress;
        Block.OnPress += OnPress;
    }
    void OnPress(GameObject unit,bool state)
    {
        switch (unit.name)
        {
            case "Left":
                LeftMove(state);
            break;
            case "Right":
                RightMove(state);
            break;
            case "Down":
                DownMove(state);
            break;
            case "Up":
                UpMove(state);
            break;
        }
        if(unit.name == "Punch"){
          //  Debug.Log("da nhan nut dam");
            PunchAttack(state);
        }
        if (unit.name == "Kick")
        {
            //  Debug.Log("da nhan nut dam");
            KickAttack(state);
        }
        if (unit.name == "Block")
        {
            //  Debug.Log("da nhan nut dam");
            Blocking(state);
        }
    }

    private void LeftMove(bool state)
    {
        leftMove = state;
    }
    private void RightMove(bool state)
    {
        rightMove = state;
    }
    private void DownMove(bool state)
    {
        downMove = state;
    }
    private void UpMove(bool state)
    {
        upMove = state;
    }
    private void PunchAttack(bool state)
    {
        punch = state;
    }
    private void KickAttack(bool state)
    {
        kick = state;
    }
    private void Jumping(bool state)
    {
        jump = state;
    }
    private void Blocking(bool state)
    {
        block = state;
    }
    private void setAllMoveFalse(){
        lastLeft = false;
        lastRight = false;
        lastUp = false;
        lastDown = false;
        lastIdle = false;
    }

    public void Hello() {
        //player.kick();
    }

    private void Update()
    {
        if (leftMove || Input.GetKey("a"))
        {
            //   Debug.Log("chuan bi update LEft" + lastLeft);
            if (!lastLeft) {
                setAllMoveFalse();
                lastLeft = true;
                // di chuyen nhan vat
                //player.moveBack();
                // gui len server
                Controller.instance.ChangeVelocity("left");
            }
        }
        else if (rightMove || Input.GetKey("d"))
        {
            if (!lastRight) {
                setAllMoveFalse();
                lastRight = true;
                // di chuyen nhan vat
                //  player.moveFoward();
                // gui len server
                Controller.instance.ChangeVelocity("right");
            }
        }
        else if (downMove || Input.GetKey("s"))
        {
            if (!lastDown)
            {
                setAllMoveFalse();
                lastDown = true;
                // di chuyen nhan vat
                player.moveDown();
                // gui len server
                Controller.instance.moveDown();
            }
        }
        else if (upMove || Input.GetKey("w"))
        {
            if (!lastUp)
            {
                setAllMoveFalse();
                lastUp = true;
                Debug.Log("nhay len joy");
                // di chuyen nhan vat
                player.standUp();
                // gui len server
                // Controller.instance.moveUp();
            }
        }
        else
        {

            if (lastLeft || lastRight)
            {
                setAllMoveFalse();
                //Debug.Log("vua di chuyen xong");
                player.stopMove();
                int n = lastLeft ? 180 : 0;
                player.updatePositionToServer(n);
            }
            if (lastUp || lastDown) {
                setAllMoveFalse();
            }
        }

        if (punch || Input.GetKey("k")) {
            //Debug.Log("aaaaaaaaaaa");
            if ((punchTimer == 0) || ((Time.realtimeSinceStartup - punchTimer) > 0.6))
            {
                player.punch();
                punchTimer = Time.realtimeSinceStartup;
            }
            //player.punch();
        }

        if (kick || Input.GetKey("l"))
        {
            //Debug.Log("kick roi");
            if ((kickTimer == 0) || ((Time.realtimeSinceStartup - kickTimer) > 0.6))
            {
                player.kick();
                kickTimer = Time.realtimeSinceStartup;
            }
        }

        if (block)
        {
            player.block();
            if (player.state == 1)
            {
                i = 1;
                Debug.Log("1");
            }
            if (player.state == 2)
            {
                i = 2;
            }
        }
        else {
            //player.animationIdle();
            // Dung bien i: chi sau khi action Block thi moi tro ve idle
            if (i == 1)
            {
                Debug.Log("Ve trang thai ban dau");
                player.idle();
                i = 0;
            }
            if (i == 2)
            {
                player.moveDown();
                i = 0;
                Controller.instance.moveDown();
            }
        }
    }
}
