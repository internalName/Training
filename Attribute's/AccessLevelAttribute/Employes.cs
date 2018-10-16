using System;
using System.Runtime.Remoting.Messaging;
using System.Xml.Serialization;

namespace AccessLevelAttribute
{
    [XmlInclude(typeof(Manager))]
    [Serializable]
   public class Employes
    {
        private string _name;
        private string _lName;
        // [XmlAttribute]
        [XmlIgnore]
        public string LName
        {
            get => _lName;
            set => _lName = value;
        }
       // [XmlAttribute]
       public string Name
       {
           get => _name;
           set => _name = value;
       }

       public AccessLevelAttribute AccessLevelAttribute => (AccessLevelAttribute)Attribute.GetCustomAttribute(GetType(), typeof(AccessLevelAttribute));

        [Obsolete("Problem, Huston")]
       public string Message()=> $"{GetType()} устарел";

        [Obsolete("",true)]
       public string Message1()=> "not bad, try again later";
    }

    [AccessLevel(EnumAccessLevel.Low),Serializable,XmlInclude(typeof(Programmer))]
    public class Manager : Employes
    {
        
    }

    [AccessLevel(EnumAccessLevel.Medium),Serializable,XmlInclude(typeof(Director))]
    public class Programmer : Manager
    {

    }

    [AccessLevel(EnumAccessLevel.Hight),Serializable]
    public sealed class Director : Programmer
    {

    }
}
