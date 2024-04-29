using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static Collectable Instance;
    public Image[] collectableSprites; // Lista de los sprites para las balas
    public GameObject[] descriptionTexts; // Array de GameObjects para los textos de descripci�n
    private int max_collectibles = 8;
    private int lastCollectedIndex = -1; // �ndice del �ltimo coleccionable obtenido

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitializeCollectSprites();
    }

    void InitializeCollectSprites()
    {
        collectableSprites = new Image[max_collectibles];
        for (int i = 0; i < max_collectibles; i++)
        {
            collectableSprites[i] = GameObject.Find("Collectible" + (i + 1)).GetComponent<Image>();
            // Desactivar los sprites al inicio
            collectableSprites[i].gameObject.SetActive(false);
        }
    }

    public void OnCollectibleTriggered(GameObject collectible)
    {
        int collectibleIndex = int.Parse(collectible.name.Substring(collectible.name.Length - 1)) - 1;
        if (collectibleIndex >= 0 && collectibleIndex < collectableSprites.Length)
        {
            lastCollectedIndex = collectibleIndex;
            collectableSprites[collectibleIndex].gameObject.SetActive(true);
        }
    }


    // M�todo llamado cuando se hace clic en un coleccionable en el men� de pausa
    public void OnCollectibleClicked(int buttonIndex)
    {
        // Desactivar todos los textos de descripci�n
        foreach (GameObject text in descriptionTexts)
        {
            text.SetActive(false);
        }

        // Activar el texto de descripci�n correspondiente al bot�n que se ha hecho clic
        descriptionTexts[buttonIndex].SetActive(true);
    }
}

