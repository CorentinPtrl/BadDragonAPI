using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BadDragonAPI.Data
{
    public partial class Inventory
    {
        [JsonProperty("limit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Limit { get; set; }

        [JsonProperty("page")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Page { get; set; }

        [JsonProperty("toys")]
        public Toy[] Toys { get; set; }
    }

    public partial class Toy
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_list")]
        public string IdList { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("firmness")]
        public long Firmness { get; set; }

        [JsonProperty("cumtube")]
        public long Cumtube { get; set; }

        [JsonProperty("suction_cup")]
        public long SuctionCup { get; set; }

        [JsonProperty("color_theme")]
        public long? ToyColorTheme { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("is_flop")]
        public bool IsFlop { get; set; }

        [JsonProperty("external_flop_reason")]
        public string ExternalFlopReason { get; set; }

        [JsonProperty("color_display")]
        public ColorDisplay? ColorDisplay { get; set; }

        [JsonProperty("original_price")]
        public string OriginalPrice { get; set; }

        [JsonProperty("colorTheme", NullValueHandling = NullValueHandling.Ignore)]
        public ColorTheme ColorTheme { get; set; }

        [JsonProperty("images")]
        public Image[] Images { get; set; }
    }

    public partial class ColorTheme
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("bodyOptionId")]
        public long BodyOptionId { get; set; }

        [JsonProperty("baseOptionId")]
        public long BaseOptionId { get; set; }

        [JsonProperty("priceModifier")]
        public string PriceModifier { get; set; }

        [JsonProperty("singleProduct")]
        public bool SingleProduct { get; set; }

        [JsonProperty("specification")]
        public string Specification { get; set; }

        [JsonProperty("isSurpriseMe")]
        public bool IsSurpriseMe { get; set; }

        [JsonProperty("globalPreviewOverrideImage")]
        public object GlobalPreviewOverrideImage { get; set; }

        [JsonProperty("swatchImageId")]
        public long SwatchImageId { get; set; }

        [JsonProperty("sort_order")]
        public long SortOrder { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("endDate")]
        public object EndDate { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("inventoryToyId")]
        public long InventoryToyId { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("sortOrder")]
        public long SortOrder { get; set; }

        [JsonProperty("imageUrlFull")]
        public Uri ImageUrlFull { get; set; }

        [JsonProperty("imageUrl450")]
        public Uri ImageUrl450 { get; set; }

        [JsonProperty("imageUrl240")]
        public Uri ImageUrl240 { get; set; }

        [JsonProperty("imageUrl150")]
        public Uri ImageUrl150 { get; set; }

        [JsonProperty("imageUrl1200")]
        public Uri ImageUrl1200 { get; set; }

        [JsonProperty("isFlopPhoto")]
        public bool IsFlopPhoto { get; set; }
    }

    public enum ColorDisplay { Empty, Frankenpour, Rogue };

    public enum Size : long { 
        OneSize = 6,
        Mini = 10,
        Small = 1,
        Medium = 2,
        Large = 8,
        ExtraLarge = 3,
        TwoTLarge = 287

    };

    public partial class Inventory
    {
        public static Inventory FromJson(string json) => JsonConvert.DeserializeObject<Inventory>(json, BadDragonAPI.Data.ConverterInventory.Settings);
    }

    internal static class ConverterInventory
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ColorDisplayConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ColorDisplayConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ColorDisplay) || t == typeof(ColorDisplay?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return ColorDisplay.Empty;
                case "Frankenpour":
                    return ColorDisplay.Frankenpour;
                case "Rogue":
                    return ColorDisplay.Rogue;
            }
            throw new Exception("Cannot unmarshal type ColorDisplay");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ColorDisplay)untypedValue;
            switch (value)
            {
                case ColorDisplay.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case ColorDisplay.Frankenpour:
                    serializer.Serialize(writer, "Frankenpour");
                    return;
                case ColorDisplay.Rogue:
                    serializer.Serialize(writer, "Rogue");
                    return;
            }
            throw new Exception("Cannot marshal type ColorDisplay");
        }

        public static readonly ColorDisplayConverter Singleton = new ColorDisplayConverter();
    }
}
