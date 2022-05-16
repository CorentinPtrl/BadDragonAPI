using BadDragonAPI.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadDragonAPI
{
    public static class Extensions
    {
        public static string ToFriendlyString(this Size me)
        {
            switch (me)
            {
                case Size.OneSize:
                    return "One-Size";
                    break;
                case Size.Mini:
                    return "Mini";
                    break;
                case Size.Small:
                    return "Small";
                    break;
                case Size.Medium:
                    return "Medium";
                    break;
                case Size.Large:
                    return "Large";
                    break;
                case Size.ExtraLarge:
                    return "Extra Large";
                    break;
                case Size.TwoTLarge:
                    return "2X Large";
                    break;
                default:
                    return "None";
            }
        }
    }
}
