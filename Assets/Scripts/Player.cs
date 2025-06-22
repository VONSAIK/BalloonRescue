using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IService
{
    [SerializeField] private int _health = 3;

    public int Health => _health;
}
