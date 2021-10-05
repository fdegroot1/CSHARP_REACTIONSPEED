using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parser
{
    class DataParser
    {
        public const string LOGIN = "LOGIN";
        public const string SAVEHIGHSCORE = "SAVEHIGHSCORE";
        public const string GETHIGHSCORELIST = "GETHIGHSCORELIST";
        public const string HIGHSCORELIST = "HIGHSCORELIST";
        public const string DISCONNECT = "DISCONNECT";
        public static string getJsonIdentifier(byte[] messageBytes)
        {
            dynamic json = JsonConvert.DeserializeObject(Encoding.ASCII.GetString(messageBytes.Skip(4).ToArray()));
            return json.identifier;
        }

        public static byte[] loginJSON(string name)
        {
            dynamic json = new
            {
                identifier = LOGIN,
                data = new
                {
                    username = name
                }
            };

            return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json));
        }

        public static string getLoginUsername(byte[] bytes)

        {
            dynamic json = JsonConvert.DeserializeObject(Encoding.ASCII.GetString(bytes));
            string username = json.data.username;
            return username;
        }

        public static byte[] saveHighScoreJSON(double time)
        {
            dynamic json = new
            {
                identifier = SAVEHIGHSCORE,
                data = new
                {
                    highscore = time
                }
            };

            return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json));
        }

        public static double getHighscore(byte[] bytes)
        {
            dynamic json = JsonConvert.DeserializeObject(Encoding.ASCII.GetString(bytes));
            double time = json.data.highscore;
            return time;
        }

        public static byte[] getHighScoreListJSON()
        {
            dynamic json = new
            {
                identifier = GETHIGHSCORELIST

            };
            return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json));
        }

        public static byte[] highScoreListJSON(List<double> list)
        {
            dynamic json = new
            {
                identifier = GETHIGHSCORELIST,
                data = new
                {
                    highscorelist = list
                }

            };
            return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json));
        }

        public static List<double> highScoreList(byte[] bytes)
        {
            dynamic json = JsonConvert.DeserializeObject(Encoding.ASCII.GetString(bytes));
            List<double> list = json.data.highscorelist;
            return list;
        }

        public static byte[] disconnectJSON()
        {
            dynamic json = new
            {
                identifier = DISCONNECT
            };
            return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json));
        }

        public static byte[] getMessage(byte[] payload)
        {
            byte[] res = new byte[payload.Length + 4];

            Array.Copy(BitConverter.GetBytes(payload.Length + 4), 0, res, 0, 4);
            Array.Copy(payload, 0, res, 4, payload.Length);

            return res;
        }
    }

}
