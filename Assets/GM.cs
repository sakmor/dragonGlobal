using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : MonoBehaviour
{
    public List<player> allPlayer = new List<player>();

    public void Login(player player)
    {
        allPlayer.Add(player);
        player.RpcSetPlayer(allPlayer.Count);
    }
}