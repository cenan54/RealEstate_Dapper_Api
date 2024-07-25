﻿using Dapper;
using RealEstate_Dapper_Api.Dtos.ChartDtos;
using RealEstate_Dapper_UI.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.EstateAgentRepositories.DashboardRepositories.ChartRepositories
{
    public class ChartRepository : IChartRepository
    {
        private readonly Context _context;

        public ChartRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultChartDto>> Get5CityForChart()
        {
            string query = "select top(5) City,Count(*) as 'CityCount' from Product Group By City order by CityCount desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultChartDto>(query);
                return values.ToList();
            }
        }
    }
}
