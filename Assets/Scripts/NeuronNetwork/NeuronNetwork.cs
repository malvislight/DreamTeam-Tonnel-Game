using System;
using UnityEngine;

namespace NeuronNetwork
{
    public class NeuronNetwork : MonoBehaviour
    {
        [SerializeField] private Entity _human;
        [SerializeField] private Entity _dog;
        [SerializeField] private Entity _spider;
        [SerializeField] private Entity _fish;

        private void Start()
        {
            
        }
    }

    [Serializable]
    public struct Entity
    {
        public bool _haveIntelligence;
        public bool _haveTwoLegs;
        public bool _haveBrain;
        public bool _haveFourLegs;
        public bool _haveGills;
        public bool _haveFins;
        public bool _isLivingOnLand;
        public bool _isLivingInSea;
        public bool _canTalking;
    }
    
    public static class BoolExtension
    {
        public static int ToInt(this bool @bool)
        {
            return @bool ? 1 : 0;
        }
    }
}