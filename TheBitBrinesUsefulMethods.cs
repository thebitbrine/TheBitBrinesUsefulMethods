using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace TheBitBrine
{
    class UsefulMethods
    {
        public bool Empty(object Stuff)
        {
            if (Stuff == "")
                return true;
            if (Stuff == null)
                return true;
            if (Stuff.ToString() == "")
                return true;
            return false;
        }
        public List<string> GetLinksRegex(string message)
        {
            List<string> list = new List<string>();

            if (!Empty(message))
            {
                Regex urlRx = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase);

                MatchCollection matches = urlRx.Matches(message);
                foreach (Match match in matches)
                {
                    list.Add(match.Value);
                }
                return list;
            }

            return list;
        }

        public string HrefToFullLink(string ParentLink, string HTMLContent)
        {
            if (!Empty(ParentLink) && !Empty(HTMLContent))
            {
                string HTMLContentWithTheMeat = "";
                string[] HTMLContentLines = HTMLContent.Split('\n');
                for (int i = 0; i < HTMLContentLines.Length; i++)
                {
                    try
                    {
                        //string TheFatMeat = GetBetween(HTMLContentLines[i], "<a href=\"", "\">");
                        string TheMeat = GetBetween(HTMLContentLines[i], "<a href=\"", "\">"); /*GetBetween(TheFatMeat, ">", "<");*/
                        string TheHalfFatMeat = "<a href=\"" + TheMeat + "\">" + TheMeat + "</a>";
                        string TheBBQ = "";

                        if (ParentLink.EndsWith("/") == true) TheBBQ = "<a href=\"" + ParentLink + TheMeat + "\">" + TheMeat + "</a>";
                        else TheBBQ = "<a href=\"" + ParentLink + "/" + TheMeat + "\">" + TheMeat + "</a>";

                        if (TheMeat != "" && TheMeat != "../" && TheMeat != "..")
                            HTMLContentWithTheMeat += TheBBQ;
                        else
                            HTMLContentWithTheMeat += "";

                    }
                    catch
                    {
                        HTMLContentWithTheMeat += "";
                    }

                }
                return HTMLContentWithTheMeat;
            }
            else
            {
                return null;
            }
        }

        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public List<FileInfo> DirSearch(string sDir)
        {
            List<FileInfo> List = new List<FileInfo>();
            FileInfo TempInfo;
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        TempInfo = new FileInfo(f);
                        List.Add(TempInfo);
                    }
                    DirSearch(d);
                }
                return List;
            }
            catch (System.Exception excpt)
            {
                //List.Add(excpt.Message);
                return null;
            }

        }

        public string LinkToHTML(string Link)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                try
                {
                    return webClient.DownloadString(Link);
                }
                catch (Exception ex)
                {

                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            return webClient.DownloadString(Link);
                            Thread.Sleep(250);
                        }
                        catch
                        {
                            
                        }
                    }
                    //MessageBox.Show("Invaild or unreachable host. Check the link to make sure it's up and valid.\r\nStats for nerds: \r\n" + ex.Message, "Something's Wrong", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
                    return webClient.DownloadString(Link);
            }
        }

        public bool VaildateLink(string Link)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadString(Link);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool HTMLOrNot(string Link)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Link);
                request.Method = "GET";
                request.ServicePoint.Expect100Continue = false;
                //request.ContentType = "application/x-www-form-urlencoded";

                using (WebResponse response = request.GetResponse())
                {
                    if (response.ContentType == "text/html")
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

    }

}
