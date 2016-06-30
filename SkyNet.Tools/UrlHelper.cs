using System;
using System.Text.RegularExpressions;

namespace SkyNet.Tools
{
    public static class UrlHelper
    {
        #region CanonicalizeUrl

        /// <summary>
        ///     域名合并url
        /// </summary>
        /// <param name="url">子集目录url</param>
        /// <param name="domain">url 域名</param>
        public static string CanonicalizeUrl(string url, string domain)
        {
            try
            {
                var domainUri = new Uri(domain);

                var absoluteUri = new Uri(domainUri, url);

                return absoluteUri.AbsoluteUri;
            }
            catch (Exception)
            {
                return url;
            }
        }

        #endregion

        #region RemoveProtocol

        /// <summary>
        ///     移除 url 协议内容
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>移除后的url</returns>
        public static string RemoveProtocol(string url)
        {
            return Regex.Replace(url, "[\\w]+://", "", RegexOptions.IgnoreCase);
        }

        #endregion

        #region GetDomain

        /// <summary>
        ///     获取域名
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>url 的域名</returns>
        public static string GetDomain(string url)
        {
            var domain = RemoveProtocol(url);
            var i = domain.IndexOf("/", 1, StringComparison.Ordinal);
            if (i > 0)
                domain = domain.Substring(0, i);

            return domain;
        }

        #endregion

        #region IsUrl

        /// <summary>
        ///     验证是否为url
        /// </summary>
        /// <param name="url">待验证的url</param>
        /// <returns>验证的结果</returns>
        public static bool IsUrl(string url)
        {
            const string regexString = @"(((http|ftp|https)://)|(www|WWW))(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";
            return Regex.IsMatch(url, regexString);
        }

        #endregion
    }
}