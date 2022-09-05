using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ChooseOption
{
    [SerializeField] AudioSource audios;
    int score;

    private void Start()
    {
        collectedText.text = score.ToString();
    }
    private void Update()
    {
        if(isGameStarted && currentObjectList.Count<=0)
        {
            winPanel.SetActive(true);
            GetComponent<PlayerMovementController>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ObjectTag>() != null)
        {
            if(other.GetComponent<ObjectTag>().objTag == optionSelected)
            {
                score++;
                collectedText.text = score.ToString();
                audios.Play();
                currentObjectList.Remove(other.gameObject);
                other.gameObject.SetActive(false);
                
            }
            else if (other.GetComponent<ObjectTag>().objTag == wrongOption)
            {
                LevelFailed();
            }
           
        }
        if (other.gameObject.name == "River")
        {
            LevelFailed();
        }
    }


    private void LevelFailed()
    {
        characterAnimator.SetBool("Idle", true);
        failPanel.SetActive(true);
        jumpButton.SetActive(false);
        GetComponent<PlayerMovementController>().enabled = false;
    }

} 
