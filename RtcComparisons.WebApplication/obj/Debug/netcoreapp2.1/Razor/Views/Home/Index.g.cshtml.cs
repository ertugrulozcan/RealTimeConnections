#pragma checksum "/Users/admin/Projects/ArGe/RealTimeConnections/RtcComparisons.WebApplication/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7cc7eba7baf7671235629b4d6e8063c011427a1a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "/Users/admin/Projects/ArGe/RealTimeConnections/RtcComparisons.WebApplication/Views/_ViewImports.cshtml"
using RtcComparisons.WebApplication;

#line default
#line hidden
#line 2 "/Users/admin/Projects/ArGe/RealTimeConnections/RtcComparisons.WebApplication/Views/_ViewImports.cshtml"
using RtcComparisons.WebApplication.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7cc7eba7baf7671235629b4d6e8063c011427a1a", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"528af9fd56a05941f5ab3fc0031f181a29eff41e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "/Users/admin/Projects/ArGe/RealTimeConnections/RtcComparisons.WebApplication/Views/Home/Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(42, 4031, true);
            WriteLiteral(@"<div class=""outline_border"" xmlns=""http://www.w3.org/1999/html"">
	<div style=""text-align: center;"">
		<h1>Real Time Connections</h1>
	</div>
	<br/>
	<br/>
	<br/>
	
	<!-- WebSocket -->
	<img src=""images/websockets.png"" width=""110""/>
	<h4>WebSocket Client</h4>
	<div class=""row"">
		<div class=""window browser fading"">
			<div class=""header"">
				<span class=""bullet bullet-red""></span>
				<span class=""bullet bullet-yellow""></span>
				<span class=""bullet bullet-green""></span>
				<span class=""title"">
					<label for=""websocketUrlInputBox""></label><input id=""websocketUrlInputBox"" value=""ws://localhost:9716/ws""/>
				</span>

				<button id=""websocketConnectButton"" type=""submit"" href=""#"">Connect</button>
			</div>

			<div class=""body"">
				<div id=""websocketOutputBox""></div>
			</div>

			<div class=""footer"">
				<span class=""title"">
					<label for=""websocketMessageInputBox""></label><input id=""websocketMessageInputBox""/>
				</span>

				<button id=""websocketSendButton"" type=""submit"" href=""#"">Send</button>
				<b");
            WriteLiteral(@"utton id=""websocketDataTransferTestButton"" type=""submit"" href=""#"">Send Test Data</button>
			</div>
		</div>
	</div>
	<br/>
	<hr/>
	<br/>

	<!-- Grpc -->
	<img src=""images/grpc.png"" width=""170""/>
	<h4>Grpc Client</h4>
	<div class=""row"">
		<div class=""window browser fading"">
			<div class=""header"">
				<span class=""bullet bullet-red""></span>
				<span class=""bullet bullet-yellow""></span>
				<span class=""bullet bullet-green""></span>
				<span class=""title"">
					<label for=""grpcUrlInputBox""></label><input id=""grpcUrlInputBox"" value=""localhost:9720""/>
				</span>

				<button id=""grpcConnectButton"" type=""submit"" href=""#"">Connect</button>
			</div>

			<div class=""body"">
				<div id=""grpcOutputBox""></div>
			</div>

			<div class=""footer"">
				<span class=""title"">
					<label for=""grpcMessageInputBox""></label><input id=""grpcMessageInputBox""/>
				</span>

				<button id=""grpcSendButton"" type=""submit"" href=""#"">Send</button>
				<button id=""grpcDataTransferTestButton"" type=""submit"" href=""#"">Send Test Data</button>");
            WriteLiteral(@"
			</div>
		</div>
	</div>
	<br/>
	<hr/>
	<br/>

	<!-- NodeJS -->
	<img src=""images/nodejs.png"" width=""200""/>
	<h4>NodeJS Client</h4>
	<div class=""row"">
		<div class=""window browser fading"">
			<div class=""header"">
				<span class=""bullet bullet-red""></span>
				<span class=""bullet bullet-yellow""></span>
				<span class=""bullet bullet-green""></span>
				<span class=""title"">
					<label for=""nodejsUrlInputBox""></label><input id=""nodejsUrlInputBox"" value=""http://localhost:9722""/>
				</span>

				<button id=""nodejsConnectButton"" type=""submit"" href=""#"">Connect</button>
			</div>

			<div class=""body"">
				<div id=""nodejsOutputBox""></div>
			</div>

			<div class=""footer"">
				<span class=""title"">
					<label for=""nodejsMessageInputBox""></label><input id=""nodejsMessageInputBox""/>
				</span>

				<button id=""nodejsSendButton"" type=""submit"" href=""#"">Send</button>
				<button id=""nodejsDataTransferTestButton"" type=""submit"" href=""#"">Send Test Data</button>
			</div>
		</div>
	</div>
	<br/>
	<hr/>
	<br/>

	<!-- Sign");
            WriteLiteral(@"alR -->
	<img src=""images/signalr.png"" width=""200""/>
	<h4>SignalR Client</h4>
	<div class=""row"">
		<div class=""window browser fading"">
			<div class=""header"">
				<span class=""bullet bullet-red""></span>
				<span class=""bullet bullet-yellow""></span>
				<span class=""bullet bullet-green""></span>
				<span class=""title"">
					<label for=""signalrUrlInputBox""></label><input id=""signalrUrlInputBox"" value=""http://localhost:9718/hub""/>
				</span>

				<button id=""signalrConnectButton"" type=""submit"" href=""#"">Connect</button>
			</div>

			<div class=""body"">
				<div id=""signalrOutputBox""></div>
			</div>

			<div class=""footer"">
				<span class=""title"">
					<label for=""signalrMessageInputBox""></label><input id=""signalrMessageInputBox""/>
				</span>

				<button id=""signalrSendButton"" type=""submit"" href=""#"">Send</button>
				<button id=""signalrDataTransferTestButton"" type=""submit"" href=""#"">Send Test Data</button>
				
			</div>
		</div>
	</div>
</div>
");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
