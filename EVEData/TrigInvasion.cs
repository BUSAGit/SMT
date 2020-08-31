﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Triangles;
//
//    var invasion = Invasion.FromJson(jsonString);
// Data from : https://kybernaut.space/invasions.json

namespace Triangles
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;


    public partial class Invasion
    {
        [JsonProperty("system_id")]
        public long SystemId { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("derived_security_status")]
        public string DerivedSecurityStatus { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("system_name")]
        public string SystemName { get; set; }

        [JsonProperty("system_security")]
        public string SystemSecurity { get; set; }

        [JsonProperty("system_sovereignty")]
        public SystemSovereignty SystemSovereignty { get; set; }


        public override string ToString()
        {
            return $"{SystemName}, {Status.ToString()}";
        }
    }

    public enum Status { EdencomMinorVictory, FinalLiminality, Fortress, StellarReconnaissance, TriglavianMinorVictory };

    public enum SystemSovereignty { Amarr, Caldari, Gallente, Minmatar };

    public partial class Invasion
    {
        public static Invasion[] FromJson(string json) => JsonConvert.DeserializeObject<Invasion[]>(json, Triangles.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Invasion[] self) => JsonConvert.SerializeObject(self, Triangles.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StatusConverter.Singleton,
                SystemSovereigntyConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "edencom_minor_victory":
                    return Status.EdencomMinorVictory;
                case "final_liminality":
                    return Status.FinalLiminality;
                case "fortress":
                    return Status.Fortress;
                case "stellar_reconnaissance":
                    return Status.StellarReconnaissance;
                case "triglavian_minor_victory":
                    return Status.TriglavianMinorVictory;
                case "bulwark":
                    return Status.EdencomMinorVictory;
                case "redoubt":
                    return Status.EdencomMinorVictory;



            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.EdencomMinorVictory:
                    serializer.Serialize(writer, "edencom_minor_victory");
                    return;
                case Status.FinalLiminality:
                    serializer.Serialize(writer, "final_liminality");
                    return;
                case Status.Fortress:
                    serializer.Serialize(writer, "fortress");
                    return;
                case Status.StellarReconnaissance:
                    serializer.Serialize(writer, "stellar_reconnaissance");
                    return;
                case Status.TriglavianMinorVictory:
                    serializer.Serialize(writer, "triglavian_minor_victory");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class SystemSovereigntyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SystemSovereignty) || t == typeof(SystemSovereignty?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "amarr":
                    return SystemSovereignty.Amarr;
                case "caldari":
                    return SystemSovereignty.Caldari;
                case "gallente":
                    return SystemSovereignty.Gallente;
                case "minmatar":
                    return SystemSovereignty.Minmatar;
            }
            throw new Exception("Cannot unmarshal type SystemSovereignty");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SystemSovereignty)untypedValue;
            switch (value)
            {
                case SystemSovereignty.Amarr:
                    serializer.Serialize(writer, "amarr");
                    return;
                case SystemSovereignty.Caldari:
                    serializer.Serialize(writer, "caldari");
                    return;
                case SystemSovereignty.Gallente:
                    serializer.Serialize(writer, "gallente");
                    return;
                case SystemSovereignty.Minmatar:
                    serializer.Serialize(writer, "minmatar");
                    return;
            }
            throw new Exception("Cannot marshal type SystemSovereignty");
        }

        public static readonly SystemSovereigntyConverter Singleton = new SystemSovereigntyConverter();
    }
}
