using SuperPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SuperPhotoShop.Infrostructure.Storage
{
    public static class LocalCacheStorage
    {
        private static Dictionary<string, CacheEntry> _cache = new Dictionary<string, CacheEntry>();
        private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public static void Add(string url, ImageModel image)
        {
            _cache[url] = new CacheEntry { Image = image, ExpirationTime = DateTime.Now.Add(_cacheDuration) };
        }

        public static bool TryGet(string url, out ImageModel image)
        {
            if (_cache.ContainsKey(url))
            {
                if (_cache[url].ExpirationTime > DateTime.Now)
                {
                    image = _cache[url].Image;
                    return true;
                }
                else
                {
                    _cache.Remove(url);
                }
            }
            image = null;
            return false;
        }
    }
}
