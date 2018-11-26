using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    [SerializeField]
    private Text playerName = null;

    public void setText(string newText)
    {
        playerName.text = newText;
    }
}
