using System;
using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    [SerializeField] private AudioClip pickSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPickedUp(collision.gameObject);

        if (pickSound != null) SoundManager.PlayerClip(pickSound);

        Destroy(gameObject);
    }

    protected abstract void OnPickedUp(GameObject gameObject);
}
