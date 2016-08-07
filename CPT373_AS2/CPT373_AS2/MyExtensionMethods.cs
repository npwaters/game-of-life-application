using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CPT373_AS2
{
    // http://www.codeproject.com/Articles/787320/An-Absolute-Beginners-Tutorial-on-HTML-Helpers-and
    // http://stackoverflow.com/questions/9030763/how-to-make-html-displayfor-display-line-breaks

    public static class MyExtensionMethods
    {
        //public static IHtmlString DisplayCells(this HtmlHelper helper,
        //string content)
        public static MvcHtmlString DisplayCells
            <TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            // split string on '\r\n' and create string array

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model).Split(new string[]
                { Environment.NewLine },
                StringSplitOptions.None);
            
            //string[] AllLines = metadata.Split(

            StringBuilder output = new StringBuilder();

            foreach (var item in model)
            {
                int i;

                for (i = 0; i < item.Length; i++)
                {
                    if (item[i] == 'O')
                    {
                        output.Append('\u2588');
                    }
                    else
                    {
                        output.Append("\u0020");
                    }
                }
                output.AppendLine();

            }

            return new MvcHtmlString(output.ToString());
        }
    }
}