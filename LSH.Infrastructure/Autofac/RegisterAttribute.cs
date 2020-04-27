using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.Infrastructure.Autofac
{
   public class RegisterAttribute:Attribute
    {
        public  Type DestType { get; set; }

        public RegisterAttribute(Type destType)
        {
            DestType = destType;
        }

    }
}
