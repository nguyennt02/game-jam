using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DoorWiner m_DoorWinerBig;
    [SerializeField] private DoorWiner m_DoorWinerSmall;

    private void Update()
    {
        Gateway();
    }

    private void Gateway()
    {
        if (IsGateway())
        {
            LoadNextScene();
        }
    }
    private bool IsGateway()
    {

        return m_DoorWinerBig.isOpenDoor == m_DoorWinerSmall.isOpenDoor && m_DoorWinerSmall.isOpenDoor == true;
    }
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
