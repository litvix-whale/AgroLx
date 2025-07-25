﻿using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Xml
{
    public class RealEstateSearchCriteria
    {
        public Guid? UserId { get; set; }
        
        public string? SearchQuery { get; set; }
        public ProductCategoryEnum? Category { get; set; }
        public RealEstateTypeEnum? RealtyType { get; set; }
        public DealTypeEnum? Deal { get; set; }
        public bool? IsNewBuilding { get; set; }

        // Локація
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? Locality { get; set; }
        public string? Borough { get; set; }

        // Характеристики
        public int? MinFloor { get; set; }
        public int? MaxFloor { get; set; }
        public float? MinAreaTotal { get; set; }
        public float? MaxAreaTotal { get; set; }
        public int? MinRoomCount { get; set; }
        public int? MaxRoomCount { get; set; }

        // Ціна
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public CurrencyEnum? Currency { get; set; }

        // Пагінація
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;

        // Сортування
        public string? SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = true;
    }
}
