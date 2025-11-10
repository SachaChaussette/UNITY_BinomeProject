using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathComponent : MonoBehaviour
{
    [SerializeField] bool isGrounded = false;
    [SerializeField] float maxDist = 10.0f;
    [SerializeField] float currentTime = 0.0f, maxTime = 5.0f;

    void Start()
    {
        
    }


    void Update()
    {
        isGrounded = CheckGround();
        if (!isGrounded)
        {
            IncreaseTime();
            return;
        }
        Reset();

    }

    void Reset()
    {
        currentTime = 0.0f;
    }

    void IncreaseTime()
    {
        currentTime += Time.deltaTime;

        if(currentTime > maxTime)
        {
            currentTime = 0.0f;
            Die();
        }
    }

    bool CheckGround()
    {
        Ray _ray = new Ray(transform.position, -transform.up);

        return Physics.Raycast(_ray, maxDist);
    }

    void Die()
    {
        ReloadLevel();
    }

    void ReloadLevel()
    {
        Scene _currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_currentScene.name);
    }
}
