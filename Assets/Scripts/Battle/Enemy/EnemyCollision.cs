using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollision : MonoBehaviour
{
    // �ړ��������V�[���̖��O���w��
    public string sceneToLoad;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 'Player' �^�O���t���Ă���I�u�W�F�N�g�Ƃ̏Փ˂����m
        if (other.CompareTag("Player"))
        {
            // �V�[���ړ�
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
