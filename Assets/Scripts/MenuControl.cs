using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public void LoadMap()
    {
        // Kun klikataan Play-apiniketta menussa -> Siirrytään Map sceneen. 
        SceneManager.LoadScene("Map");

    }


    public void Save()
    {
        // Tämä ajetaan menusta, kun painetaan Save painiketta
        // -> Kutsutaan GameManagerin Save funktiota.
        GameManager.manager.Save();

    }

    public void Load()
    {
        // Sama kuin yllä, mutta Load
        GameManager.manager.Load();


    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
