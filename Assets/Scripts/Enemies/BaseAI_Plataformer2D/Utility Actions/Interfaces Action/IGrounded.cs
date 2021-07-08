using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGrounded
{
    /// <summary>
    /// Check if is grounded, returns false if not
    /// </summary>
    /// <returns></returns>
    bool CheckGround();
}
