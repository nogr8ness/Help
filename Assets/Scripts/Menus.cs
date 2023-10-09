using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject col1, col2;

    private void Awake()
    {
        if(col1 != null)
        {
            if (PlayerPrefs.GetInt("collisions") == 1)
            {
                col2.SetActive(true);
                col1.SetActive(false);
            }

            else
            {
                col1.SetActive(true);
                col2.SetActive(false);
            }
        }
    }

    public void SetCollisions(int value)
    {
        PlayerPrefs.SetInt("collisions", value);
    }

    public void LoadSinglePlayer()
    {
        PlayerPrefs.SetInt("multiplayer", 0);
        SceneManager.LoadScene("Help");
    }
    
    public void LoadMultiPlayer()
    {
        PlayerPrefs.SetInt("multiplayer", 1);
        SceneManager.LoadScene("Help");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
