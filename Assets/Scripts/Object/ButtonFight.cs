using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonFight : MonoBehaviour {
    public string id;
    void Awake () {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(() => OnlineUserController.instance.sendRq(id,Controller.instance.namePlayer));
    }
	
}
