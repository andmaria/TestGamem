using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameManageer : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
