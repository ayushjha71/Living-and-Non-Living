using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockManger : MonoBehaviour
{
    [SerializeField] public GameObject startPanel;
    [SerializeField] public GameObject infoPanel;
    [SerializeField] public GameObject levelEndPanel;
    [SerializeField] public GameObject failPanel;

    bool canLockMouse = true;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canLockMouse = false;
        }

        if (startPanel.activeInHierarchy || infoPanel.activeInHierarchy || levelEndPanel.activeInHierarchy || failPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
             if(!canLockMouse)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
                
        }
    }

    IEnumerator CanLockMouse()
    {
        yield return new WaitForSeconds(10f);
        canLockMouse = true;
    }

}
