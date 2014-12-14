using System;
using System.Collections.Generic;
using System.Text;

namespace form
{
   public class PrintModel
   {
       public class ActiveCodeMessage
       {
           public string message { get; set; }
           public string msgcode { get; set; }
     
       }
       public class NeedPrintImgModel
       {
           public string resource_id { get; set; }
           public string resource_content { get; set; }
       }
       
       public class MessageModel
       {
           public string message { get; set; } 
       }
       public class CodeModel
       {
           public string code { get; set; }
       }
    }
}
