using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EmailProcessing.Service
{
    public interface ISoapService
    {
        HttpWebRequest SendRequest(string message);
    }
}
