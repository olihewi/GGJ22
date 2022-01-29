using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Triggerable : MonoBehaviour
{
    public virtual void Triggered() {}
    public virtual void Held() {}
    public virtual void Finished() {}
}
