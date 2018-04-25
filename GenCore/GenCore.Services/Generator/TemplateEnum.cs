using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.Services.Generator
{
    public class Templates
    {
        private Templates(string value) { Value = value; }

        public string Value { get; set; }

        public static Templates Class { get { return new Templates("ClassTemplate.txt"); } }
        public static Templates Property { get { return new Templates("PropertyTemplate.txt"); } }
        public static Templates Provider { get { return new Templates("ProviderTemplate.txt"); } }
    }
}
