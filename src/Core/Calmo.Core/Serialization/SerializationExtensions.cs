using System;
using System.IO;
#if !__MOBILE__
using System.Runtime.Serialization.Formatters.Binary;
#endif
using System.Text;
using System.Xml.Serialization;

#if __MOBILE__
using Newtonsoft.Json;
#endif

namespace Calmo.Core.Serialization
{
	/// <summary>
	/// Utilities for data serialization
	/// </summary>
    public static class SerializationExtensions
    {
		/// <summary>
		/// Convert any object to a byte[]
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="objeto">Object</param>
		/// <returns>Byte array</returns>
        public static byte[] ToBytes<T>(this T objeto)
        {
            if (objeto == null)
                return null;

#if !__MOBILE__
            var formatter = new BinaryFormatter();
            byte[] buffer;

            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, objeto);
#else
            var json = JsonConvert.SerializeObject(objeto);
            var bytes = Encoding.UTF8.GetBytes(json);

            byte[] buffer;

            using (var ms = new MemoryStream(bytes))
            {
#endif
                ms.Flush();
                ms.Position = 0;

                buffer = new byte[ms.Length];
                ms.Read(buffer, 0, buffer.Length);
            }

            return buffer;
        }

		/// <summary>
		/// Convert a byte[] to an object of type T
		/// </summary>
		/// <typeparam name="T">Object Type</typeparam>
		/// <param name="bytes">Object as a byte[]</param>
		/// <returns>T object</returns>
        public static T FromBytes<T>(this byte[] bytes)
        {
            if (bytes == null) return default(T);

#if !__MOBILE__
            var formatter = new BinaryFormatter();
#endif

            using (var ms = new MemoryStream(bytes))
            {
                try
                {
#if !__MOBILE__
                    var value = (T)formatter.Deserialize(ms);
                    return value;
#else
                        ms.Flush();
                        ms.Position = 0;

                        var buffer = new byte[ms.Length];
                        ms.Read(buffer, 0, buffer.Length);
                        
                        var json = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                        return JsonConvert.DeserializeObject<T>(json);
#endif
                }
                catch (Exception)
                {
                    throw new InvalidCastException($"Não foi possível converter os bytes informados para o tipo {typeof(T).FullName}.");
                }

            }
        }

		/// <summary>
		/// Convert an object of type T to a base 64 string
		/// </summary>
		/// <typeparam name="T">Object Type</typeparam>
		/// <param name="objeto">Object Value</param>
		/// <returns>Base 64 string</returns>
        public static string ToBase64<T>(this T objeto)
        {
            var bytes = ToBytes(objeto);

            var encoding = Encoding.GetEncoding("iso-8859-1");
            var binaryText = encoding.GetString(bytes, 0, bytes.Length);
            var base64Text = StringToBase64(binaryText);

            return base64Text;
        }


		/// <summary>
		/// Convert a base 64 string to a object of type T
		/// </summary>
		/// <typeparam name="T">Object Type</typeparam>
		/// <param name="conteudo">Base 64 string</param>
		/// <returns>Object</returns>
        public static T FromBase64<T>(this string conteudo)
        {
            var binaryText = Base64ToString(conteudo);

            var encoding = Encoding.GetEncoding("iso-8859-1");
            var bytes = encoding.GetBytes(binaryText);

            return FromBytes<T>(bytes);
        }

        private static string StringToBase64(string text)
        {
            var textBytes = Encoding.Unicode.GetBytes(text);
            var returnValue = Convert.ToBase64String(textBytes);

            return returnValue;
        }

        private static string Base64ToString(string text)
        {
            var textBytes = Convert.FromBase64String(text);
            var returnValue = Encoding.Unicode.GetString(textBytes, 0, textBytes.Length);

            return returnValue;
        }

		/// <summary>
		/// Serialize a object of type T to a XML string
		/// </summary>
		/// <typeparam name="T">Object Type</typeparam>
		/// <param name="objeto">Object Value</param>
		/// <returns>Object XML string</returns>
        public static string ToXml<T>(this T objeto)
        {
            string result;
            using (var sw = new StringWriter())
            {
                var serializador = new XmlSerializer(typeof(T));
                serializador.Serialize(sw, objeto);
                result = sw.ToString();
            }
            return result;
        }

		/// <summary>
		/// Serialize an object of type T to a Stream
		/// </summary>
		/// <typeparam name="T">Object Type</typeparam>
		/// <param name="obj">Object Value</param>
		/// <param name="stream">Stream</param>
        public static void SerializeTo<T>(this T obj, Stream stream)
        {
#if !__MOBILE__
            new BinaryFormatter().Serialize(stream, obj);
#else
            var json = JsonConvert.SerializeObject(obj);

            using (var sw = new StreamWriter(stream))
            {
                sw.Flush();
                sw.Write(json);
            }
            stream.Position = 0;
#endif
        }

		/// <summary>
		/// Deserialize an object of Type T from a Stream
		/// </summary>
		/// <typeparam name="T">Object Type</typeparam>
		/// <param name="stream">Stream containing the object data</param>
		/// <returns>Object T</returns>
        public static T Deserialize<T>(this Stream stream)
        {
#if !__MOBILE__
            return (T)new BinaryFormatter().Deserialize(stream);
#else
            var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(json);
#endif
        }
    }
}
