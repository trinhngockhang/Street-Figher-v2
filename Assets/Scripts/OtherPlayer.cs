using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Text.RegularExpressions;
public class OtherPlayer : MonoBehaviour {

    public SocketIOComponent socket;
    [HideInInspector]
    public GameObject playerEnemy;

    private void Start()
    {
        socket.On("MOVE", onUserMove);
    }
    void onUserMove(SocketIOEvent obj)
    {
        //Debug.Log("GEt the message server: " + obj + "user connected");
        //Debug.Log("Position enemy: " + JsontoVector2(JsonToString(obj.data.GetField("position").ToString(), "\"")));
        ////   GameObject player = GameObject.Find(JsonToString(obj.data.GetField("name").ToString(), "\" ")) as GameObject;
        ////player.transform.position = JsontoVector2(JsonToString(obj.data.GetField("position").ToString(), "\""));
        //playerEnemy.transform.position = JsontoVector2(JsonToString(obj.data.GetField("position").ToString(), "\""));
        //Debug.Log("name move: " + obj.data.GetField("name").ToString());
    }
 
    Vector2 JsontoVector2(string target)
    {
        Vector2 newVector;
        string[] newString = Regex.Split(target, ",");
        newVector = new Vector2(float.Parse(newString[0]), float.Parse(newString[1]));
        return newVector;
    }


    string JsonToString(string target, string s)
    {
        string[] newString = Regex.Split(target, s);
        return newString[1];
    }
}
