using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileProperties : MonoBehaviour
{
    public struct ProjectilePorperties
    {
        public Vector3 direction;
        public Vector3 initial_position;
        public float initial_speed;
        public float mass;
        public float drag;
    }
}
