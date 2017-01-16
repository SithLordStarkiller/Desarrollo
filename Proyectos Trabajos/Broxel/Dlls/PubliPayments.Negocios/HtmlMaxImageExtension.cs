using System.Web.Mvc;

namespace PubliPayments.Negocios
{
    public static class HtmlMaxImageExtension
    {
        public static MvcHtmlString MaxImage(this HtmlHelper helper, string src, string altText = null,
            string height = null, string width = null, object htmlAttributes = null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            if (altText != null)
                builder.MergeAttribute("alt", altText);
            if (height != null)
                builder.MergeAttribute("height", height);
            if (width != null)
                builder.MergeAttribute("width", width);
            if (htmlAttributes != null)
                builder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}