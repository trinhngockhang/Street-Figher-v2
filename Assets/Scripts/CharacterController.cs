using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public SocketIOComponent socket;
    // Bar
    // Ken
    public HealthBar KenHealthBar;
    public HealthBar KenDamgeBar;
    // Barog
    public HealthBar BarogHealthBar;
    public HealthBar BarogDamgeBar;
    // Blanka
    public HealthBar BlankaHealthBar;
    public HealthBar BlankaDamgeBar;
    // Guile
    public HealthBar GuileHealthBar;
    public HealthBar GuileDamgeBar;
    // Ryu
    public HealthBar RyuHealthBar;
    public HealthBar RyuDamgeBar;
    // Chun-Li
    public HealthBar ChunliHealthBar;
    public HealthBar ChunliDamgeBar;
    // T
    public HealthBar THealthBar;
    public HealthBar TDamgeBar;
    // Character
    public Player KenPlayer;
    public Player GuilePlayer;
    public Player BarogPlayer;
    public Player BlankaPlayer;
    public Player RyuPlayer;
    public Player ChunLiPlayer;
    public Player TPlayer;

    private float maxHealth = 300;
    private float maxDamge = 70;

    public void clickKen(){
        selectCharacter("1");
        Debug.Log("Chon Ken");
    }
    public void clickBarog()
    {
        selectCharacter("2");
        Debug.Log("Chon barog" + Controller.instance.idPlayer2);
    }
    public void clickBlanka()
    {
        selectCharacter("3");
        Debug.Log("Chon blanka");
    }
    public void clickGuile()
    {
        selectCharacter("4");
        Debug.Log("Chon guile");
    }
    public void clickRyu()
    {
        selectCharacter("5");
        Debug.Log("Chon ryu");
    }
    public void clickChunLi()
    {
        selectCharacter("6");
        Debug.Log("Chon Chun-Li");
    }
    public void clickT()
    {
        selectCharacter("7");
        Debug.Log("Chon T");
    }
    public void selectCharacter(string n){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["gaming"] = "2";
        data["character"] = n;
        Debug.Log("ID player 2: " + Controller.instance.idPlayer2);
        data["id"] = Controller.instance.idPlayer2;
        data["id"] = data["id"].Remove(0, 1);
        data["id"] = data["id"].Remove(data["id"].Length - 1, 1);
        socket.Emit("WAITING", new JSONObject(data));
        Controller.instance.myCharacter = n;
        Controller.instance.moveToWaiting();
    }
    private void Awake()
    {
        float KenHealth = KenPlayer.GetComponent<Character>().Health;
        KenHealthBar.transform.localScale = new Vector2(KenHealth / maxHealth, 1);
        float KenDamge = KenPlayer.GetComponent<Character>().Damge;
        KenDamgeBar.transform.localScale = new Vector2(KenDamge / maxDamge, 1);
        // 
        float BarogHealth = BarogPlayer.GetComponent<Character>().Health;
        BarogHealthBar.transform.localScale = new Vector2(BarogHealth / maxHealth, 1);
        float BarogDame = BarogPlayer.GetComponent<Character>().Damge;
        BarogDamgeBar.transform.localScale = new Vector2(BarogDame / maxDamge, 1);
        //
        float BlankaHealth = BlankaPlayer.GetComponent<Character>().Health;
        BlankaHealthBar.transform.localScale = new Vector2(BlankaHealth / maxHealth, 1);
        float BlankaDamge = BlankaPlayer.GetComponent<Character>().Damge;
        BlankaDamgeBar.transform.localScale = new Vector2(BlankaDamge / maxDamge, 1);
        //
        float GuileHealth = GuilePlayer.GetComponent<Character>().Health;
        GuileHealthBar.transform.localScale = new Vector2(GuileHealth / maxHealth, 1);
        float GuileDamge = GuilePlayer.GetComponent<Character>().Damge;
        GuileDamgeBar.transform.localScale = new Vector2(GuileDamge / maxDamge, 1);
        //
        float ChunliHealth = ChunLiPlayer.GetComponent<Character>().Health;
        ChunliHealthBar.transform.localScale = new Vector2(ChunliHealth / maxHealth, 1);
        float ChunliDamge = ChunLiPlayer.GetComponent<Character>().Damge;
        ChunliDamgeBar.transform.localScale = new Vector2(ChunliDamge / maxDamge, 1);
        //
        float RyuHealth = RyuPlayer.GetComponent<Character>().Health;
        RyuHealthBar.transform.localScale = new Vector2(RyuHealth / maxHealth, 1);
        float RyuDamge = RyuPlayer.GetComponent<Character>().Damge;
        RyuDamgeBar.transform.localScale = new Vector2(RyuDamge / maxDamge, 1);
        //
        float THealth = TPlayer.GetComponent<Character>().Health;
        THealthBar.transform.localScale = new Vector2(THealth / maxHealth, 1);
        float TDamge = TPlayer.GetComponent<Character>().Damge;
        TDamgeBar.transform.localScale = new Vector2(TDamge / maxDamge, 1);
    }
}
