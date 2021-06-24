using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;

    [SerializeField]
    private int _damage = 1;

    private Rigidbody _rb = null;
    private float _ttl = 4f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleTTL();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var moveVector = transform.TransformDirection(Vector3.forward) * (_speed);
        _rb.velocity = moveVector;
    }

    private void HandleTTL()
    {
        _ttl -= Time.deltaTime;

        if (_ttl <= 0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        ActorBase actor;

        if (other.gameObject.TryGetComponent(out actor))
        {
            actor.HurtActor(_damage);
        }

        Destroy(gameObject);
    }
}
