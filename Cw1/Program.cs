using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //int a = 1;
            //double b = 2.0;
            //string tmp1 = "Ala ma kota";
            //string tmp2 = "ABC";
            //var res = $"{tmp1} {tmp2}";
            //Console.WriteLine(res);

            //var newPerson = new Person { FirstName = "Daniel" };
            if (args.Length==0)
            {
                throw new ArgumentNullException
                    ("Zero parameters were passed");
            }
            //var url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";
            var url = args[0];
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var htmlContent = await response.Content.ReadAsStringAsync();
                    var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);
                    var matches = regex.Matches(htmlContent);
                    if (matches.Count != 0)
                    {
                        //List<String> repeat = new List<String>();
                        HashSet<String> nonRepeat=new HashSet<string>();
                        foreach (var item in matches)
                        {
                            // if (!repeat.Contains(item.ToString()))
                            // {
                            //     repeat.Add(item.ToString());
                            //     Console.WriteLine(item.ToString());
                            // }
                            nonRepeat.Add(item.ToString());
                        }

                        foreach (var text in nonRepeat)
                        {
                            Console.WriteLine(text);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No adress were found");
                    }
                }

                httpClient.Dispose();
            }
            catch (InvalidOperationException wrongReq)
            {
                throw new ArgumentException("Wrong adress");
            }
            catch (HttpRequestException reqEx)
            {
                Console.WriteLine("Error while requesting");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
