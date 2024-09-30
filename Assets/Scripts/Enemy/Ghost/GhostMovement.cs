using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwareofPlayer
{
    public class GhostMovement : MonoBehaviour
    {
        public Transform player;

        [SerializeField]
        private float speed = 4f;

        [SerializeField]
        private float rotationSpeed = 100f;

        private Rigidbody2D rigidbody2D;
        private PlayerAware playerAware;
        private Vector2 targetDirection;
        public GameObject sprite;
        public GameObject anchor;

        // Variable for AwareOfPlayer
        public bool AwareOfPlayer { get; private set; }

        void Start()
        {
            anchor = GameObject.Find("EnemyAnchor");
        }

        void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            playerAware = GetComponent<PlayerAware>();
        }

        void FixedUpdate()
        {
            sprite.transform.rotation = anchor.transform.rotation;
            UpdateTargetDirection();
            RotateTowardsTarget();
            SetVelocity();
        }

        private void UpdateTargetDirection()
        {
            AwareOfPlayer = playerAware.AwareOfPlayer;
            if (AwareOfPlayer)
            {
                targetDirection = playerAware.DirectionToPlayer;
            }
            else
            {
                targetDirection = Vector2.zero;
            }
        }

        private void RotateTowardsTarget()
        {
            if (targetDirection == Vector2.zero) return;

            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            rigidbody2D.rotation = Mathf.LerpAngle(rigidbody2D.rotation, targetRotation.eulerAngles.z, rotationSpeed * Time.deltaTime);
        }

        private void SetVelocity()
        {
            if (targetDirection == Vector2.zero)
            {
                rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                rigidbody2D.velocity = transform.up * speed;
            }
        }
    }

    public class PlayerAware : MonoBehaviour
    {
        public bool AwareOfPlayer { get; private set; }
        public Vector2 DirectionToPlayer { get; private set; }

        // Implement logic to detect player and set AwareOfPlayer and DirectionToPlayer accordingly
    }
}
