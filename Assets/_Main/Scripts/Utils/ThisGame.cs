using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseElement : MonoBehaviour
{
    public ThisGame App => FindObjectOfType<ThisGame>();
}

public class ThisGame : MonoBehaviour
{
    [Header("Models")]
    public PlayerModel PlayerModel;
    public EnemyModel EnemyModel;
}
