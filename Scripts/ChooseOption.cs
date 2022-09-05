using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseOption : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject winPanel;
    public GameObject failPanel;
    public GameObject instructionPanel;
    public GameObject jumpButton;
    public Animator characterAnimator;
    
    public GameObject livingButton;
    public GameObject nonLivingButton;

    public Text optionText;
    public Text totalText;
    public Text collectedText;

    public List<GameObject> livingObjects;
    public List<GameObject> nonLivingObjects;
    public List<GameObject> currentObjectList;

    public int totalObjectToSapwn;
    public int objectSpawnedCount;

    protected string optionSelected;
    protected string wrongOption;
    protected bool isGameStarted;

    private void Start()
    {
        instructionPanel.SetActive(true);
    }
    public void OnLivingClick()
    {
        optionSelected = "Living";  
        wrongOption = "NonLiving";
        currentObjectList = livingObjects;
        StartingOptions();
    }

    public void OnNonlivingClick()
    {
        optionSelected = "NonLiving";
        wrongOption = "Living";
        currentObjectList = nonLivingObjects;
        StartingOptions();
    }

    void StartingOptions()
    {
        optionText.text = optionSelected + " : ";
        totalText.text = "/ " +currentObjectList.Count.ToString() ;
        optionPanel.SetActive(false);
        GetComponent<PlayerMovementController>().enabled = true;
        isGameStarted = true;
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void OnInstructionClick()
    {
        instructionPanel.SetActive(true);
    }

    public void OnInstructionCrossClick()
    {
        instructionPanel.SetActive(false);

    }
}

