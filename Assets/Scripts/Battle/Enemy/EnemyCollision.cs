using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollision : MonoBehaviour
{
    // 移動したいシーンの名前を指定
    public string sceneToLoad;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 'Player' タグが付いているオブジェクトとの衝突を検知
        if (other.CompareTag("Player"))
        {
            // シーン移動
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
