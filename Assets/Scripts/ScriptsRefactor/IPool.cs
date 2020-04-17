using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool
{
    void Instantiate();
    void Spawn(Vector3 position);
    void Destroy();
}
