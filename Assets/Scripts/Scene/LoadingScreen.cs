using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public void LoadNextScene(string scene)
    {
        gameObject.SetActive(true);
        StartCoroutine(Load(scene));
    }
    IEnumerator Load(string scene)
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            _slider.value = async.progress;
            yield return null;
        }
    }
}
