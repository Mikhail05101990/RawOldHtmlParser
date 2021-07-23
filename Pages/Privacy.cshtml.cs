using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace webAnaliser.Pages
{
    public class PrivacyModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        string str;
        string beautyStr;

        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            str = await GetResponseText();
            beautyStr = GetCleanString(str);
            ViewData["htmlString"] = beautyStr;
        }

        public async Task<string> GetResponseText()
        {
            try	
            {
                string responseBody = await client.GetStringAsync("https://tinder.com/");
                return responseBody;
            }
            catch(HttpRequestException e)
            {   
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        private string GetCleanString(string text){
            char[] arr = new char[]{')', '(', (char)39, '´', '©', '_','’', '"', ':', ';', '/', '\\', '|', '!', '@', '.', ',', '<', '>', '?', '{', '}', '=', '+', '-', '#', '%' };

            string result = string.Concat(text
            .Where(c => Char.IsLetter(c) || Char.IsDigit(c) || Char.IsWhiteSpace(c) || arr.Contains(c)));

            return result;
        }

        public string[] GetWordArray(string text){
            string[] arr = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return arr;
        }

    }
}
