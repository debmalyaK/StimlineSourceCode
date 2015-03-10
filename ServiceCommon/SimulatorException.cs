using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCommon
{
    [DataContract]
    public class SimulatorException
    {
        [DataMember]
        public string Title;
        [DataMember]
        public string ExceptionMessage;
        [DataMember]
        public Exception InnerException;
        [DataMember]
        public string StackTrace;
    }
}
