﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChessEngineTuner
{
    public partial struct ParameterGroup
    {
        static private JsonSerializerOptions Options => new()
        {
            WriteIndented = true,
            IncludeFields = true,
        };

        public void WriteToFile(string path)
        {
            // Write parameter group data in JSON format
            RawParameterGroup rawParams = new RawParameterGroup(this);
            string myJsonData = JsonSerializer.Serialize(rawParams, Options);
            File.WriteAllText(path, myJsonData);
        }

        public static ParameterGroup ReadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("There was no file at the specified path: {0}", path);
                return new ParameterGroup();
            }

            // Read all data from the file and creates a new parameter group
            string jsonData = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ParameterGroup>(jsonData, Options);
        }

        public struct Parameter
        {
            public int Value;
            public int MinDelta;
            public int MaxDelta;

            public Parameter(int val)
            {
                Value = val;
                MinDelta = 1;
                MaxDelta = 5;
            }

            public static implicit operator int(Parameter parameter) => parameter.Value;
            public static implicit operator Parameter(int value) => new Parameter(value);
        }
    }
}
