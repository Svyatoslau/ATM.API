﻿using ATM.API.Model;

namespace ATM.API
{
    public class AtmsDataStore
    {
        public List<AtmDto> ATMs { get; set; }
        public static AtmsDataStore Current { get; set; } = new AtmsDataStore();

        public AtmsDataStore()
        {
            ATMs = new List<AtmDto>()
            {
                new AtmDto()
                {
                    Id = 0,
                    Name = "Central ATM",
                    TotalMoney = 1000
                }
            };
        }
    }
}