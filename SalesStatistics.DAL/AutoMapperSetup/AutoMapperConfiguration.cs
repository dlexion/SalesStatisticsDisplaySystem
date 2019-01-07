﻿using AutoMapper;

namespace SalesStatistics.DAL.AutoMapperSetup
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<AutoMapperProfile>();
            });
        }
    }
}