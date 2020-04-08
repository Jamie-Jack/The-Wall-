using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class ReturntoMainMenu : MonoBehaviour
    {

        public void OptionsButton()
        {
            SceneManager.LoadScene(0);
        }
    }
}
