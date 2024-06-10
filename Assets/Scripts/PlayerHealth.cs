using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health == 3){
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }else if (player.health == 2){
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }else if (player.health == 1){
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }else if (player.health == 0){
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
