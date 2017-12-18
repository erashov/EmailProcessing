using System;
using System.Collections.Generic;
using System.Text;

namespace EmailProcessing.Model.EmailProcessingViewModels
{

    public class Message
    {
      
        public string MessageID { get; set; }
        public string Subject { get; set; }
        public List<ParamMessage> Params { get; set; }
    }
}
