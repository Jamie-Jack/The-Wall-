using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.NewVisionGamesStudio.TheWall
{
    public class EnemyCount : MonoBehaviour
    {
        public Text enemyCount;
        // Start is called before the first frame update
        void changeText()
        {
            enemyCount.text = "Enemy Count: " + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            changeText();// change to an enemy spawn and despawn.
        }
    }
}
