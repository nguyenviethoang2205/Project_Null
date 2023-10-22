using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;

[JsonObject(MemberSerialization.OptIn)]
public class Character : MonoBehaviour 
{
    [JsonProperty]
    public new string name; 

    

}
