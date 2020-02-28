using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.NewVisionGamesStudio.TheWall
{
    public class Respawn : MonoBehaviour
    {
        public GameObject Player;
        public GameObject RespawnCanvas;
        public GameObject respawnPosition;
        public Text Countdown;
        public int CD_Int;

        public void RespawnButton()
        {

            Countdown.text = "5";



            InvokeRepeating("RespawnCountdown", 1, 1);
        }

        void RespawnCountdown()
        {
            CD_Int--;
            Countdown.text = CD_Int.ToString();
            if (CD_Int <= -1)
            {
                Instantiate(Player, respawnPosition.transform.position, respawnPosition.transform.rotation);
                RespawnCanvas.GetComponent<Canvas>().enabled = false;
                CancelInvoke("RespawnCountdown");
                CD_Int = 5;
                Countdown.text = "";
            }
        }
    }
}