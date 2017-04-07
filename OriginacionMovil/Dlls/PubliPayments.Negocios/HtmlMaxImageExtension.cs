using System.IO;
using System.Web.Mvc;

namespace PubliPayments.Negocios
{
    public static class HtmlMaxImageExtension
    {

        public static string TagsLinkImage(string src)
        {
            var extension = Path.HasExtension(src);
            string ext;
            if (extension)
            {
                ext = Path.GetExtension(src);
            }
            else
            {
                return "";
            }
            return ext;
        }


        public static MvcHtmlString TagImg(this HtmlHelper helper, string src, string altText = null,
            string height = null, string width = null, object htmlAttributes = null,string id = "")
        {

            if (TagsLinkImage(src) == ".pdf")
                return TagPdf(helper, src,"","28","28");

            var tagSpan = new TagBuilder("span");

            tagSpan.MergeAttribute("class", "zoom");
            if(id != "")
                tagSpan.MergeAttribute("id", "ex" + id);

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

            tagSpan.InnerHtml = builder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(tagSpan.ToString(TagRenderMode.Normal   ));
        }

        public static MvcHtmlString TagPdf(this HtmlHelper helper, string src, string altText = null,
            string height = null, string width = null, object htmlAttributes = null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", "/imagenes/pdf.jpg");
            if (altText != null)
                builder.MergeAttribute("alt", altText);
            if (height != null)
                builder.MergeAttribute("height", height);
            if (width != null)
                builder.MergeAttribute("width", width);
            if (htmlAttributes != null)
                builder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            var link = new TagBuilder("a");

            link.MergeAttribute("href", src);
            link.MergeAttribute("target", "_blank");

            link.InnerHtml = builder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(link.ToString(TagRenderMode.Normal));
        }
    }
}