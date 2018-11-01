using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;
using UnityEngine.UI;

namespace ECSSystem
{
    public class ItemSystem : ComponentSystem

    {

        struct Data
        {
            public Heath heath;
        }

        
        protected override void OnUpdate()
        {
            
        }

    }
}

