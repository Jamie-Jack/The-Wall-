using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.NewVisionGamesStudio.TheWall
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void StartButton()
        {
            SceneManager.LoadScene(0);
        }
        public void OptionsButton()
        {
            SceneManager.LoadScene(2);
        }
    }
}