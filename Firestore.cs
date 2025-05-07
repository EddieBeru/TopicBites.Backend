using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace TopicBites
{
    public class FirestoreTranslator
    {
        static private Dictionary<string, object>? ReplaceReferenceTags(Dictionary<string, object> Input)
        {
            if (Input == null)
                return null;
            Dictionary<string, object> Output = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> Pair in Input)
            {
                Dictionary<string, object>? SubDictionary = Pair.Value as Dictionary<string, object>;
                if (Pair.Key == "$id")
                {
                    Output.Add("UniqueIdentification", Pair.Value);
                }
                else if (Pair.Key == "$ref")
                {
                    Output.Add("ReferenceIdentification", Pair.Value);
                }
                else if (SubDictionary != null)
                {
                    Output.Add(Pair.Key, ReplaceReferenceTags(SubDictionary));
                }
                else
                {
                    Output[Pair.Key] = Pair.Value;
                }
            }

            return Output;
        }
        static private Dictionary<string, object> ToFlatDictionary(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                var dict = new Dictionary<string, object>();
                foreach (var prop in token.Children<JProperty>())
                {
                    dict[prop.Name] = ToFlatDictionary(prop.Value);
                }
                return dict;
            }
            else if (token.Type == JTokenType.Array)
            {
                return new Dictionary<string, object> {
                    { "$values", token.Children().Select(ToFlatDictionary).ToList() }
                };
            }
            else
            {
                return new Dictionary<string, object> {
                    { "$value", ((JValue)token).Value }
                };
            }
        }
        static public Dictionary<string, object> ConverTopictToFirestore(StudyTopic Input)
        {
            Dictionary<string, object>? Deserialized = ToFlatDictionary(JToken.Parse(ConvertTopicToJson(Input)));
            Dictionary<string, object> Output = new Dictionary<string, object>();

            Dictionary<string, string> TopicList = new Dictionary<string, string>();
            if (Deserialized == null ) 
                return null;

            //Deserialized = ReplaceReferenceTags(Deserialized);
            //return Deserialized;
            return null;

        }

        static string ConvertTopicToJson(StudyTopic root)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(root, settings);
            return json;
        }

    }

    public class FirestoreHelper
    {
        static FirestoreDb Instance { get; set; }
        static bool Initilized { get { return (Instance != null); } }
        static public FirestoreDb Initialize(string ProjectId, string KeysPath)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", KeysPath);
            return FirestoreDb.Create(ProjectId);
        }
        static public FirestoreDb GetInstace()
        {
            if (Instance == null)
            {
                throw new Exception("Firestore Database instance not initialized");
            }
            else
            {
                return Instance;
            }
        }
    }

}
