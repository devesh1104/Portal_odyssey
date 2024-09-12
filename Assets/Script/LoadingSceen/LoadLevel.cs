using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    //public int sceneBuildIndex=1;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        SceneManager.LoadScene(4);
    }
 
}
