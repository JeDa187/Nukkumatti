using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public string levelToLoad;

    public bool cleared; // T‰m‰ on true jos kentt‰ on p‰‰sty l‰pi. Muuten false. 

    // Start is called before the first frame update
    void Start()
    {
        // Katsotaan aina Map Scene avattessa, ett‰ onko GameManagerissa merkattu, ett‰ kyseinen taso on l‰p‰isty
        // Jos on l‰p‰isty, ajetaan Cleared funktio, joka tekee muutokset t‰h‰n objektiin,
        // eli n‰ytt‰‰ Stage Clear kyltin.
        if(GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true); // Koska rasti on olemassa, merkataan taso l‰pik‰ydyksi. 
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cleared(bool isClear)
    {

        if(isClear == true)
        {
            // Taso on p‰‰sty l‰pi. Asetetaan GameManagerissa oikea boolean arvo trueksi.
            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);

            // On m‰‰ritelty, ett‰ kentt‰ on p‰‰sty l‰pi. Laitetaan Stage Clear kyltti n‰kyviin
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true; 
            // Koska taso on p‰‰sty l‰pi. Deaktivoidaan LevelTriggerin Circlecollider. N‰in tasoon ei p‰‰se
            // palaamaan myˆhemmin.
            GetComponent<CircleCollider2D>().enabled = false; 


        }


    }
}
