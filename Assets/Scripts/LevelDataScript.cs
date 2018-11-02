using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hivemind.Utilities
{
    [Serializable]
    public class LevelData
    {
        public int height;
        public int width;
        public string mapdata_layer1;
        public string mapdata_layer2;
        public Vector3 playerLocation;
        public Vector3 finishLocation;
        public string[] formattedMapData_layer1;
        public string[] formattedMapData_layer2;
    }
}