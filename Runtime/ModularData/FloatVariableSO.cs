using UnityEngine;

namespace GameCore.ModularData
{
    [CreateAssetMenu(fileName = "Float", menuName = "Variable/Float")]
    public class FloatVariableSO : ScriptableObject
    {
        public int Value;
    }
}