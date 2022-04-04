using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BadDragonAPI.Data
{
    public partial class Products
    {
        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("basePrice")]
        public string BasePrice { get; set; }

        [JsonProperty("baseWeight")]
        public string BaseWeight { get; set; }

        [JsonProperty("productImageId")]
        public long ProductImageId { get; set; }

        [JsonProperty("type")]
        public ProductType Type { get; set; }

        [JsonProperty("previewImageId")]
        public long? PreviewImageId { get; set; }

        [JsonProperty("previewBaseThreshold")]
        public string PreviewBaseThreshold { get; set; }

        [JsonProperty("previewBaseMargin")]
        public string PreviewBaseMargin { get; set; }

        [JsonProperty("previewFadeGreenOnly")]
        public bool? PreviewFadeGreenOnly { get; set; }

        [JsonProperty("defaultHsCode")]
        public long DefaultHsCode { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("endDate")]
        public object EndDate { get; set; }

        [JsonProperty("seoTitle")]
        public string SeoTitle { get; set; }

        [JsonProperty("seoDescription")]
        public string SeoDescription { get; set; }

        [JsonProperty("furryDescription")]
        public string FurryDescription { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("furryTitle")]
        public string FurryTitle { get; set; }

        [JsonProperty("previewModelId")]
        public long? PreviewModelId { get; set; }

        [JsonProperty("previewNormalMapId")]
        public long? PreviewNormalMapId { get; set; }

        [JsonProperty("previewTextureMapId")]
        public long? PreviewTextureMapId { get; set; }

        [JsonProperty("productVideoId")]
        public object ProductVideoId { get; set; }

        [JsonProperty("sortOrder")]
        public long SortOrder { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("productTaxCode")]
        public long? ProductTaxCode { get; set; }

        [JsonProperty("internalProductionDisplayName")]
        public string InternalProductionDisplayName { get; set; }

        [JsonProperty("productImage")]
        public PreviewImage ProductImage { get; set; }

        [JsonProperty("relatedProducts")]
        public object[] RelatedProducts { get; set; }

        [JsonProperty("scaleImages")]
        public ScaleImage[] ScaleImages { get; set; }

        [JsonProperty("productThumbnails")]
        public DescriptionImage[] ProductThumbnails { get; set; }

        [JsonProperty("previewImage", NullValueHandling = NullValueHandling.Ignore)]
        public PreviewImage PreviewImage { get; set; }

        [JsonProperty("previewObjModel", NullValueHandling = NullValueHandling.Ignore)]
        public PreviewImage PreviewObjModel { get; set; }

        [JsonProperty("furryDescriptionImage")]
        public DescriptionImage FurryDescriptionImage { get; set; }

        [JsonProperty("previewTextureMap", NullValueHandling = NullValueHandling.Ignore)]
        public PreviewImage PreviewTextureMap { get; set; }

        [JsonProperty("previewNormalMap", NullValueHandling = NullValueHandling.Ignore)]
        public PreviewImage PreviewNormalMap { get; set; }

        [JsonProperty("descriptionImage")]
        public DescriptionImage DescriptionImage { get; set; }
    }

    public partial class DescriptionImage
    {
        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductId { get; set; }

        [JsonProperty("thumbImageId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ThumbImageId { get; set; }

        [JsonProperty("fullImageId", NullValueHandling = NullValueHandling.Ignore)]
        public long? FullImageId { get; set; }

        [JsonProperty("sortOrder", NullValueHandling = NullValueHandling.Ignore)]
        public long? SortOrder { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public DescriptionImageType? Type { get; set; }

        [JsonProperty("medImageId")]
        public long? MedImageId { get; set; }

        [JsonProperty("thumbImage", NullValueHandling = NullValueHandling.Ignore)]
        public PreviewImage ThumbImage { get; set; }

        [JsonProperty("fullImage", NullValueHandling = NullValueHandling.Ignore)]
        public PreviewImage FullImage { get; set; }

        [JsonProperty("medImage", NullValueHandling = NullValueHandling.Ignore)]
        public PreviewImage MedImage { get; set; }
    }

    public partial class PreviewImage
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class ScaleImage
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("optionId")]
        public long OptionId { get; set; }

        [JsonProperty("imageId")]
        public long ImageId { get; set; }

        [JsonProperty("image")]
        public PreviewImage Image { get; set; }
    }

    public enum DescriptionImageType { Description, FurryDescription, Product };

    public enum ProductType { Accessory, Insertable, Merchandise, Packer, Penetrable, Shooter, Vibrator, Wearable };

    public partial class Products
    {
        public static Products[] FromJson(string json) => JsonConvert.DeserializeObject<Products[]>(json, BadDragonAPI.Data.ConverterProducts.Settings);
    }

    internal static class ConverterProducts
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                DescriptionImageTypeConverter.Singleton,
                ProductTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class DescriptionImageTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DescriptionImageType) || t == typeof(DescriptionImageType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "description":
                    return DescriptionImageType.Description;
                case "furryDescription":
                    return DescriptionImageType.FurryDescription;
                case "product":
                    return DescriptionImageType.Product;
            }
            throw new Exception("Cannot unmarshal type DescriptionImageType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DescriptionImageType)untypedValue;
            switch (value)
            {
                case DescriptionImageType.Description:
                    serializer.Serialize(writer, "description");
                    return;
                case DescriptionImageType.FurryDescription:
                    serializer.Serialize(writer, "furryDescription");
                    return;
                case DescriptionImageType.Product:
                    serializer.Serialize(writer, "product");
                    return;
            }
            throw new Exception("Cannot marshal type DescriptionImageType");
        }

        public static readonly DescriptionImageTypeConverter Singleton = new DescriptionImageTypeConverter();
    }

    internal class ProductTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProductType) || t == typeof(ProductType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "accessory":
                    return ProductType.Accessory;
                case "insertable":
                    return ProductType.Insertable;
                case "merchandise":
                    return ProductType.Merchandise;
                case "packer":
                    return ProductType.Packer;
                case "penetrable":
                    return ProductType.Penetrable;
                case "shooter":
                    return ProductType.Shooter;
                case "vibrator":
                    return ProductType.Vibrator;
                case "wearable":
                    return ProductType.Wearable;
            }
            throw new Exception("Cannot unmarshal type ProductType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ProductType)untypedValue;
            switch (value)
            {
                case ProductType.Accessory:
                    serializer.Serialize(writer, "accessory");
                    return;
                case ProductType.Insertable:
                    serializer.Serialize(writer, "insertable");
                    return;
                case ProductType.Merchandise:
                    serializer.Serialize(writer, "merchandise");
                    return;
                case ProductType.Packer:
                    serializer.Serialize(writer, "packer");
                    return;
                case ProductType.Penetrable:
                    serializer.Serialize(writer, "penetrable");
                    return;
                case ProductType.Shooter:
                    serializer.Serialize(writer, "shooter");
                    return;
                case ProductType.Vibrator:
                    serializer.Serialize(writer, "vibrator");
                    return;
                case ProductType.Wearable:
                    serializer.Serialize(writer, "wearable");
                    return;
            }
            throw new Exception("Cannot marshal type ProductType");
        }

        public static readonly ProductTypeConverter Singleton = new ProductTypeConverter();
    }
}
