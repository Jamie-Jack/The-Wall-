using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.NewVisionGamesStudio.TheWall
{
    public class Coin_Rotate : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 3, 0);
        }
    }
}