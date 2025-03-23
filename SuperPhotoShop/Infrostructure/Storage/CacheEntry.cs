using SuperPhotoShop.Models;
using System;

namespace SuperPhotoShop.Infrostructure.Storage
{
    internal class CacheEntry
    {
        public DateTime ExpirationTime { get; set; }
        public ImageModel Image { get; set; }
    }
}