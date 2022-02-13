using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ARParent : MonoBehaviour
{
    public void Start()
    {
        LookAtConstraint lookAt = transform.GetChild(0).GetComponent<LookAtConstraint>();
        ConstraintSource src = new ConstraintSource();
        src.sourceTransform = Camera.main.transform;
        src.weight = 1f;
        lookAt.AddSource(src);
    }

    public void DestroyOnClic()
    {
        Destroy(gameObject);
    }
}
