﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour {
    // am thanh
    public AudioClip hitAudio;
    public AudioClip getHitAudio;
    public AudioClip winAudio;
    private AudioSource audioSource;
    public Rigidbody myBody;
    // ten player 
    public string playerName;
    // vi tri
    public Vector2 position;
    // luong damge 1 hit
    int enemyDamge = 10;
    // id tu server 
    public string id;
    public int direct = 0; // director of tank,1234 up right down left
    // tap instance
    public static Player instance;
    // mau
    private float maxHealth = 100f;
    private float health = 100f;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public bool enoughTime = true;
    // check trong 1 attack
    private bool attacked = false;
    private Text nameUser;
    // dieu khien animation
    Animator m_Animator;
    // nhin sang ben nao
    private bool rightLook;
    // thanh mau
    private HealthBar HealthImage;
    // la player ben nao
    private bool firstPlayer;
    // 1 dung,2 ngoi,3 nhay
    public int state = 1;
    public bool collisionEnter = false;

    public float playerY;
    private Collision otherPlayerCollision;

    private bool collisionPlayer = false;

    public Player playerTest;

    public void test() {
        //Debug.Log("chay dc ne");
    }

    public void setEnemyDamge(int n){
        this.enemyDamge = n;
    }

    public void animationWin(){
        m_Animator.Play("player_win");
    }

    public void animationLose()
    {
        m_Animator.Play("player_lose");
    }

    public void setMyHealth(float n){
        this.maxHealth = n;
        this.health = n;
    }

    public void setName(bool first, Text textPlayer1, Text Player2, HealthBar HealthBar1, HealthBar HealthBar2)
    {
        //Debug.Log("la ng dau toen : " + first);
        //Debug.Log("ten la " + playerName);
        //firstPlayer = first;
        if (first)
        {
            //Debug.Log("da vao first la ng dau tien");
            nameUser = textPlayer1;
            HealthImage = HealthBar1;
        }
        else
        {
            //Debug.Log("da vao day deo phai ng dau ten");
            nameUser = Player2;
            HealthImage = HealthBar2;
        }
        nameUser.text = playerName;
        //Debug.Log("ten la " + playerName);
    }

    public float getHealth()
    {
        return health;
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        playerY = myBody.position.y;
        firstPlayer = true;
        m_Animator = this.GetComponent<Animator>();
        this.name = playerName;
       
        Physics.gravity = new Vector3(0, -25F, 0);
        _makeInstance();
    }

    void _makeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // check xem dang danh nhau hay ko
    public bool checkAttacking()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_H_punch")
           || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_M_punch")
           || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_Kick")
           || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_SitKick"))
        {
            return true;
        }
        return false;
    }
    // sang trai
    public void moveBack() {
        velocityBack();
        if (state == 1) {
            m_Animator.ResetTrigger("player_idle");
            m_Animator.SetTrigger("player_move");
        }
    }
    // thay doi van toc de sang trai
    public void velocityBack() {
        //Debug.Log("chay sang ben trai");
        myBody.velocity = new Vector2(-6f, 0);
        this.transform.eulerAngles = new Vector3(0, 180, 0);
        rightLook = false;
    }
    // thay doi van toc de sang phai
    public void velocityFoward() {
        myBody.velocity = new Vector2(6f, 0);
        this.transform.eulerAngles = new Vector3(0, 0, 0);
        rightLook = true;
    }
    // sang phai
    public void moveFoward() {
        velocityFoward();
        if (state == 1)
        {
            m_Animator.ResetTrigger("player_idle");
            m_Animator.SetTrigger("player_move");
        }
    }
    // ngoi xuong
    public void moveDown() {
        Debug.Log("ngoi xuong");
        m_Animator.SetTrigger("player_sit");
        state = 2;
    }

    public void animationStandUp(){
        m_Animator.ResetTrigger("player_sit");
        m_Animator.SetTrigger("player_idle");
        state = 1;
    }
    // dung len
    public void standUp()
    {
        if (state == 2)
        {
            Debug.Log("gui len sv yeu cau dung day");
            Controller.instance.moveUp();
        }
        else if(state == 1){
            if (collisionEnter == true && !this.checkAttacking())
            {
                Controller.instance.jump();
                state = 3;
                collisionEnter = false;
                Debug.Log("gui len sv yeu cau nhay len!!!");

            }
        }
    }

    public void updatePositionToServer(int n){
        Controller.instance.OnCommandMove(this.transform.position, n);
    }

    public void stopMove(){
        if (!this.checkAttacking())
        {
            //Debug.Log("set status");
            if(state == 1)
            m_Animator.SetTrigger("player_idle");
            else if(state == 2)
            m_Animator.SetTrigger("player_sit");
        }
        myBody.velocity = new Vector2(0, 0);
    }

    public void punch(){
        if (!this.checkAttacking())
        {
            // this.animationPunch();
            // gui len server
            Controller.instance.h_punch();
        }
    }
    public void kick()
    {
        if (!this.checkAttacking())
        {
            //this.animationKick();
            //gui len server
            Controller.instance.kick();
        }
    }

    public void jump()
    {
        Debug.Log("Nhay len ne");
        state = 3;
        collisionEnter = false;
        myBody.AddForce(new Vector3(0, 6, 0) * 100);
        // myBody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        // myBody.AddForce(Vector3.up * 90 * Time.deltaTime);
        this.animationStandup();

    }

    public void block()
    {
        if (!this.checkAttacking())
        {
            //this.animationBlock();
            // gui len server
            Controller.instance.block();
        }
    }

    public void idle()
    {
        if (!this.checkAttacking())
        {
            //this.animationIdle();
            // gui len server
            Controller.instance.idle();
        }
    }

    public void animationPunch(){
        if (state == 1 || state == 3)
            m_Animator.Play("player_H_punch");
        else if (state == 2)
            m_Animator.Play("player_M_punch");
        audioSource.PlayOneShot(hitAudio);
    }
    public void animationKick()
    {
        if (state == 1)
        {
            m_Animator.Play("player_Kick");
        }
        else if (state == 2)
        {
            m_Animator.Play("player_SitKick");
        }
        audioSource.PlayOneShot(hitAudio);
        //m_Animator.Play("player_Kick");
    }
    public void animationJump()
    {
        m_Animator.SetTrigger("player_jumping");
    }
    public void animationBlock()
    {
        if (state == 1)
        {
            m_Animator.Play("player_Block");
        }
        else if (state == 2)
        {
            m_Animator.Play("player_SitBlock");
        }
    }

    public void animationIdle()
    {
        m_Animator.SetTrigger("player_idle");
    }

    public void animationStandup()
    {
        Debug.Log("nhay len animatino");
        if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_idle")){
            m_Animator.ResetTrigger("player_idle");
            m_Animator.SetTrigger("player_jumping");
        }
        if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_move")){
            m_Animator.ResetTrigger("player_move");
            m_Animator.SetTrigger("player_jumping");
        }

    }

    public void beFirebyNormalBulletEnemy()
    {      
    }

    public void FireNormalBullet()
    {
       
    }
    public void hit(){
        if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_hit"))
        {
            if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_Block")){
                audioSource.PlayOneShot(getHitAudio);
                if (this.health > 0)
                {
                    this.health = Mathf.Max(0, this.health - enemyDamge/2);
                }
                HealthImage.transform.localScale = new Vector2(health / maxHealth, 1);
            }
            else{
                m_Animator.Play("player_hit");
                audioSource.PlayOneShot(getHitAudio);
                if (this.health > 0)
                {
                    this.health = Mathf.Max(0, this.health - enemyDamge);
                }
                HealthImage.transform.localScale = new Vector2(health / maxHealth, 1);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if(!collisionEnter){
                Debug.Log("Cham dat ne");
                collisionEnter = true;
                if (state == 3) state = 1;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player"){
            collisionPlayer = true;
            otherPlayerCollision = collision;
            myBody.velocity = new Vector2(0, 0);
        }
        else if(collision.collider.tag == "Ground"){
            Debug.Log("Cham dat ne");
            collisionEnter = true;
            state = 1;
        }
        // myBody.velocity = new Vector2(0, 0);
        //Debug.Log("bat dau cham");
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Player"){
            collisionPlayer = false;
        }
        else if (collision.collider.tag == "Ground")
        {
            state = 3;
            collisionEnter = false;
        }
        // myBody.velocity = new Vector2(0, 0);
        //Debug.Log("het va cham");
    }

    public void playWinAudio(){
        // audioSource.PlayOneShot(winAudio);
    }
    private void playerColl(Collision collision){
        float positionPlayerX = this.transform.position.x;
        float positionEnemyX = collision.transform.position.x;
        //Debug.Log("Danh trung r,dm");
        //Debug.Log(positionEnemyX + " " + positionPlayerX + " " + attacked);
        // neu o ben phai va dang nhin sang phai
        if ((positionEnemyX > positionPlayerX && rightLook) || (positionEnemyX < positionPlayerX && !rightLook))
        {
            Player player2 = collision.gameObject.GetComponent<Player>();
            
            if ((m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_H_punch")
              || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_M_punch") 
              || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_Kick")
              || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_SitKick"))
              && !attacked) 
            {
                //playerTest = player2;
                //Debug.Log("Client dang nhan Player: " + playerTest);
                //Debug.Log("Minh la player: " + Controller.instance.playerCom);
                if (player2 == Controller.instance.otherPlayCom)
                {
                    Controller.instance.attack();
                }
                //Debug.Log("Thang Player danh la: " + player2);
                //player2.hit();
                attacked = true;
            }
        }
    }

/*    private void onCollisionStay(Collision collision)
    {
        float positionPlayerX = this.transform.position.x;
        float positionEnemyX = collision.transform.position.x;
        //Debug.Log(positionEnemyX + " " + positionPlayerX + " " + attacked);
        // neu o ben phai va dang nhin sang phai    
        if ((positionEnemyX > positionPlayerX && rightLook) || (positionEnemyX < positionPlayerX && !rightLook))
        {
            Player player2 = collision.gameObject.GetComponent<Player>();
            if ((m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_H_punch")
              || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_M_punch")
                 || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("player_kick") )
              && !attacked)
            {
                Debug.Log("dam trung r 11");
                player2.hit();
                attacked = true;
            }
        }
    }
*/
    private void Update()
    {   
        //Debug.Log("velocity:" + myBody.velocity);
        if (!checkAttacking()) attacked = false;
    }
    private void FixedUpdate()
    {
        if(collisionPlayer){
            playerColl(otherPlayerCollision);
        }
    }
}
