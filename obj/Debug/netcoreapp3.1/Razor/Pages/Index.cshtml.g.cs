#pragma checksum "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6603f9240f7c8fd0cbccf9ded73d6a27ea1a56be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(webAnaliser.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace webAnaliser.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/media/joker/hdd/webcore31/webAnaliser/Pages/_ViewImports.cshtml"
using webAnaliser;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
using System.Text.RegularExpressions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6603f9240f7c8fd0cbccf9ded73d6a27ea1a56be", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"792bec1fe9cdbd531b4b5529f5b14aae0ee8a810", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
  
    ViewData["Title"] = "Home page";
    var arr = (string[])ViewData["NiceStrings"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div align=\"left\" id=\"exploreSource\" class=\"text-center\">\r\n");
#nullable restore
#line 10 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
     for(int i =0; i<arr.Length; i++){
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
         if(@arr[i].Contains("<link")){
            if(@arr[i].Contains("href=\"/")){
                int st = @arr[i].IndexOf("href=\"");
                if(st>0){
                    string txt = arr[i].Substring(st+6);
                    string[] str = txt.Split('"');
                    bool check = @IndexModel.CheckIfScript(str[0]);
                    if(check){

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p");
            BeginWriteAttribute("id", " id=", 668, "", 697, 1);
#nullable restore
#line 19 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 672, IndexModel.GetParName(i), 672, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"text-align:left;\">");
#nullable restore
#line 19 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                                                                            Write(arr[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("<a onclick=\"GetScript(this)\"");
            BeginWriteAttribute("href", " href=", 758, "", 823, 1);
#nullable restore
#line 19 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 764, IndexModel.GetFullLink(@ViewData["DOM"].ToString(),str[0]), 764, 59, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Get script</a></p>\r\n");
#nullable restore
#line 20 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                    }else{

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p");
            BeginWriteAttribute("id", " id=", 898, "", 927, 1);
#nullable restore
#line 21 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 902, IndexModel.GetParName(i), 902, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"text-align:left;\">");
#nullable restore
#line 21 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                                                                            Write(arr[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("<a");
            BeginWriteAttribute("href", " href=", 962, "", 1027, 1);
#nullable restore
#line 21 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 968, IndexModel.GetFullLink(@ViewData["DOM"].ToString(),str[0]), 968, 59, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">LINK</a></p>\r\n");
#nullable restore
#line 22 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                    }
                }else{
                    st = arr[i].IndexOf("http");
                    if(st>0){
                        string txt = arr[i].Substring(st);
                        string[] str = txt.Split('"');

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p");
            BeginWriteAttribute("id", " id=", 1312, "", 1341, 1);
#nullable restore
#line 28 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 1316, IndexModel.GetParName(i), 1316, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"text-align:left;\">");
#nullable restore
#line 28 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                                                                            Write(arr[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("<a");
            BeginWriteAttribute("href", " href=", 1376, "", 1416, 1);
#nullable restore
#line 28 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 1382, IndexModel.GetFullLink("",str[0]), 1382, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">LINK</a></p>\r\n");
#nullable restore
#line 29 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                    }else{

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p");
            BeginWriteAttribute("id", " id=", 1485, "", 1514, 1);
#nullable restore
#line 30 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 1489, IndexModel.GetParName(i), 1489, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"text-align:left;\">");
#nullable restore
#line 30 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                                                                            Write(arr[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 31 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                    }
                    
                }
            }else{
                int st = arr[i].IndexOf("http");
                if(st>0){
                    string txt = arr[i].Substring(st);
                    string[] str = txt.Split('"');

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <p");
            BeginWriteAttribute("id", " id=", 1844, "", 1873, 1);
#nullable restore
#line 39 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 1848, IndexModel.GetParName(i), 1848, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"text-align:left;\">");
#nullable restore
#line 39 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                                                                        Write(arr[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("<a");
            BeginWriteAttribute("href", " href=", 1908, "", 1948, 1);
#nullable restore
#line 39 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 1914, IndexModel.GetFullLink("",str[0]), 1914, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">LINK</a></p>\r\n");
#nullable restore
#line 40 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                }else{

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <p");
            BeginWriteAttribute("id", " id=", 2009, "", 2038, 1);
#nullable restore
#line 41 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 2013, IndexModel.GetParName(i), 2013, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"text-align:left;\">");
#nullable restore
#line 41 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                                                                        Write(arr[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 42 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                }
            }
        }else{

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p");
            BeginWriteAttribute("id", " id=", 2141, "", 2170, 1);
#nullable restore
#line 45 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
WriteAttributeValue("", 2145, IndexModel.GetParName(i), 2145, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"text-align:left;\">");
#nullable restore
#line 45 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
                                                                Write(arr[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 46 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "/media/joker/hdd/webcore31/webAnaliser/Pages/Index.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591