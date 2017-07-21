using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGrapher
{
    public class MyJSON
    {
        internal JsonStruct _value;

        public MyJSON(string strJson)
        {
            _value = new JsonStruct("root", strJson.Replace('\n', ' ').Replace('\r', ' '));
        }

        public JsonStruct this[string key]
        { get { return _value[key]; } }

        public override string ToString()
        {
            return _value.ToString();
        }

        public class JsonStruct
        {
            public enum JsonType
            {
                Integer,
                String,
                JsonGroup,
                Array
            }

            private Dictionary<string, JsonStruct> __jsonDict = new Dictionary<string, JsonStruct>();
            private List<JsonStruct> __jsonArray = new List<JsonStruct>();
            private string __jsonString;
            private int __jsonInteger;

            public JsonStruct(string parentName, string strJson)
            {
                ParentName = parentName;
                _decode(strJson);
            }

            public JsonStruct this[string key]
            {
                get
                {
                    return __jsonDict[key];
                }
            }
            public string ParentName { get; private set; }
            public JsonType Type { get; private set; }
            public object Value
            {
                get
                {
                    switch (Type)
                    {
                        case JsonType.Integer:
                            return __jsonInteger;
                        case JsonType.String:
                            return __jsonString;
                        case JsonType.JsonGroup:
                            return __jsonDict;
                        case JsonType.Array:
                            return __jsonArray;
                    }
                    return null;
                }
            }

            public override string ToString()
            {
                string strReturn = "";
                switch (Type)
                {
                    case JsonType.Integer:
                        strReturn += __jsonInteger.ToString();
                        break;
                    case JsonType.String:
                        strReturn += "\"";
                        strReturn += __jsonString;
                        strReturn += "\"";
                        break;
                    case JsonType.JsonGroup:
                        strReturn += "{";
                        List<string> items = new List<string>(__jsonDict.Count);
                        foreach (KeyValuePair<string,JsonStruct> item in __jsonDict)
                            items.Add("\"" + item.Key + "\": " + item.Value.ToString());
                        strReturn += string.Join(", ", items.ToArray());
                        strReturn += "}";
                        break;
                    case JsonType.Array:
                        strReturn += "[";
                        List<string> tmpArray = new List<string>();
                        foreach (JsonStruct item in __jsonArray)
                            tmpArray.Add(item.ToString());
                        strReturn += string.Join(", ", tmpArray.ToArray());
                        strReturn += "]";
                        break;
                    default:
                        break;
                }
                return strReturn;
            }

            public bool ContainsKey(string strKey)
            {
                return __jsonDict.ContainsKey(strKey);
            }

            private void _decode(string strJson)
            {
                string[] _keyValItems;
                string[] _arrayItems;

                strJson = strJson.Trim(' ');
                switch (strJson[0])
                {
                    case '{':
                        Type = JsonType.JsonGroup;
                        _keyValItems = GetKeyItemPairs(strJson.TrimStart('{').TrimEnd('}').Trim(' '));
                        foreach (string keyVal in _keyValItems)
                        {
                            string _key = keyVal.Split(':')[0].Trim('\"', '\'');
                            string _val = keyVal.Remove(0, keyVal.IndexOf(':') + 1);
                            __jsonDict.Add(_key, new JsonStruct(_key, _val));
                        }
                        _keyValItems = null;
                        break;
                    case '[':
                        Type = JsonType.Array;
                        _arrayItems = GetKeyItemPairs(strJson.TrimStart('[').TrimEnd(']').Trim(' '));
                        foreach (string item in _arrayItems)
                            __jsonArray.Add(new JsonStruct(ParentName, item));
                        break;
                    case '\"':
                    case '\'':
                        Type = JsonType.String;
                        __jsonString = strJson.Trim('\"', '\'');
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        Type = JsonType.Integer;
                        __jsonInteger = int.Parse(strJson.Trim());
                        break;
                    default:
                        break;
                }
            }

            private string[] GetKeyItemPairs(string strJson)
            {
                List<int> lstSeparator = new List<int>(new int[] { 0 });
                Stack<char> stackChar = new Stack<char>();
                char poppedChar;

                for (int i = 0; i < strJson.Length; i++)
                {
                    switch (strJson[i])
                    {
                        case '{':
                            stackChar.Push('{');
                            break;
                        case '}':
                            poppedChar = stackChar.Pop();
                            if (poppedChar != '{')
                                throw new KeyNotFoundException();
                            break;
                        case '[':
                            stackChar.Push('[');
                            break;
                        case ']':
                            poppedChar = stackChar.Pop();
                            if (poppedChar != '[')
                                throw new KeyNotFoundException();
                            break;
                        case ',':
                            if (stackChar.Count == 0)
                                lstSeparator.Add(i);
                            break;
                        default:
                            break;
                    }
                }
                lstSeparator.Add(strJson.Length - 1);

                string strDummy = strJson;
                string[] strReturn = new string[lstSeparator.Count - 1];
                for (int x = 0; x < strReturn.Length; x++)
                {
                    strReturn[x] = strJson.Substring(lstSeparator[x], lstSeparator[x + 1] - lstSeparator[x] + 1).Trim(' ', ',');
                }

                return strReturn;
            }
        }
    }
}
