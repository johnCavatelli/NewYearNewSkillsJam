using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int SeneToLoad = -1;
    public bool autoLoad = false;
    public float autoLoadTime = -1f;

    private void Start() {
        if(autoLoad){
            Load(autoLoadTime);
        }
    }

    public void Load(float time)
    {
        
        StartCoroutine(ActuallyLoad(SeneToLoad, time));

    }

    public void LoadNext(float time)
    {
        StartCoroutine(ActuallyLoad(SceneManager.GetActiveScene().buildIndex + 1, time));
    }     

    IEnumerator ActuallyLoad(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(index);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SeneToLoad != -1)
        {
            Load(0);
        }
        else
        {
            LoadNext(0);
        }
    }
}
