using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class Coin_Collct : MonoBehaviour
    {
        public Text coinCount;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
                EventSystem.current.GetComponent<PlayerVariables>().UpdateCoins(1);
                

            }
        }

    }
}
