#pragma checksum "C:\Users\ol\Desktop\C# Materials\EduConnect\EduConnect\Views\User\SignIn.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cf029477190e6461fe2c58efc2402afc5027fbd7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_SignIn), @"mvc.1.0.view", @"/Views/User/SignIn.cshtml")]
namespace AspNetCore
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
#line 1 "C:\Users\ol\Desktop\C# Materials\EduConnect\EduConnect\Views\_ViewImports.cshtml"
using EduConnect;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ol\Desktop\C# Materials\EduConnect\EduConnect\Views\_ViewImports.cshtml"
using EduConnect.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf029477190e6461fe2c58efc2402afc5027fbd7", @"/Views/User/SignIn.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b1ec55a997d597f273d40534883444a0bcc6d914", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_User_SignIn : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EduConnect.DTOs.LoginRequest>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SignIn", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\ol\Desktop\C# Materials\EduConnect\EduConnect\Views\User\SignIn.cshtml"
  
    ViewData["Title"] = "Login Page";
    Layout = "~/Views/Shared/_LandingPageLayout.cshtml";


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""mt-5"" style=""background-color:green"">
     <section class=""login_section layout_padding"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-md-6"">
                    <div class=""detail-box"">
                        <h3>
                            GET ONLINE COURSES FOR FREE
                        </h3>
                        <p>
                            Create your free account now and get immediate access to 100s of
                            online courses
                        </p>
                        <a");
            BeginWriteAttribute("href", " href=\"", 740, "\"", 747, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                            REGISTER NOW
                        </a>
                    </div>
                </div>
                <div class=""col-md-6"">
                    <div class=""login_form"">
                        <h5>
                            Login Now
                        </h5>
                        <h3 style=""color : red"">");
#nullable restore
#line 31 "C:\Users\ol\Desktop\C# Materials\EduConnect\EduConnect\Views\User\SignIn.cshtml"
                                           Write(ViewBag.success);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cf029477190e6461fe2c58efc2402afc5027fbd75257", async() => {
                WriteLiteral(@"
                            <div>
                                <input type=""email"" name=""Email"" placeholder=""Email""/>
                            </div>
                            <div>
                                <input type=""password"" name=""Password"" placeholder=""Password""/>
                            </div>
                            <button type=""submit"">Login</button>
                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n</div>\r\n\r\n   ");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EduConnect.DTOs.LoginRequest> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591