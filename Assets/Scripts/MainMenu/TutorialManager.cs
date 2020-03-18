using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;

    [SerializeField] private GameObject upButton;
    [SerializeField] private GameObject downButton;

    private int numberOfChildren;

    private int currentPage = 0;

    void Start()
    {
        numberOfChildren = this.transform.childCount;
        pages = new GameObject[numberOfChildren];

        //Gets the reference of all children Pages
        for (int i = 0; i < numberOfChildren; i++)
        {
            pages[i] = transform.GetChild(i).gameObject;
        }
    }

    private void OnEnable()
    {
        currentPage = 0;
        upButton.SetActive(true);
    }



    void Update()
    {
        for (int x = 0; x < numberOfChildren; x++)
        {
            if(x==currentPage)
            {
                pages[x].SetActive(true);
            }
            else
            {
                pages[x].SetActive(false);
            }
        }

        if(currentPage <= 0)
        {
            downButton.SetActive(false);
        }
        else if(currentPage >= numberOfChildren - 1)
        {
            upButton.SetActive(false);
        }
        else
        {
            downButton.SetActive(true);
            upButton.SetActive(true);
        }
    }

    public void UpOnePage()
    {
        if(currentPage < numberOfChildren -1)
            currentPage++;
    }

    public void DownOnePage()
    {
        if(currentPage > 0)
            currentPage--;
    }
}
