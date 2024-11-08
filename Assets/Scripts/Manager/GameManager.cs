using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private string playerTag;
    public ParticleSystem effectParticle;


    public ObjectPool ObjectPool { get; private set; }
    public Transform Player { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        ObjectPool = GetComponent<ObjectPool>();
        effectParticle = GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>();
    }
}
