using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoLevel3 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        SceneManager.LoadScene(13);
    }
}
