using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites.Model.DataManagment
{
    public class UIDManager
    {
        List<int> _unusedId;
        int _nextId;
        static private UIDManager? _instance = null;
        private UIDManager()
        {
            _nextId = 0;
            _unusedId = new List<int>();
        }
        public static UIDManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UIDManager();
            }
            return _instance;
        }
        public int GetNewId()
        {
            if (_unusedId.Count != 0)
            {
                int id = _unusedId[_unusedId.Count - 1];
                _unusedId.RemoveAt(_unusedId.Count - 1); // Remove last element
                return id;
            }
            return _nextId++;
        }
        public void AddUnusedId(int id)
        {
             _unusedId.Add(id);
        }
    }
}
