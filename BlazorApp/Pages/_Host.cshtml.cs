using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public class _Host : PageModel
    {
        [BindProperty]
        public IEnumerable<string> JsFiles { get; private set; }
        [BindProperty]
        public IEnumerable<string> CssFiles { get; private set; }

        private static readonly Dictionary<string, Dictionary<SupportFileType, IEnumerable<string>>> SupportFiles = new Dictionary<string, Dictionary<SupportFileType, IEnumerable<string>>>();

        private enum SupportFileType
        {
            Js,
            Css,
        }

        static _Host()
        {
            InitSupportFiles();
        }

        private static void InitSupportFiles()
        {
            InitSupportFiles(
                requestPath: "/server",
                jsFiles: Enumerate( "theather" ),
                cssFiles: Enumerate( "theather" )
            );

            InitSupportFiles(
                requestPath: "/",
                jsFiles: Enumerate( "client", "browse" ),
                cssFiles: Enumerate( "client", "browse", "mobile-interface" )
            );

            InitSupportFiles(
                requestPath: "/browse",
                jsFiles: Enumerate( "browse" ),
                cssFiles: Enumerate( "browse", "mobile-interface" )
            );
        }

        private static IEnumerable<string> Enumerate( params string[] strings )
        {
            return strings;
        }

        private static void InitSupportFiles( string requestPath, IEnumerable<string> jsFiles, IEnumerable<string> cssFiles )
        {

            SupportFiles.Add( requestPath, new Dictionary<SupportFileType, IEnumerable<string>>() { { SupportFileType.Js, jsFiles }, { SupportFileType.Css, cssFiles } } );
        }

        public void OnGet()
        {
            try
            {
                var pair = SupportFiles.First( o => Request.Path.Equals(o.Key, StringComparison.OrdinalIgnoreCase ) );
                JsFiles  = pair.Value[ SupportFileType.Js ];//.Select( o => "js/"  + o + ".js" );
                CssFiles = pair.Value[ SupportFileType.Css ];//.Select( o => "css/"  + o + ".css" );
            }
            catch
            {
            }

        }

        public _Host()
        {
            JsFiles  = Enumerable.Empty<string>();
            CssFiles = Enumerable.Empty<string>();
        }
    }
}
