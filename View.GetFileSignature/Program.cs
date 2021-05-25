using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace View.GetFileSignature
{
    class Program
    {
        /// <summary>
        /// 需要获取文件签名的后缀名
        /// </summary>
        static List<string> exts = new List<string>()
        {
            ".323",
            ".aaf",
            ".aca",
            ".accdb",
            ".accde",
            ".accdt",
            ".acx",
            ".afm",
            ".ai",
            ".aif",
            ".aifc",
            ".aiff",
            ".application",
            ".art",
            ".asd",
            ".asf",
            ".asi",
            ".asm",
            ".asr",
            ".asx",
            ".atom",
            ".au",
            ".avi",
            ".axs",
            ".bas",
            ".bcpio",
            ".bin",
            ".bmp",
            ".c",
            ".cab",
            ".calx",
            ".cat",
            ".cdf",
            ".chm",
            ".class",
            ".clp",
            ".cmx",
            ".cnf",
            ".cod",
            ".cpio",
            ".cpp",
            ".crd",
            ".crl",
            ".crt",
            ".csh",
            ".css",
            ".csv",
            ".cur",
            ".dcr",
            ".deploy",
            ".der",
            ".dib",
            ".dir",
            ".disco",
            ".dll",
            ".config",
            ".dlm",
            ".doc",
            ".docm",
            ".docx",
            ".dot",
            ".dotm",
            ".dotx",
            ".dsp",
            ".dtd",
            ".dvi",
            ".dwf",
            ".dwp",
            ".dxr",
            ".eml",
            ".emz",
            ".eot",
            ".eps",
            ".etx",
            ".evy",
            ".exe",
            ".fdf",
            ".fif",
            ".fla",
            ".flr",
            ".flv",
            ".gif",
            ".gtar",
            ".gz",
            ".h",
            ".hdf",
            ".hdml",
            ".hhc",
            ".hhk",
            ".hhp",
            ".hlp",
            ".hqx",
            ".hta",
            ".htc",
            ".htm",
            ".html",
            ".htt",
            ".hxt",
            ".ico",
            ".ics",
            ".ief",
            ".iii",
            ".inf",
            ".ins",
            ".isp",
            ".IVF",
            ".jar",
            ".java",
            ".jck",
            ".jcz",
            ".jfif",
            ".jpb",
            ".jpe",
            ".jpeg",
            ".jpg",
            ".js",
            ".jsx",
            ".latex",
            ".lit",
            ".lpk",
            ".lsf",
            ".lsx",
            ".lzh",
            ".m13",
            ".m14",
            ".m1v",
            ".m3u",
            ".man",
            ".manifest",
            ".map",
            ".mdb",
            ".mdp",
            ".me",
            ".mht",
            ".mhtml",
            ".mid",
            ".midi",
            ".mix",
            ".mmf",
            ".mno",
            ".mny",
            ".mov",
            ".movie",
            ".mp2",
            ".mp3",
            ".mpa",
            ".mpe",
            ".mpeg",
            ".mpg",
            ".mpp",
            ".mpv2",
            ".ms",
            ".msi",
            ".mso",
            ".mvb",
            ".mvc",
            ".nc",
            ".nsc",
            ".nws",
            ".ocx",
            ".oda",
            ".odc",
            ".ods",
            ".one",
            ".onea",
            ".onetoc",
            ".onetoc2",
            ".onetmp",
            ".onepkg",
            ".osdx",
            ".p10",
            ".p12",
            ".p7b",
            ".p7c",
            ".p7m",
            ".p7r",
            ".p7s",
            ".pbm",
            ".pcx",
            ".pcz",
            ".pdf",
            ".pfb",
            ".pfm",
            ".pfx",
            ".pgm",
            ".pko",
            ".pma",
            ".pmc",
            ".pml",
            ".pmr",
            ".pmw",
            ".png",
            ".pnm",
            ".pnz",
            ".pot",
            ".potm",
            ".potx",
            ".ppam",
            ".ppm",
            ".pps",
            ".ppsm",
            ".ppsx",
            ".ppt",
            ".pptm",
            ".pptx",
            ".prf",
            ".prm",
            ".prx",
            ".ps",
            ".psd",
            ".psm",
            ".psp",
            ".pub",
            ".qt",
            ".qtl",
            ".qxd",
            ".ra",
            ".ram",
            ".rar",
            ".ras",
            ".rf",
            ".rgb",
            ".rm",
            ".rmi",
            ".roff",
            ".rpm",
            ".rtf",
            ".rtx",
            ".scd",
            ".sct",
            ".sea",
            ".setpay",
            ".setreg",
            ".sgml",
            ".sh",
            ".shar",
            ".sit",
            ".sldm",
            ".sldx",
            ".smd",
            ".smi",
            ".smx",
            ".smz",
            ".snd",
            ".snp",
            ".spc",
            ".spl",
            ".src",
            ".ssm",
            ".sst",
            ".stl",
            ".sv4cpio",
            ".sv4crc",
            ".swf",
            ".t",
            ".tar",
            ".tcl",
            ".tex",
            ".texi",
            ".texinfo",
            ".tgz",
            ".thmx",
            ".thn",
            ".tif",
            ".tiff",
            ".toc",
            ".tr",
            ".trm",
            ".tsv",
            ".ttf",
            ".txt",
            ".u32",
            ".uls",
            ".ustar",
            ".vbs",
            ".vcf",
            ".vcs",
            ".vdx",
            ".vml",
            ".vsd",
            ".vss",
            ".vst",
            ".vsto",
            ".vsw",
            ".vsx",
            ".vtx",
            ".wav",
            ".wax",
            ".wbmp",
            ".wcm",
            ".wdb",
            ".wks",
            ".wm",
            ".wma",
            ".wmd",
            ".wmf",
            ".wml",
            ".wmlc",
            ".wmls",
            ".wmlsc",
            ".wmp",
            ".wmv",
            ".wmx",
            ".wmz",
            ".wps",
            ".wri",
            ".wrl",
            ".wrz",
            ".wsdl",
            ".wvx",
            ".x",
            ".xaf",
            ".xaml",
            ".xap",
            ".xbap",
            ".xbm",
            ".xdr",
            ".xla",
            ".xlam",
            ".xlc",
            ".xlm",
            ".xls",
            ".xlsb",
            ".xlsm",
            ".xlsx",
            ".xlt",
            ".xltm",
            ".xltx",
            ".xlw",
            ".xml",
            ".xof",
            ".xpm",
            ".xps",
            ".xsd",
            ".xsf",
            ".xsl",
            ".xslt",
            ".xsn",
            ".xtp",
            ".xwd",
            ".z",
            ".zip",
        };

        static void Main(string[] args)
        {

            Console.WriteLine("获取文件签名的 C# 代码文件:\r\n");

            int count = exts.Count;
            int index = 0;
            var pos = Console.GetCursorPosition();
            Console.WriteLine($"当前进度 : {index} / {count}");

            HttpClient client = new HttpClient();
            Dictionary<string, List<string>> FileSignature = new Dictionary<string, List<string>>();
            foreach (var ext in exts)
            {
                var html = client.GetStringAsync(ext.ToUrl()).GetAwaiter().GetResult();

                var results = html.GetMarkups("span", "results");
                if (results != null && results.Count >= 2 && results.Count % 2 == 0)
                {
                    for (int i = 0; i < results.Count; i += 2)
                    {
                        var extName = results[i].GetContent("a")?.ToLower();
                        var signuratureName = results[i + 1].GetContent("a");
                        if (!string.IsNullOrEmpty(extName) && !string.IsNullOrEmpty(signuratureName))
                        {
                            string key = $".{extName}";
                            if (!FileSignature.ContainsKey(key))
                            { FileSignature.Add(key, new List<string>()); }
                            string vs = string.Join(", ", signuratureName.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => $"0x{x}").ToArray());
                            FileSignature[key].Add($"new byte[] {{ {vs} }}");
                        }
                    }
                }

                index++;
                Console.SetCursorPosition(pos.Left, pos.Top);
                Console.WriteLine($"当前进度 : {index} / {count}");
            }


            Console.WriteLine("合成 C# 代码...");
            string csharp = "";
            foreach (var keyValue in FileSignature)
            {
                string value = string.Join(", ", keyValue.Value);
                csharp += $"{{ \"{keyValue.Key}\", new List<byte[]> {{ {value} }} }},\r\n";
            }

            csharp = @$"static readonly Dictionary<string, List<byte[]>> FileSignature = new Dictionary<string, List<byte[]>>
{{
    {csharp}
}};
";

            Console.WriteLine("合成 C# 代码...");
            File.WriteAllText(@"FileSignature.txt", csharp);

            Console.WriteLine();
            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }

    public static class HtmlExtensions
    {
        /// <summary>
        /// 获取访问签名数据库网页的 url
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string ToUrl(this string ext)
        {
            if (ext.StartsWith("."))
            { ext = ext[1..]; }
            return $"https://www.filesignatures.net/index.php?search={ext.ToLower()}&mode=EXT";
        }

        /// <summary>  
        /// 获取 html 文本中指定标签的值  
        /// </summary>  
        /// <param name="html">html 文本</param>  
        /// <param name="title">标签</param>  
        /// <returns>值</returns>  
        public static string GetContent(this string html, string title)
        {
            string tmpStr = string.Format("<{0}[^>]*?>(?<Text>[^<]*)</{1}>", title, title); //获取<title>之间内容  

            Match TitleMatch = Regex.Match(html, tmpStr, RegexOptions.IgnoreCase);

            return TitleMatch.Groups["Text"].Value;
        }

        /// <summary>  
        /// 获取 html 文本中id为指定值的标签  
        /// </summary>  
        /// <param name="html">html 文本</param>  
        /// <param name="title">标签</param>  
        /// <param name="id">Id</param>  
        /// <returns>属性</returns>  
        public static List<string> GetMarkups(this string html, string title, string id)
        {
            string tmpStr = $"<{title}(.*?)id\\s?=\\s?\"{id}\"\\s?>.*?</{title}>"; //获取<title>之间内容  

            var TitleMatchs = Regex.Matches(html, tmpStr, RegexOptions.IgnoreCase);

            return TitleMatchs.Select(x => x.Value).ToList();
        }
    }
}
