using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ExcavatorScriptableObject", order = 1)]
public class ExcavatorScriptableObject : ScriptableObject
{
    /*   Names of the Hierarchy objects  */
    public string prefabParentName;
    public string excavatorModelName;
    public string rightLeverName;
    public string leftLeverName;
    public string excavatorStartName;
    public string gearStickName;

    /*   Default positions of Hierarchy objects  */
    public Vector3 rightLeverDefaultPosition;
    public Vector3 leftLeverDefaultPosition;
    public Vector3 excavatorStartDefaultPosition;
    public Vector3 gearStickDefaultPosition;

    /*   Max rotation amounts    */
    public Vector3 rightLeverMaxRotation;

    /*   Min rotation amounts   */
    public Vector3 rightLeverMinRotation;
}