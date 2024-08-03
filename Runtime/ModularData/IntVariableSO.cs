using UnityEngine;

namespace GameCore.ModularData
{
    [CreateAssetMenu(fileName = "Int", menuName = "Variable/Int")]
    public class IntVariableSO : ScriptableObject
    {
        public int Value;
    }
}