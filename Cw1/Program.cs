using System;
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
            var url = args.Length>0?args[0]:"https://www.pja.edu.pl";
            var httpClient = new HttpClient();
            var response =await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                var htmlContent = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+",RegexOptions.IgnoreCase);
                var matches = regex.Matches(htmlContent);
                foreach (var item in matches)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
    }
}
