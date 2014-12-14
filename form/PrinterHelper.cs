using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace form
{
   public class PrinterHelper
   {
       private string BaseUrl;
       public PrinterHelper(string _baseUrl)
       {
           BaseUrl = _baseUrl; //http://wxprinter.webs.dlwebs.com/
       }
       private PrinterHelper()
       {
           
       }
       #region 同步通过GET方式发送数据
       /// <summary>
       /// 通过GET方式发送数据
       /// </summary>
       /// <param name="Url">url</param>
       /// <param name="postDataStr">GET数据</param>
       /// <param name="cookie">GET容器</param>
       /// <returns></returns>
       public string SendDataByGET(string Url, string postDataStr/*, ref CookieContainer cookie*/)
       {
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "") + postDataStr);
       /*    if (cookie.Count == 0)
           {
               request.CookieContainer = new CookieContainer();
               cookie = request.CookieContainer;
           }
           else
           {
               request.CookieContainer = cookie;
           }*/
           request.Method = "GET";
           request.ContentType = "text/html;charset=UTF-8";
           request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
      
           HttpWebResponse response = (HttpWebResponse)request.GetResponse();
           Stream myResponseStream = response.GetResponseStream();
           StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
           string retString = myStreamReader.ReadToEnd();
           myStreamReader.Close();
           myResponseStream.Close();
           return retString;
       }
       #endregion
    
       public  PrintModel.ActiveCodeMessage  ActiveDeviced(string code)
       {
           var url = BaseUrl+"index.php/service/regprinter?activecode=" + code;
           var wc = new WebClient();

           var resultJson = SendDataByGET( url, null);// wc.DownloadString(new Uri(url)).Trim();
           if (!string.IsNullOrEmpty(resultJson))
           {
               try
               {
                  var resultmsg = JsonConvert.DeserializeObject<PrintModel.ActiveCodeMessage>(resultJson);

                   return resultmsg;

               }
               catch (Exception)
               {


               }
           }
           return null;
       }


       public PrintModel.NeedPrintImgModel GetNeedPrintImg(string code)
       {
           var url = BaseUrl + "index.php/service/getimage?activecode=" + code;
          
           var resultJson = SendDataByGET(url, null);// wc.DownloadString(new Uri(url)).Trim();
           if (!string.IsNullOrEmpty(resultJson))
           {
               try
               {
                   var resultmsg = JsonConvert.DeserializeObject<PrintModel.NeedPrintImgModel>(resultJson);

                   return resultmsg;

               }
               catch (Exception)
               {
                   return null;

               }
           }
           return null;
       }

       public string SetSuccessPrinted(string resourceid)
       {

           var url = BaseUrl + "index.php/service/upres?result=1&rid=" + resourceid;
        
           var resultJson = SendDataByGET(url, null);// wc.DownloadString(new Uri(url)).Trim();
           if (!string.IsNullOrEmpty(resultJson))
           {
               try
               {
                   var resultmsg = JsonConvert.DeserializeObject<PrintModel.MessageModel>(resultJson);

                   return resultmsg.message;

               }
               catch (Exception)
               {
                   return null;

               }
           }
           return null;
           
       }


       public string GetPrintCode(string code)
       {
    

           var url = BaseUrl + "index.php/service/getcode?atcode=" + code;

           var resultJson = SendDataByGET(url, null);// wc.DownloadString(new Uri(url)).Trim();
           if (!string.IsNullOrEmpty(resultJson))
           {
               try
               {
                   var resultmsg = JsonConvert.DeserializeObject<PrintModel.CodeModel>(resultJson);

                   return resultmsg.code;

               }
               catch (Exception)
               {
                   return null;

               }
           }
           return null;
           
       }
    }
}
