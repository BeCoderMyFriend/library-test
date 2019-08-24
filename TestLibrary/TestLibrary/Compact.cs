using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestLibrary
{
    public class Compact
    {
        // the unique object key to return from the Save method.
        private static long objectID;
        // it is used to generate the key for the serialized object.
        private static ObjectIDGenerator ObjectIDGenerator;
        // this dictionary is used to keep the memory stream for each serialized object.
        private static Dictionary<long, MemoryStream> dataObjects;
        // to check if the object has already an id.
        private static bool isFirstTime;

        public Compact()
        {
            ObjectIDGenerator = new ObjectIDGenerator();
            dataObjects = new Dictionary<long, MemoryStream>();
        }

        /// <summary>
        /// This method is used to serialize the data object and returns a unique key for it.
        /// </summary>
        /// <param name="dataObject">the object that we want to serialize</param>
        /// <returns>a unique id</returns>
        public long Save(object dataObject)
        {
            objectID = ObjectIDGenerator.GetId(dataObject, out isFirstTime);
            if (isFirstTime)
            {
                MemoryStream stream = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, dataObject);
                dataObjects.Add(objectID, stream);
            }
            return objectID;
        }

        /// <summary>
        /// This method is used to find the desired object based on its unique id.
        /// </summary>
        /// <param name="id">the object unique id</param>
        /// <returns>the found object otherwise returns null</returns>
        public object Find(long id)
        {
            if (dataObjects.ContainsKey(id))
            {
                IFormatter formatter = new BinaryFormatter();
                dataObjects[id].Seek(0, SeekOrigin.Begin);
                object result = formatter.Deserialize(dataObjects[id]);
                return result;
            }
            return null;
        }
    }
}
