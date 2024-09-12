using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToBossLevel1 : MonoBehaviour
{
     private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        SceneManager.LoadScene(17);
    }
}
