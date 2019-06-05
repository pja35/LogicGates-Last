using UnityEngine;


/// <summary>
/// Cette interface permet de mettre en place le patron de conception Stratégie car les portes ont le même comportement exepté aux moment de la verification. 
/// </summary>
public abstract class Comportement : MonoBehaviour
{
    public abstract bool CalculateOut(Obj_Input[] inputs);
}