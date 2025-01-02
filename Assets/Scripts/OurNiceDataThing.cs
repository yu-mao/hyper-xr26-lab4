using UnityEngine;

[CreateAssetMenu(fileName = "OurNiceDataThing", menuName = "Scriptable Objects/OurNiceDataThing")]
public class OurNiceDataThing : ScriptableObject
{
    [SerializeField]
    private string name;

    public string Name => name;
}
