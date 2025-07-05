using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicBites.Model.DataManagment;

namespace TopicBites.Model.Iteration
{
    public class IterateToAddress : Iteration
    {
        string _address;
        string[] _splitAddress;
        string _nextStep;
        Tree? _current;
        public IterateToAddress(string address) { 
            _address = address; 
            _current = DataBase.GetInstance().Value.GetMainTree();
            _splitAddress = address.Split('/');
            if (_splitAddress.Length > 0)
            {
                _nextStep = _splitAddress[0];
            }
            else
            {
                _nextStep = string.Empty;
            }
        }
        public override StudyTopic? Current()
        {
            return _current?.GetItem();
        }

        public override bool IsNext()
        {
            if (_current == null)
            {
                return false; // No current topic
            }
            else if (_nextStep == string.Empty)
            {
                return false; // No more steps in the address
            }
            return true;
        }
        public override void Next()
        {
            _current = _current?.NavigateToAddress(_nextStep);
            int index = Array.IndexOf(_splitAddress, _nextStep);
            if (index == -1)
            {
                _nextStep = string.Empty; // No more steps in the address
            }
            else if (index < _splitAddress.Length - 1)
            {
                _nextStep = _splitAddress[index + 1]; // Get the next step in the address
            }
            else
            {
                _nextStep = string.Empty; // No more steps in the address
            }
        }
    }
}
