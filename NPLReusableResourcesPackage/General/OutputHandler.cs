using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLReusableResourcesPackage.General
{
    public class OutputHandler
    {
        //this class is returned by methods in the BLL to tell the application about the out 
        public bool IsErrorOccured { get; set; } //if an error occoured this is set to true otherwise false
        public object Result { get; set; }

        public bool IsErrorKnown { get; set; } //if it's an inner exception/any other unknown error this is set to true 

        public string Message { get; set; } //holds predifined message which is returned/passed to the user
    }

}
