using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.NewVisionGamesStudio.TheWall
{
    public class PlayerVariables : MonoBehaviour
    {

        public Text coinCount;
        public int coinInt;

  
        private void Start()
        {
            UpdateCoins(0);
          
        }

        public void UpdateCoins(int coinUpdateAmount)
        {
            coinInt += coinUpdateAmount;
            coinCount.text = " Coins : " + coinInt;
        }

    }
}
