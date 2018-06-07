using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routific.Respone.Error
{
    class RoutificException : Exception
    {
        public RoutificException()
        {

        }

        public RoutificException(string name)
        : base(String.Format("Name: {0}", name))
        {

        }
    }
}
