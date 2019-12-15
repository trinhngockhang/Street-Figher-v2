using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
public class WinController : MonoBehaviour {
    public SocketIOComponent socket;
    public WinPanel winpanel;
    public Text textWin;
    public Button Back;
    public GameObject Map;
    public BackGround bg;
    public InfoPanel info;
    public HealthBar healthbar1;
    public HealthBar healthbar2;
    public JoyStickController joyStick;
    void Start () {
        Back.onClick.AddListener(() => backWaiting());
    }
	
    void backWaiting()
    {
        healthbar1.transform.localScale = new Vector2(1, 1) ;
        healthbar2.transform.localScale = new Vector2(1, 1);
        Controller.instance.gaming = false;
        info.gameObject.SetActive(false);
        DestroyImmediate(Controller.instance.otherPlayCom.gameObject);
        DestroyImmediate(Controller.instance.playerCom.gameObject);
        Map.gameObject.SetActive(false);
        joyStick.gameObject.SetActive(false);
        bg.gameObject.SetActive(true);
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["gaming"] = "false";
        socket.Emit("CHANGESTATUS", new JSONObject(data));
        winpanel.gameObject.SetActive(false);
        //Debug.Log("Delete all" + Time.time);
    }

	void Update () {
        if (Controller.instance.playerCom != null && Controller.instance.otherPlayCom != null)
        {
            if ((Controller.instance.playerCom.getHealth() <= 0))
            {
                Controller.instance.otherPlayCom.playWinAudio();
                //Debug.Log("still" + Time.time);
                winpanel.gameObject.SetActive(true);
                textWin.text = "You Lose!";
            }
            else if (Controller.instance.otherPlayCom.getHealth() <= 0)
            {
                //Debug.Log("still" + Time.time);
                Controller.instance.playerCom.playWinAudio();
                winpanel.gameObject.SetActive(true);
                textWin.text = "You Win!";
            }
        }
        
	}
}
