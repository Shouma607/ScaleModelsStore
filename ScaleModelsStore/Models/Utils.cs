using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public partial class Utils
    {
        public static Dictionary<String, String> CountriesList()
        {
            Dictionary<String, String> countriesList = new Dictionary<String, String>();
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (var item in getCultureInfo)
            {
                RegionInfo getRegionInfo = new RegionInfo(item.LCID);
                if (!(countriesList.ContainsKey(getRegionInfo.Name)))
                {
                    countriesList.Add(getRegionInfo.Name, getRegionInfo.EnglishName);
                }
            }

            return countriesList;
        }

        public static Dictionary<int, string> GetSortOptions()
        {
            Dictionary<int, string> SortOptions = new Dictionary<int, string>();

            SortOptions.Add(0, "Default");
            SortOptions.Add(1, "Price ↓");
            SortOptions.Add(2, "Price ↑");
            SortOptions.Add(3, "New items");

            return SortOptions;
        }

        public static SortedDictionary<int, string> GetScalesList(List<Product> products)
        {
            SortedDictionary<int, string> ScalesList = new SortedDictionary<int, string>();

            foreach (var product in products)
            {
                if (!(ScalesList.ContainsKey(Int32.Parse(product.Scale.Substring(2)))))
                    ScalesList.Add(Int32.Parse(product.Scale.Substring(2)), product.Scale);
            }

            return ScalesList;
        }

        public static Dictionary<int, string> GetAddressDropDown(List<Address> addressList)
        {
            Dictionary<int, string> AddressList = new Dictionary<int, string>();

            int key = 0;

            foreach (var item in addressList)                
               AddressList.Add(key++, item.ShortDescription);
            

            return AddressList;
        }
    }
}