using System.IO.Compression;
using System.Text;

namespace Ichihai1415.Utilities
{
    public class GZipConverter
    {
        /// <summary>
        /// 文字列をBase64に変換します。
        /// </summary>
        /// <param name="text">変換する文字列</param>
        /// <returns>変換された文字列</returns>
        public static string String2Base64(string text) => Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

        /// <summary>
        /// 文字列をGZipに変換します。
        /// </summary>
        /// <param name="text">変換する文字列</param>
        /// <returns>変換された文字列</returns>
        public static string String2GZip(string text) => Convert.ToBase64String(GZipConverterInternal.GZipCompress(Encoding.UTF8.GetBytes(text)));

        /// <summary>
        /// Base64を文字列に変換します。
        /// </summary>
        /// <param name="base64String">変換するbase64の文字列</param>
        /// <returns>変換された文字列</returns>
        public static string Base642String(string base64String) => Encoding.UTF8.GetString(Convert.FromBase64String(base64String));

        /// <summary>
        /// GZipを文字列に変換します。
        /// </summary>
        /// <param name="gzipString">変換するgzipの文字列</param>
        /// <returns>変換された文字列</returns>
        public static string GZip2String(string gzipString) => Encoding.UTF8.GetString(GZipConverterInternal.GZipExtract(Convert.FromBase64String(gzipString)));

        /// <summary>
        /// GZipの展開・圧縮の内部クラスです。通常はこちらを使用する必要はありません。
        /// </summary>
        public class GZipConverterInternal
        {
            /// <summary>
            /// GZipを圧縮します。
            /// </summary>
            /// <remarks>参考: <see href="https://kagasu.hatenablog.com/entry/2016/10/26/034311"/></remarks>
            /// <param name="bytes">圧縮前の<see cref="byte[]"/></param>
            /// <returns>圧縮後の<see cref="byte[]"/></returns>
            public static byte[] GZipCompress(byte[] bytes)
            {
                using var ms = new MemoryStream();
                using (var gzipStream = new GZipStream(ms, CompressionLevel.Fastest))
                    gzipStream.Write(bytes, 0, bytes.Length);
                return ms.ToArray();
            }

            /// <summary>
            /// GZipを展開します。
            /// </summary>
            /// <remarks>参考: <see href="https://kagasu.hatenablog.com/entry/2016/10/26/034311"/></remarks>
            /// <param name="bytes">展開前のgzip(<see cref="byte[]"/>)</param>
            /// <returns>展開後の<see cref="byte[]"/></returns>
            public static byte[] GZipExtract(byte[] bytes)
            {
                var buffer = new byte[1024];
                using var ms = new MemoryStream();
                using (var gzipStream = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress))
                    while (true)
                    {
                        var readSize = gzipStream.Read(buffer, 0, buffer.Length);
                        if (readSize == 0) break;
                        ms.Write(buffer, 0, readSize);
                    }
                return ms.ToArray();
            }
        }
    }
}
