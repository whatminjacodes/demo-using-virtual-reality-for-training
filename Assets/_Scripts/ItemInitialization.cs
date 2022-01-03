using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemInitialization", order = 2)]
public class ItemInitialization: ScriptableObject
{
 /*   Names of the Hierarchy objects  */
    public string parentName;
    public string objectName;

    /*   Default positions of Hierarchy objects  */
    public Vector3 defaultPosition;
    public Vector3 defaultRotation;

    /*   Max rotation amount    */
    public Vector3 maxRotation;

    /*   Min rotation amount   */
    public Vector3 minRotation;
}
