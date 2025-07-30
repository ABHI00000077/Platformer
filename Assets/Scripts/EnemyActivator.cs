using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public float activationDistance = 5f;
    public GameObject visualModel;
    public Behaviour behaviorScript;
    private Transform player;
    private bool isActive = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (visualModel != null)
            visualModel.SetActive(false);

        if (behaviorScript != null)
            behaviorScript.enabled= false;
    }

    void Update()
    {
        if (!isActive && Vector2.Distance(transform.position, player.position) <= activationDistance)
        {
            ActivateEnemy();
        }
    }

    void ActivateEnemy()
    {
        isActive = true;

        if (visualModel != null)
            visualModel.SetActive(true);

        if (behaviorScript != null)
            behaviorScript.enabled=true;
    }
}
