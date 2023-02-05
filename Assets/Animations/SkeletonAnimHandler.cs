using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimHandler : MonoBehaviour
{
    public void GetDamaged()
    {
        gameObject.GetComponentInParent<Enemy>().ExecuteDamage();
    }
}
