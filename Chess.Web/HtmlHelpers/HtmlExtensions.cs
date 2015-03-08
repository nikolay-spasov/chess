namespace Chess.Web.HtmlHelpers
{
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static bool IsDebug(this HtmlHelper html)
        {
#if DEBUG
            return true;
#endif
            return false;
        }
    }
}