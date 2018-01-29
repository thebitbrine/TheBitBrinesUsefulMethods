# TheBitBrine's Useful Methods


### Dependencies:
```
System;
System.Collections.Generic;
System.IO;
System.Net;
System.Security.Cryptography;
System.Text;
System.Text.RegularExpressions;
System.Threading;
```
### Contains:
* `public bool Empty(object Stuff)` *Checks if object is either `null` or not*
* `public List<string> GetLinksRegex(string message)` *Returns URLs from a string as a `List<string>` using Regex*
* `public string HrefToFullLink(string ParentLink, string HTMLContent)` *Gets URLs From HTML, puts them in `<a>`*
* `public static string GetBetween(string strSource, string strStart, string strEnd)` *Return between*
* `public List<FileInfo> DirSearch(string sDir)` *Directory Crawler*
* `public string LinkToHTML(string Link)` *Gets and URL and returns its HTML content*
* `public bool VaildateLink(string Link)` *Validates the given link*
* `public bool HTMLOrNot(string Link)` *Checks if the given URL points to an HTML file or not*
* `public string MD5Hash(string input)` *Returns the `MD5 Hash` of a `string`*
