using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameStats : MonoBehaviour {

    public int RoundWins = 0;
    public int Kills = 0;
    public int Deaths = 0;

    public int Gold = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
