using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiSite.Utils {

    public delegate string CustomFormatter(string id, string format, bool isLeftJustify, int width, object arg, bool dummy);

    public interface ICustomFormatter {
        #region Public Properties

        //string Format(string id, string format, bool isLeftJustify, int width, object arg, bool dummy);
        CustomFormatter Format { get; }

        #endregion Public Properties
    }

    public static class Helper {
        private static void FormatError() {
            throw new FormatException("源格式字符串出错");
        }

        #region string的扩展函数：包含Like, Format

        /// <summary>
        ///     格式化字符串，与<see cref="T:String.Format" />拥有相同的格式化说明
        /// </summary>
        /// <param name="me">格式化说明</param>
        /// <param name="args">可以使用的参数</param>
        /// <param name="format">自定义的格式化委托<see cref="ICustomFormatter" /></param>
        /// <param name="throwable">在参数不存在的情况下是否抛出异常，默认抛出</param>
        /// <returns>经过格式化后的字符串</returns>
        /// <example>"{参数,宽度(-前导为左对齐):格式化字符串}"</example>
        public static string Format(this string me, object args, ICustomFormatter format = null, bool throwable = true)
            => Format(me, Data.FromObject(args), format, throwable);

        /// <summary>
        ///     格式化字符串，与<see cref="T:String.Format" />拥有相同的格式化说明
        /// </summary>
        /// <param name="me">格式化说明</param>
        /// <param name="args">可以使用的参数</param>
        /// <param name="format">自定义的格式化委托<see cref="ICustomFormatter" /></param>
        /// <param name="throwable">在参数不存在的情况下是否抛出异常，默认抛出</param>
        /// <returns>经过格式化后的字符串</returns>
        /// <example>"{参数,宽度(-前导为左对齐):格式化字符串}"</example>
        public static string Format<T>(this string me, Dictionary<string, T> args, ICustomFormatter format = null, bool throwable = true)
            => Format(me, Data.FromDictionary(args), format, throwable);

        /// <summary>
        ///     格式化字符串，与<see cref="T:String.Format" />拥有相同的格式化说明
        /// </summary>
        /// <param name="me">格式化说明</param>
        /// <param name="args">可以使用的参数</param>
        /// <param name="format">自定义的格式化委托<see cref="CustomFormatter" /></param>
        /// <param name="throwable">在参数不存在的情况下是否抛出异常，默认抛出</param>
        /// <returns>经过格式化后的字符串</returns>
        /// <example>"{参数,宽度(-前导为左对齐):格式化字符串}"</example>
        public static string Format(this string me, object args, CustomFormatter format, bool throwable = true)
            => Format(me, Data.FromObject(args), format, throwable);

        /// <summary>
        ///     格式化字符串，与<see cref="T:String.Format" />拥有相同的格式化说明
        /// </summary>
        /// <param name="me">格式化说明</param>
        /// <param name="args">可以使用的参数</param>
        /// <param name="format">自定义的格式化委托<see cref="CustomFormatter" /></param>
        /// <param name="throwable">在参数不存在的情况下是否抛出异常，默认抛出</param>
        /// <returns>经过格式化后的字符串</returns>
        /// <example>"{参数,宽度(-前导为左对齐):格式化字符串}"</example>
        public static string Format<T>(this string me, Dictionary<string, T> args, CustomFormatter format, bool throwable = true)
            => Format(me, Data.FromDictionary(args), format, throwable);

        /// <summary>
        ///     格式化字符串，与<see cref="T:String.Format" />拥有相同的格式化说明
        /// </summary>
        /// <param name="me">格式化说明</param>
        /// <param name="args">可以使用的参数</param>
        /// <param name="format">自定义的格式化委托<see cref="ICustomFormatter" /></param>
        /// <param name="throwable">在参数不存在的情况下是否抛出异常，默认抛出</param>
        /// <returns>经过格式化后的字符串</returns>
        /// <example>"{参数,宽度(-前导为左对齐):格式化字符串}"</example>
        public static string Format(this string me, Data args, ICustomFormatter format = null, bool throwable = true)
            => Format(me, args, format?.Format, throwable);

        /// <summary>
        ///     格式化字符串，与<see cref="T:String.Format" />拥有相同的格式化说明
        /// </summary>
        /// <param name="me">格式化说明</param>
        /// <param name="args">可以使用的参数</param>
        /// <param name="format">自定义的格式化委托<see cref="CustomFormatter" /></param>
        /// <param name="throwable">在参数不存在的情况下是否抛出异常，默认抛出</param>
        /// <returns>经过格式化后的字符串</returns>
        /// <example>"{参数,宽度(-前导为左对齐):格式化字符串}"</example>
        public static string Format(this string me, Data args, CustomFormatter format, bool throwable = true) {
            //Thrower.NullThrow(me, nameof(me));
            if (string.IsNullOrEmpty(me)) throw new ArgumentNullException(nameof(me));

            if (string.IsNullOrWhiteSpace(me) || args == null || args.Count == 0)
                return me;

            var len = me.Length;
            var buffer = StringBuilderCache.Acquire((ushort)(len + args.Count * 3));
            var pos = 0;
            var ch = '\x0';

            unsafe {
                fixed (char* fmt = me) {
                    while (true) {
                        while (pos < len) {
                            ch = fmt[pos];

                            pos++;
                            if (ch == '}') {
                                if (!(pos < len && fmt[pos] == '}')) FormatError();
                                pos++;
                            }

                            if (ch == '{')
                                if (pos < len && fmt[pos] == '{') // Treat as escape character for {{
                                    pos++;
                                else {
                                    pos--;
                                    break;
                                }

                            buffer.Append(ch);
                        }

                        if (pos == len)
                            break;
                        pos++;

                        while (pos < len && char.IsWhiteSpace(ch = fmt[pos]))
                            pos++;

                        if (!(pos != len && (ch = fmt[pos]) != ':' && ch != '}' && ch != ','))
                            FormatError();

                        var idSb = StringBuilderCache.Acquire(20);
                        do {
                            idSb.Append(ch);

                            pos++;

                            if (pos == len)
                                FormatError();

                            ch = fmt[pos];
                        } while (pos != len && ch != ':' && ch != '}' && ch != ',');

                        var id = StringBuilderCache.GetStringAndRelease(idSb);

                        if (throwable && !args.HasIndex(id))
                                throw new IndexOutOfRangeException($"{id} 超出索引");

                        var arg = args.HasIndex(id) ? args[id] : $"{{{id}}}";

                        while (pos < len && char.IsWhiteSpace(ch = fmt[pos]))
                            pos++;

                        var leftJustify = false;
                        var width = 0;

                        if (ch == ',') {
                            pos++;
                            while (pos < len && char.IsWhiteSpace(fmt[pos]))
                                pos++;

                            if (pos == len) FormatError();

                            ch = fmt[pos];
                            if (ch == '-') {
                                leftJustify = true;
                                pos++;

                                if (pos == len) FormatError();

                                ch = fmt[pos];
                            }

                            if (ch < '0' || ch > '9') FormatError();

                            do {
                                width = width * 10 + ch - '0';
                                pos++;

                                if (pos == len) FormatError();

                                ch = fmt[pos];
                            } while (ch >= '0' && ch <= '9' && width < 1000000);
                        }

                        while (pos < len && char.IsWhiteSpace(ch = fmt[pos]))
                            pos++;

                        StringBuilder addition = null;

                        if (ch == ':') {
                            pos++;

                            while (true) {
                                if (pos == len) FormatError();
                                ch = fmt[pos];
                                pos++;

                                if (ch == '{') {
                                    if (!(pos < len && fmt[pos] == '{')) FormatError();

                                    pos++;
                                } else if (ch == '}') {
                                    if (pos < len && fmt[pos] == '}') // Treat as escape character for }}
                                        pos++;
                                    else {
                                        pos--;
                                        break;
                                    }
                                }

                                if (addition == null)
                                    addition = StringBuilderCache.Acquire(300);

                                addition.Append(ch);
                            }
                        }

                        if (ch != '}') FormatError();

                        pos++;
                        var customFormat = (string)null;
                        var s = (string)null;

                        if (addition != null)
                            customFormat = StringBuilderCache.GetStringAndRelease(addition).Trim();

                        if (format != null)
                            s = format(id, customFormat, leftJustify, width, arg, !args.HasIndex(id));

                        if (s != null) {
                            buffer.Append(s);
                            continue;
                        }

                        var formattableArg = arg as IFormattable;

                        s = formattableArg?.ToString(customFormat, null) ?? arg.ToString();

                        var pad = width - s.Length;
                        if (!leftJustify && pad > 0)
                            buffer.Append(' ', pad);

                        buffer.Append(s);

                        if (leftJustify && pad > 0)
                            buffer.Append(' ', pad);
                    }

                    return StringBuilderCache.GetStringAndRelease(buffer);
                }
            }
        }

        /// <summary>
        ///     符合unix,dos的like语法
        /// </summary>
        /// <param name="me">进行比较的字符串</param>
        /// <param name="wildcards">通配符</param>
        /// <param name="ignoreCase" default="true">是否区分大小写</param>
        /// <returns></returns>
        public static bool Like(this string me, string wildcards, bool ignoreCase = true) {
            wildcards = Regex.Replace($"^{Regex.Escape(wildcards).Replace("\\*", ".*").Replace("\\?", ".")}$",
                @"\\\[(.+?)\]", m => {
                    var context = m.Groups[1].Value;

                    return $"(?:{context.Replace("\\|", "|")}){(!context.Contains("|") ? "?" : String.Empty)}";
                });

            return Regex.IsMatch(me, wildcards, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        public static bool NotLike(this string me, string wildcards, bool ignoreCase = true) => !Like(me, wildcards, ignoreCase);

        #endregion string的扩展函数：包含Like, Format
    }
}