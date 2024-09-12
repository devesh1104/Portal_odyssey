using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public string[] lines;
    public TextMeshProUGUI textComponent;
    public float textspeed;
    private int index;
    void Start()
    {
        textComponent.text=string.Empty;
        StartChat();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text==lines[index])
            {
                nextline();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text=lines[index];            }
        }
    }
    void StartChat()
    {
        index=0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text+=c;
            yield return new WaitForSeconds(textspeed);
        }
    }
    void nextline()
    {
        if(index<lines.Length-1)
        {
            index++;
            textComponent.text=string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            gameObject.SetActive(false);

        }
    }
    
}
